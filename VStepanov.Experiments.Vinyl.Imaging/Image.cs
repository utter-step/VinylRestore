using System.Drawing;
using System.IO;

namespace VStepanov.Experiments.Vinyl.Imaging
{
    public class Image
    {
        #region Fields
        private int[,] _imageData; 
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

            _imageData = new int[Width, Height];

            for (int x = 0; x < Width; x++)
            {
                for (int y = 0; y < Height; y++)
                {
                    _imageData[x, y] = bitmap.GetPixel(x, y).R;
                }
            }
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
        public int this[int x, int y]
        {
            get
            {
                return _imageData[x, y]; 
            }
        }
    }
}
