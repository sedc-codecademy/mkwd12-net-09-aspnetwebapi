using NotesAppConsoleClient.Models;

namespace NotesAppConsoleClient
{
    public static class ConsoleHelper
    {
        public static void ColorWriteLine(this string text, ConsoleColor color)
        {
            Console.ForegroundColor = color;
            Console.WriteLine(text);
            Console.ResetColor();
        }

        public static void PrintNotes(this List<NoteResponse> notes)
        {
            Console.Clear();
            ColorWriteLine("\n\tThe notes :", ConsoleColor.Blue);
            foreach (var note in notes)
            {
                Console.WriteLine(note);
            }
        }
    }
}
