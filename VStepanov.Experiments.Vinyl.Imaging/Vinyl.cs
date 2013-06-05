using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        #endregion

        #region Constructors
        public Vinyl(string path)
        {
            _recordImage = Image.FromFile(path);

            TrackWidth = ComputeTrackWidth();
            GapWidth = ComputeGapWidth(TrackWidth);

            SpinCount = (ComputePossibleSpinCount(TrackWidth) + 1) / 2;
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
        #endregion
    }
}
