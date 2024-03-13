namespace TextEditor.Core;

public interface IKeyHandler
{
    void Handle(object? sender, KeyPressedEventArgs e);
}
