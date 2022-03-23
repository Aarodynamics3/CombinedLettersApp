using System;
using System.IO;

namespace CombinedLettersApp {
    internal class Program {
        static void Main(string[] args) {
            // Directory path of the application executable.
            var startupPath = Path.GetDirectoryName(System.Diagnostics.Process.GetCurrentProcess().MainModule.FileName);

            var combinedPath = $"{startupPath}\\CombinedLetters";

            // If not in the right spot don't do anything.
            if (!Directory.Exists(combinedPath)) Environment.Exit(0);

            LetterService letterService = new LetterService(combinedPath);
            letterService.Run();
        }
    }
}
