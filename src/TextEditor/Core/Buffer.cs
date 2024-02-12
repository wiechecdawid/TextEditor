namespace TextEditor.Core;

internal sealed class Buffer
{
    private static readonly object Lock = new object();
    private static Buffer? Instance = null;
    private string? _buffer;
    
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
        return _buffer ?? string.Empty;
    }

    public void LoadBuffer(string buffer)
    {
        _buffer = buffer;
    }

    public void LoadBuffer(string[] buffer)
    {
        _buffer = string.Join('\n', buffer);
    }

    public void LoadBuffer(Func<string[]> buffer)
    {
        _buffer = string.Join('\n', buffer.Invoke());
    }

    public void WriteToBuffer(int l, int col, ReadOnlySpan<char> txt)
    {
        var index = 0;
        var line = 0;
        while (line < l)
        {
            var sub = (_buffer ?? "")[index..].AsSpan();
            index += sub.IndexOf('\n') + 1;
            line++;
        }

        index += col;
        var before = (_buffer ?? "").Substring(0, index).AsSpan();
        var after = (_buffer ?? "").Substring(index).AsSpan();
        _buffer = string.Concat(before, txt, after);
    }

    // Remove sequence of characters from buffer. l is the line (delimited by '\n'), col is the column, count is the number of backwards characters to remove.
    public void RemoveFromBuffer(int l, int col, int count)
    {
        var index = 0;
        var line = 0;
        while (line < l)
        {
            var sub = (_buffer ?? "")[index..].AsSpan();
            index += sub.IndexOf('\n') + 1;
            line++;
        }

        index += col;
        _buffer = _buffer?.Remove(index - count, count);
    }
}
