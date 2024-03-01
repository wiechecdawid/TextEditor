// See https://aka.ms/new-console-template for more information
using System;
using System.Threading;
using System.Threading.Tasks;
using TextEditor.Core;
using MyBuffer = TextEditor.Core.Buffer;

var fileName = args[0];

var cts = new CancellationTokenSource();

var buffer = new MyBuffer(fileName);
var view = new EditorView(buffer);
while (!cts.IsCancellationRequested)
{
    view.DisplayBuffer();
    var key = Console.ReadKey();
}  

// static async Task ControlConsoleSize(ConsoleHandler ch, CancellationToken stoppingToken)
// {
//     while(!stoppingToken.IsCancellationRequested)
//     {
//         ch.UpdateSizes();
//         await Task.Delay(100);
//     }
// }