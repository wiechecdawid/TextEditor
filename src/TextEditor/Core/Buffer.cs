namespace TextEditor.Core;

internal sealed class Buffer
{
    private static readonly object Lock = new object();
    private static Buffer? Instance = null;
    private string[]? _buffer;
    
    private Buffer()
    {
    }

    public static Buffer GetInstance()
    {
        lock(Lock)
        {
            Instance ??= new Buffer();
        }

        return Instance;
    }

    public string GetBuffer()
    {
        return string.Join('\n', _buffer ?? []);
    }

    public void LoadBuffer(string buffer)
    {
        _buffer = buffer.Split('\n');
    }

    public void LoadBuffer(string[] buffer)
    {
        _buffer = buffer;
    }

    public void LoadBuffer(Func<string[]> buffer)
    {
        _buffer = buffer.Invoke();
    }

    public void WriteToBuffer(int l, int col, ReadOnlySpan<string> txt)
    {
        // ref string line = ref _buffer[l];
        // line.Insert(col, txt.ToString());
        // _onBufferChanged?.Invoke(_buffer);
        throw new NotImplementedException();
    }

    public void RemoveFromBuffer(int l, int col, int count)
    {
        throw new NotImplementedException();
    }
}
