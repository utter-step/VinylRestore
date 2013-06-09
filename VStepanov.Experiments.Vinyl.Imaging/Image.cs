using System.Drawing;
using System.IO;
using System.Threading.Tasks;

namespace VStepanov.Experiments.Vinyl.Imaging
{
    public class Image
    {
        #region Fields
        private byte[,] _imageData;

        private Bitmap _trackImage;
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

            _trackImage = bitmap;
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

            int offset = bitmapData.Length - Width * Height * 4;
            int height = Height - 1;

            for (int i = offset, index = 0; i < bitmapData.Length - 4; i += 4, index++)
            {
                int x = index % Width;
                int y = height - index / Width;
                _imageData[x, y] = bitmapData[i];
            }
        }

        private static byte[] BitmapToByteArray(Bitmap bitmap)
        {
            var ms = new MemoryStream();
            bitmap.Save(ms, System.Drawing.Imaging.ImageFormat.Bmp);

            byte[] bitmapData = ms.ToArray();
            return bitmapData;
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

            set
            {
                _trackImage.SetPixel(x, y, Color.FromArgb(255, value, 0, 0));
            }
        }

        public Bitmap GetTrack()
        {
            return _trackImage;
        }
    }
}
