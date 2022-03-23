using System;
using System.IO;
using System.Linq;
using System.Collections;

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
        private string inputPath, archivePath, outputPath;

        public LetterService(string combinedLettersPath) {
            this.inputPath = $"{combinedLettersPath}\\Input";
            this.archivePath = $"{combinedLettersPath}\\Archive";
            this.outputPath = $"{combinedLettersPath}\\Output";
        }
          
        public void run() {
            var admissionPath = $"{inputPath}\\Admission";
            var scholarshipPath = $"{inputPath}\\Scholarship";

            // Folders in the Admissions and Scholarship directories.
            string[] admissions = Directory.GetDirectories(admissionPath)
                                        .Select(Path.GetFileName)
                                        .ToArray();
            string[] scholarships = Directory.GetDirectories(scholarshipPath)
                                        .Select(Path.GetFileName)
                                        .ToArray();

            foreach (var day in admissions.Intersect(scholarships)) {
                // Lists of student IDs from admissions and scholarships.
                ArrayList aList = getStudentIds(admissionPath, day), 
                          sList = getStudentIds(scholarshipPath, day);

                var students = aList.ToArray().Intersect(sList.ToArray());

                // If there are students to be processed and the day folder does not yet exist create it.
                if (students.Count() != 0 && !Directory.Exists($"{outputPath}\\{day}")) {
                    Directory.CreateDirectory($"{outputPath}\\{day}");
                }

                // For each student that has a file in both admissions and scholarships.
                foreach (var student in students) {
                    var resultFile = $"{outputPath}\\{day}\\combined-{student}.txt";
                    CombineTwoLetters($"{admissionPath}\\{day}\\admission-{student}.txt",
                                      $"{scholarshipPath}\\{day}\\scholarship-{student}.txt",
                                      resultFile);
                }
            }
        }

        private ArrayList getStudentIds(string filePath, string day) {
            var list = new ArrayList();

            foreach (var f in Directory.GetFiles($"{filePath}\\{day}")) {
                // Remove the admissions from the filename (ex: admissions-01830102)
                list.Add(Path.GetFileNameWithoutExtension(f).Split('-')[1]);
            }

            return list;
        }

        // Combines two letters into one result file in the Output folder. 
        public void CombineTwoLetters(string inputFile1, string inputFile2, string resultFile) {
            File.WriteAllText(resultFile, File.ReadAllText(inputFile1) + "\n" + File.ReadAllText(inputFile2));
        }
    }
}
