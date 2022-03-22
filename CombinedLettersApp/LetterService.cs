namespace CombinedLettersApp {
    public interface ILetterService {
        void CombineTwoLetters(string inputFile1, string inputFile2, string resultFile);
    }

    /*
    * LetterService will find students who have both admission letters and scholarship letters
    * and combine them in to the Output folder in the CombinedLetters folder. Then it will
    * generate a text report of the combined letters and move the processed files into Archive.
    */
    public class LetterService : ILetterService {
        private string combinedLettersFilePath;

        public LetterService(string filePath) {
            this.combinedLettersFilePath = filePath;
        }
          
        public void run() {

        }

        public void CombineTwoLetters(string inputFile1, string inputFile2, string resultFile) {
            //TODO
        }
    }
}
