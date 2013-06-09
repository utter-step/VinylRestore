using System;

namespace VStepanov.Experiments.Vinyl.Audio
{
    public abstract class AudioWriter : IDisposable
    {
        protected AudioWriter()
        {
            
        }

        public abstract void Write(byte[] data, string path);

        public abstract void Dispose();
    }
}
