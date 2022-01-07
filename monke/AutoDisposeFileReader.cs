using NAudio.Wave;

class AutoDisposeFileReader : IWaveProvider
{
    private readonly Mp3FileReader reader;
    private bool isDisposed;
    public AutoDisposeFileReader(Mp3FileReader reader)
    {
        this.reader = reader;
        this.WaveFormat = reader.WaveFormat;
    }

    public int Read(byte[] buffer, int offset, int count)
    {
        if (isDisposed)
            return 0;
        int read = reader.Read(buffer, offset, count);
        if (read == 0)
        {
            reader.Dispose();
            isDisposed = true;
        }
        return read;
    }

    public WaveFormat WaveFormat { get; private set; }
}