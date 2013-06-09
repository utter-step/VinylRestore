using System;
using System.IO;

namespace VStepanov.Experiments.Vinyl.Audio
{
    public abstract class AudioWriter : IDisposable
    {
        protected readonly FileStream FileStream;

        protected AudioWriter(string path)
        {
            FileStream = new FileStream(path, FileMode.OpenOrCreate, FileAccess.Write);
        }

        public abstract void Write(byte[] data, int offset);

        public virtual void Dispose()
        {
            FileStream.Dispose();
        }
    }
}
