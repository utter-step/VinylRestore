using System;
using System.IO;

namespace VStepanov.Experiments.Vinyl.Audio
{
    public abstract class AudioWriter : IDisposable
    {
        protected System.IO.FileStream _FileStream;

        protected AudioWriter(string path)
        {
            _FileStream = new FileStream(path, FileMode.OpenOrCreate, FileAccess.Write);
        }

        public abstract void Write(byte[] data, int offset);

        public virtual void Dispose()
        {
            _FileStream.Dispose();
        }
    }
}
