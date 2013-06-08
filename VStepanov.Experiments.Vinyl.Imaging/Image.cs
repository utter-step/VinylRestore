using System.Drawing;
using System.IO;

namespace VStepanov.Experiments.Vinyl.Imaging
{
    public class Image
    {
        #region Fields
        private byte[,] _imageData;

        private Bitmap _sourceImage;
        #endregion

        #region Properties
        public int Width { get; private set; }
        public int Height { get; private set; } 
        #endregion

        #region Constructors and constructing methods
        /// <summary>
        /// Class, providing access to image data.
        /// 
        /// Assuming that image is grayscale.
        /// </summary>
        /// <param name="bitmap">Source image.</param>
        public Image(Bitmap bitmap)
        {
            Width = bitmap.Width;
            Height = bitmap.Height;

            _imageData = new byte[Width, Height];

            MemoryStream ms = new MemoryStream();
            bitmap.Save(ms, System.Drawing.Imaging.ImageFormat.Bmp);
            byte[] bitmapData = ms.ToArray();

            int offset = bitmapData.Length - Width * Height * 4;

            for (int i = offset, index = 0; i < bitmapData.Length - 4; i += 4, index++)
            {
                int x = index % Width;
                int y = Height - index / Width - 1;
                _imageData[x, y] = bitmapData[i];
            }

            //for (int x = 0; x < Width; x++)
            //{
            //    for (int y = 0; y < Height; y++)
            //    {
            //        _imageData[x, y] = bitmap.GetPixel(x, y).G;
            //    }
            //}

            _sourceImage = bitmap;
        }

        /// <summary>
        /// Creates an instance of Image class from selected file.
        /// </summary>
        /// <param name="path">Path to file with image.</param>
        /// <returns></returns>
        public static Image FromFile(string path)
        {
            var bitmap = new Bitmap(path);

            return new Image(bitmap);
        } 
        #endregion

        /// <summary>
        /// Returns lightness of desired pixel.
        /// </summary>
        /// <param name="x">X-coordinate</param>
        /// <param name="y">Y-coordinate</param>
        /// <returns>Lightness at [x, y]</returns>
        public byte this[int x, int y]
        {
            get
            {
                return _imageData[x, y]; 
            }

            set
            {
                _sourceImage.SetPixel(x, y, Color.FromArgb(255, value, 0, 0));
            }
        }

        public void SaveTrackWay(string path)
        {
            _sourceImage.Save(path);
        }
    }
}
