// See https://aka.ms/new-console-template for more information
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TextEditor.Core;
using MyBuffer = TextEditor.Core.Buffer;

// var fileName = args[0]; DEBUG:
var fileName = @"C:\Users\wiech\Documents\Projects\TextEditor\TextEditor.sln";

var cts = new CancellationTokenSource();

var buffer = new MyBuffer(fileName);
CursorHandler cursorHandler = new();
IKeyHandler[] keyHandlers = [cursorHandler];
EditorView view = new (buffer);
EditorController controller = new (view, keyHandlers);

while (!cts.IsCancellationRequested)
{
    view.DisplayBuffer();
    var key = Console.ReadKey();
    controller.OnKeyPressed(key);
}  

Console.Clear();

// static async Task ControlConsoleSize(ConsoleHandler ch, CancellationToken stoppingToken)
// {
//     while(!stoppingToken.IsCancellationRequested)
//     {
//         ch.UpdateSizes();
//         await Task.Delay(100);
//     }
// }