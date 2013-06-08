using System;

namespace VStepanov.Experiments.Vinyl.Imaging
{
    public class Vinyl
    {
        #region Constants (approximate)
        private const int MINIMAL_TRACK_WIDTH = 3;
        private const int MINIMAL_TRACK_LIGHTNESS = 70;

        private const int MINIMAL_PLATE_LIGHTNESS = 30;
        private const int TRESHOLD = 40;
        #endregion

        #region Fields and properties
        private Image _recordImage;

        public int TrackWidth { get; private set; }
        public int GapWidth { get; private set; }

        public int SpinCount { get; private set; }

        public TimeSpan Duration { get; private set; }
        #endregion

        #region Constructors
        public Vinyl(string path, int rotationsPerMinute = 120)
        {
            _recordImage = Image.FromFile(path);

            TrackWidth = ComputeTrackWidth();
            GapWidth = ComputeGapWidth(TrackWidth);

            SpinCount = (ComputePossibleSpinCount(TrackWidth) + 1) / 2;

            Duration = TimeSpan.FromMinutes(SpinCount / (double)rotationsPerMinute);
        } 
        #endregion

        #region Helper methods
        private int ComputeTrackWidth()
        {
            int middle = _recordImage.Height / 2 - 1;

            int trackWidth = 0;

            for (int x = 0; x < _recordImage.Width; x++)
            {
                if (_recordImage[x, middle] < MINIMAL_TRACK_LIGHTNESS)
                {
                    if (trackWidth > MINIMAL_TRACK_WIDTH)
                    {
                        break;
                    }

                    trackWidth = 0;
                }
                else
                {
                    trackWidth++;
                }
            }

            return trackWidth;
        }

        private int ComputeGapWidth(int trackWidth)
        {
            bool isFirstTrackDetected = false;

            int gap = 0;
            int currentTrackWidth = 0;

            int middle = _recordImage.Height / 2 - 1;

            for (int x = 0; x < _recordImage.Width; x++)
            {
                if (_recordImage[x, middle] < MINIMAL_TRACK_LIGHTNESS)
                {
                    if (currentTrackWidth >= trackWidth)
                    {
                        if (isFirstTrackDetected)
                        {
                            break;
                        }
                        isFirstTrackDetected = true;
                    }

                    currentTrackWidth = 0;
                    if (isFirstTrackDetected)
                    {
                        gap++;
                    }
                }
                else
                {
                    if (!isFirstTrackDetected)
                    {
                        gap = 0;
                    }
                    currentTrackWidth++;
                }
            }

            return gap;
        }

        private int ComputePossibleSpinCount(int trackWidth)
        {
            int middle = _recordImage.Height / 2 - 1;

            int count = 0;

            int currentWidth = 0;

            for (int x = 0; x < _recordImage.Width; x++)
            {
                if (_recordImage[x, middle] < MINIMAL_TRACK_LIGHTNESS)
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

        public int ComputeCenterX()
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

        public int ComputeCenterY()
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

        private int FindStartX(int trackWidth)
        {
            int xMiddle = ComputeCenterX();
            int yMiddle = ComputeCenterY();

            int currentWidth = 0;

            for (int x = 0; x < _recordImage.Width; x++)
            {
                if (_recordImage[x, yMiddle] > MINIMAL_TRACK_LIGHTNESS)
                {
                    currentWidth++;
                }

                else
                {
                    currentWidth = 0;
                }

                if (currentWidth >= trackWidth)
                {
                    return x - currentWidth;
                }
            }

            throw new ApplicationException("Cannot find the start of a spiral!");
        }

        /// <summary>
        /// Assuming we're starting at most right point of inner circle.
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
            int startX = FindStartX(TrackWidth) + TrackWidth / 2;

            int centerX = ComputeCenterX();
            int centerY = ComputeCenterY();

            int approximateSamplesCount = SamplesCount(startX);

            var audio = new byte[approximateSamplesCount];

            double lapRadiusDelta = TrackWidth + GapWidth + 0.85;

            double radius = centerX - startX - 1;

            double angle = Math.PI;

            double samplesCount = radius * 4;

            double angleDelta = (Math.PI * 2) / samplesCount;
            double radiusDelta = (lapRadiusDelta / samplesCount);

            int lapcount = 0;

            for (int i = 0; i < audio.Length; i++)
            {
                if (angle < -Math.PI) 
                {
                    radius = centerX - startX - 1 - ++lapcount * lapRadiusDelta;

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
