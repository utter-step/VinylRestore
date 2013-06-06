using System;

namespace VStepanov.Experiments.Vinyl.Imaging
{
    public class Vinyl
    {
        #region Constants (approximate)
        private const int MINIMAL_TRACK_WIDTH = 3;
        private const int MINIMAL_TRACK_LIGHTNESS = 70;
        #endregion

        #region Fields and properties
        private Image _recordImage;

        public int TrackWidth { get; private set; }
        public int GapWidth { get; private set; }

        public int SpinCount { get; private set; }

        public int MyProperty { get; private set; }

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

        private int FindStartX(int trackWidth)
        {
            int xMiddle = _recordImage.Width / 2 - 1;
            int yMiddle = _recordImage.Height / 2 - 1;

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
            int samples = 0;

            for (int i = 0; i < SpinCount; i++)
            {
                samples += ((_recordImage.Width / 2) - 1 - startX) * 4;
                startX += TrackWidth + GapWidth + 1;
            }

            return samples;
        }
        #endregion

        public byte[] ExtractAudioBytes()
        {
            int startX = FindStartX(TrackWidth) + TrackWidth / 2;

            int centerX = _recordImage.Width / 2 - 1;
            int centerY = _recordImage.Height / 2 + 7;

            int approximateSamplesCount = SamplesCount(startX);

            var audio = new byte[approximateSamplesCount];

            double lapRadiusDelta = TrackWidth + GapWidth + 0.9;

            double radius = centerX - startX - 5;
            centerX -= 4;

            double angle = Math.PI;

            double samplesCount = radius * 4;

            double angleDelta = (Math.PI * 2) / samplesCount;
            double radiusDelta = (lapRadiusDelta / samplesCount);

            int lapcount = 0;

            for (int i = 0; i < audio.Length; i++)
            {
                if (angle < -Math.PI) 
                {
                    radius = centerX - startX - ++lapcount * lapRadiusDelta;

                    samplesCount = radius * 4;

                    angleDelta = (Math.PI * 2) / samplesCount;
                    radiusDelta = lapRadiusDelta / samplesCount;

                    angle = Math.PI;
                }

                int x = (int)Math.Round(centerX + radius * Math.Cos(angle));
                int y = (int)Math.Round(centerY + radius * Math.Sin(angle));

                audio[i] = _recordImage[x, y];

                radius -= radiusDelta;
                angle -= angleDelta;
            }

            return audio;
        }
    }
}
