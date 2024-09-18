
using NotesAppConsoleClient;

"\n\t\tLogin menu\n\n\n".ColorWriteLine(ConsoleColor.DarkBlue);
"  Enter username:".ColorWriteLine(ConsoleColor.Blue);
var username = Console.ReadLine();
"\n  Enter password:".ColorWriteLine(ConsoleColor.Blue);
var password = Console.ReadLine();

Console.Clear();

var noteService = new NotesAppService();

try
{
    await noteService.UserLoginAsync(username, password);
    await noteService.GetNotesAsync();
}
catch (Exception ex)
{
    Console.Clear();
    ConsoleHelper.ColorWriteLine(ex.Message, ConsoleColor.Red);
}

Console.Read();