using System;
using System.IO;

namespace CombinedLettersApp {
    internal class Program {
        static void Main(string[] args) {
            // Directory path of the application executable.
            String startupPath = Path.GetDirectoryName(System.Diagnostics.Process.GetCurrentProcess().MainModule.FileName);
            
            /* 
             * File path for the CombinedLetters folder. Would likely be in another location
             * but uses startupPath here because it is nested in the application folders. Uses
             * Path.Combine to move up two directories from /bin/Debug.
             */
            String filePath = $"{Path.GetFullPath(Path.Combine(startupPath, "..", ".."))}\\CombinedLetters";

            LetterService letterService = new LetterService(filePath);
            letterService.run();
        }
    }
}
