using System;
using System.Drawing.Imaging;
using System.Drawing;
using System.IO;

namespace VStepanov.Experiments.Vinyl.Imaging
{
    public class Image
    {
        #region Fields
        private byte[,] _imageData;

        private Bitmap _originalImage;
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

            InitializeImageData(bitmap);

            _originalImage = bitmap;
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

        #region Constructor's helper methods
        private void InitializeImageData(Bitmap bitmap)
        {
            var bitmapData = BitmapToByteArray(bitmap);

            _imageData = new byte[Width, Height];

            int bytesPerPixel = BytesPerPixel(bitmap.PixelFormat);

            int offset = bitmapData.Length - Width * Height * bytesPerPixel;
            int height = Height - 1;

            for (int i = offset, index = 0; i < bitmapData.Length - bytesPerPixel; i += bytesPerPixel, index++)
            {
                int x = index % Width;
                int y = height - index / Width;
                _imageData[x, y] = bitmapData[i];
            }
        }

        private static byte[] BitmapToByteArray(Bitmap bitmap)
        {
            var ms = new MemoryStream();
            bitmap.Save(ms, ImageFormat.Bmp);

            byte[] bitmapData = ms.ToArray();
            return bitmapData;
        }

        private static int BytesPerPixel(PixelFormat pixelFormat)
        {
            switch (pixelFormat)
            {
                case PixelFormat.Alpha:
                case PixelFormat.Format8bppIndexed:
                case PixelFormat.PAlpha:
                    return 1;

                case PixelFormat.Format16bppArgb1555:
                case PixelFormat.Format16bppGrayScale:
                case PixelFormat.Format16bppRgb555:
                case PixelFormat.Format16bppRgb565:
                    return 2;

                case PixelFormat.Format24bppRgb:
                    return 3;

                case PixelFormat.Format32bppArgb:
                case PixelFormat.Format32bppPArgb:
                case PixelFormat.Format32bppRgb:
                case PixelFormat.Canonical:
                    return 4;

                default:
                    throw new NotImplementedException("Using unknown pixel format.");
            }
        }
        #endregion
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
        }

        public Bitmap GetOriginal()
        {
            var wholeImageRectangle = new Rectangle(new System.Drawing.Point(), _originalImage.Size);

            var copy = _originalImage.Clone(wholeImageRectangle, _originalImage.PixelFormat);
            return copy;
        }
    }
}
