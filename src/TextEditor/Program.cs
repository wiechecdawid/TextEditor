// See https://aka.ms/new-console-template for more information
using TextEditor.Core;

var fileName = args[0];

var cts = new CancellationTokenSource();
var ch = new ConsoleHandler(cts);

var task = Task.Run(() => ControlConsoleSize(ch, cts.Token));

new EditorView(ch, new FileHandler(fileName))
    .DisplayBuffer();

await task;

static void ControlConsoleSize(ConsoleHandler ch, CancellationToken stoppingToken)
{
    while(!stoppingToken.IsCancellationRequested)
    {
        ch.UpdateSizes();
        Thread.Sleep(100);
    }
}