using System;

namespace VStepanov.Experiments.Vinyl.Imaging
{
    public struct Point
    {
        public int X { get; private set; }
        public int Y { get; private set; }

        public Point(int x, int y)
            : this()
        {
            X = x;
            Y = y;
        }

        public override string ToString()
        {
            return String.Format("({0};{1})", X, Y);
        }
    }

    public class Vinyl
    {
        #region Constants (approximate)
        private const int MINIMAL_TRACK_WIDTH = 3;
        private const int MINIMAL_TRACK_LIGHTNESS = 70;

        private const int MINIMAL_PLATE_LIGHTNESS = 30;
        private const int TRESHOLD = 5;
        #endregion

        #region Fields and properties
        private Image _recordImage;

        public double TrackWidth { get; private set; }
        public double GapWidth { get; private set; }

        public int SpinCount { get; private set; }

        public TimeSpan Duration { get; private set; }

        public Point Center { get; private set; }
        #endregion

        #region Constructors
        public Vinyl(string path, int rotationsPerMinute = 120)
        {
            _recordImage = Image.FromFile(path);

            Center = new Point(ComputeCenterX(), ComputeCenterY());

            TrackWidth = ComputeAverageTrackWidth(Center);
            GapWidth = ComputeAverageGapWidth((int)TrackWidth, Center);

            SpinCount = (ComputePossibleSpinCount((int)TrackWidth, Center) + 1) / 2;

            Duration = TimeSpan.FromMinutes(SpinCount / (double)rotationsPerMinute);
        } 
        #endregion

        #region Helper methods
        private double ComputeAverageTrackWidth(Point center)
        {
            double totalWidth = 0;

            int trackWidth = 0;
            int count = 0;

            for (int x = 0; x < _recordImage.Width; x++)
            {
                if (_recordImage[x, center.Y] < MINIMAL_TRACK_LIGHTNESS)
                {
                    if (trackWidth > MINIMAL_TRACK_WIDTH)
                    {
                        count++;
                        totalWidth += trackWidth;
                    }

                    trackWidth = 0;
                }
                else
                {
                    trackWidth++;
                }
            }

            return totalWidth / count;
        }

        private double ComputeAverageGapWidth(int trackWidth, Point center)
        {
            bool isGap = false;

            double totalWidth = 0;
            int count = 0;

            int gapWidthLimit = trackWidth * 2;

            int gap = 0;
            int currentTrackWidth = 0;

            for (int x = 0; x < _recordImage.Width; x++)
            {
                if (_recordImage[x, center.Y] < MINIMAL_TRACK_LIGHTNESS)
                {
                    if (currentTrackWidth >= trackWidth)
                    {
                        isGap = true;
                    }

                    currentTrackWidth = 0;
                    gap++;
                }
                else
                {
                    if (isGap)
                    {
                        totalWidth += gap;
                        count++;
                        isGap = false;
                    }
                    gap = 0;
                    currentTrackWidth++;
                }

                if (gap > gapWidthLimit)
                {
                    isGap = false;
                }
            }

            return totalWidth / count;
        }

        private int ComputePossibleSpinCount(int trackWidth, Point center)
        {
            int count = 0;

            int currentWidth = 0;

            for (int x = 0; x < _recordImage.Width; x++)
            {
                if (_recordImage[x, center.Y] < MINIMAL_TRACK_LIGHTNESS)
                {
                    if (currentWidth >= trackWidth)
                    {
                        count++;
                    }

                    currentWidth = 0;
                }
                else
                {
                    currentWidth++;
                }
            }

            return count;
        }

        private int ComputeCenterX()
        {
            int left = 0,
                right = 0;

            for (int x = 0; x < _recordImage.Width; x++)
            {
                int count = 0;
                for (int y = 0; y < _recordImage.Height; y++)
                {
                    if (_recordImage[x, y] > MINIMAL_PLATE_LIGHTNESS)
                    {
                        count++;
                    }   
                }

                if (count > TRESHOLD)
                {
                    left = x;
                    break;
                }
            }

            for (int x = _recordImage.Width - 1; x >= 0; x--)
            {
                int count = 0;
                for (int y = 0; y < _recordImage.Height; y++)
                {
                    if (_recordImage[x, y] > MINIMAL_PLATE_LIGHTNESS)
                    {
                        count++;
                    }
                }

                if (count > TRESHOLD)
                {
                    right = x;
                    break;
                }
            }

            return right - (right - left) / 2;
        }

        private int ComputeCenterY()
        {
            int lower = 0,
                upper = 0;

            for (int y = 0; y < _recordImage.Height; y++)
            {
                int count = 0;
                for (int x = 0; x < _recordImage.Width; x++)
                {
                    if (_recordImage[x, y] > MINIMAL_PLATE_LIGHTNESS)
                    {
                        count++;
                    }
                }

                if (count > TRESHOLD)
                {
                    upper = y;
                    break;
                }
            }

            for (int y = _recordImage.Height - 1; y >= 0; y--)
            {
                int count = 0;
                for (int x = 0; x < _recordImage.Width; x++)
                {
                    if (_recordImage[x, y] > MINIMAL_PLATE_LIGHTNESS)
                    {
                        count++;
                    }
                }

                if (count > TRESHOLD)
                {
                    lower = y;
                    break;
                }
            }

            return lower - (lower - upper) / 2;
        }

        private int FindStartX()
        {
            int currentWidth = 0;

            for (int x = 0; x < _recordImage.Width; x++)
            {
                if (_recordImage[x, Center.Y] > MINIMAL_TRACK_LIGHTNESS)
                {
                    currentWidth++;
                }

                else
                {
                    currentWidth = 0;
                }

                if (currentWidth == MINIMAL_TRACK_WIDTH)
                {
                    return x - MINIMAL_TRACK_WIDTH + 1;
                }
            }

            throw new ApplicationException("Cannot find the start of a spiral!");
        }

        /// <summary>
        /// Assuming we're starting at most right point of outer "circle".
        /// </summary>
        /// <param name="startX">X-coordinates of a starting point.</param>
        private int SamplesCount(int startX)
        {
            int samples = ((_recordImage.Width / 2) - 1 - startX) * 4 * SpinCount;

            return samples;
        }
        #endregion

        public byte[] ExtractAudioBytes()
        {
            int startX = FindStartX() + (int)(TrackWidth / 2);

            int centerX = Center.X;
            int centerY = Center.Y;

            int approximateSamplesCount = SamplesCount(startX);

            var audio = new byte[approximateSamplesCount];

            double lapRadiusDelta = TrackWidth + GapWidth;

            double outerRadius = centerX - startX;
            double radius = outerRadius;

            double angle = Math.PI;

            double samplesCount = radius * 4;

            double angleDelta = (Math.PI * 2) / samplesCount;
            double radiusDelta = (lapRadiusDelta / samplesCount);

            int lapcount = 0;

            for (int i = 0; i < audio.Length; i++)
            {
                if (angle < -Math.PI) 
                {
                    radius = outerRadius - ++lapcount * lapRadiusDelta;

                    angle = Math.PI;
                }

                int x = (int)Math.Round(centerX + radius * Math.Cos(angle));
                int y = (int)Math.Round(centerY + radius * Math.Sin(angle));

                audio[i] = _recordImage[x, y];

                _recordImage[x, y] = 255;

                radius -= radiusDelta;
                angle -= angleDelta;
            }

            _recordImage.SAVE();

            return audio;
        }
    }
}
