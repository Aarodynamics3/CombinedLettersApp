using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;

namespace CombinedLettersApp {
    public interface ILetterService {
        /*
         * Combines two letter files into one file.
         * 
         * inputFile1: File path for first letter.
         * inputFile2: File path for second letter.
         * resultFile: File path for the combined letter.
         */
        void CombineTwoLetters(string inputFile1, string inputFile2, string resultFile);
    }

    /*
    * LetterService will find students who have both admission letters and scholarship letters
    * and combine them in to the Output folder in the CombinedLetters folder. Then it will
    * generate a text report of the combined letters and move the processed files into Archive.
    */
    public class LetterService : ILetterService {
        private string inputPath, archivePath, outputPath, logOutput;

        public LetterService(string combinedLettersPath) {
            this.inputPath = $"{combinedLettersPath}\\Input";
            this.archivePath = $"{combinedLettersPath}\\Archive";
            this.outputPath = $"{combinedLettersPath}\\Output";
            this.logOutput = "";
        }
          
        // Main function to run the LetterService process.
        public void Run() {
            log("Running " + DateTime.Now);
            var admissionPath = $"{inputPath}\\Admission";
            var scholarshipPath = $"{inputPath}\\Scholarship";

            // Folders in the Admissions and Scholarship directories.
            string[] admissions = Directory.GetDirectories(admissionPath)
                                        .Select(Path.GetFileName)
                                        .ToArray();
            string[] scholarships = Directory.GetDirectories(scholarshipPath)
                                        .Select(Path.GetFileName)
                                        .ToArray();

            // For each day folder in both Admission and Scholarship.
            foreach (var day in admissions.Intersect(scholarships)) {
                log("Processing day " + day);

                // Lists of student IDs from admissions and scholarships.
                var students = getStudentIds(admissionPath, day).Intersect(getStudentIds(scholarshipPath, day));

                // If there are no students to process, skip this day.
                if (students.Count() == 0) {
                    log("No messages to combine for " + day + ", continuing.");
                    continue;
                }

                // Output combined results file text.
                string outputResult = DateTime.Today.ToString("dd/MM/yyyy") + " Report\n" +
                                      "-----------------------------\n" +
                                      $"Number of Combined Letters: {students.Count()}\n";

                // Create the day folder in the Output dir.
                Directory.CreateDirectory($"{outputPath}\\{day}");
                log("Created " + day + " directory in Output folder.");

                // For each student that has a file in both admissions and scholarships.
                foreach (var student in students) {
                    // Add student ID to combined output list.
                    outputResult += student + "\n";
                    log("Combining student " + student);

                    CombineTwoLetters($"{admissionPath}\\{day}\\admission-{student}.txt",
                                      $"{scholarshipPath}\\{day}\\scholarship-{student}.txt",
                                      $"{outputPath}\\{day}\\combined-{student}.txt");
                }

                // Create the combined-log file in the Output folder.
                File.WriteAllText($"{outputPath}\\{day}\\combined-log.txt", outputResult);
                log("Created combined-log.txt.");
            }

            // Archive all of the processed folders.
            archive(admissions, admissionPath, "Admission");
            archive(scholarships, scholarshipPath, "Scholarship");
            log("Archived all folders in processed folders.");

            // Creating log file.
            var time = DateTime.Now.ToString("o");
            File.WriteAllText(Path.Combine(outputPath, DateTime.Now.ToString("yyyy-MM-dd-HH-mm-ss") + ".txt"), logOutput);
        }

        // Given an array of folders and their directory, move to respective (string type) Archive dir.
        private void archive(string[] folders, string path, string type) {
            foreach (var folder in folders) {
                Directory.Move($"{path}\\{folder}", $"{archivePath}\\{type}\\{folder}");
            }
        }

        // Given a filepath to a day, returns a list of student IDs.
        private List<String> getStudentIds(string filePath, string day) {
            List<string> list = new List<string>();

            foreach (var f in Directory.GetFiles($"{filePath}\\{day}")) {
                // Remove the admissions from the filename (ex: admissions-01830102)
                list.Add(Path.GetFileNameWithoutExtension(f).Split('-')[1]);
            }

            return list;
        }

        // Log function that just writes to log string, implemented for readability.
        private void log(string message) {
            logOutput += message + "\n";
        }

        // Combines two letters into one result file in the Output folder. 
        public void CombineTwoLetters(string inputFile1, string inputFile2, string resultFile) {
            File.WriteAllText(resultFile, File.ReadAllText(inputFile1) + "\n\n" + File.ReadAllText(inputFile2));
        }
    }
}