**How to run:**

1. Clone or download the repository.
2. Create test letters, or use the ones that already exist to test run the application.
3. Run the application executable in the same directory as the CombinedLetters folder. If it does not detect the CombinedLetters folder it will exit.
4. The application will process the folders and move them all to Archive after it is done.

**Expectations:**

1. Estimated time: 1-2 hrs.
2. Documented time: 3 hrs.

**Details and explanations.**

The code has a main (Program.cs) file that calls the LetterService in case in the future the program needs to do anything else. The service runs under the assumption below that once folders are processed they are moved to the archive, thus it can handle any amount of days not ran or if it is ran at an off time. There are no unit tests, but all actions are logged to a text file that is produced in the Output folder. 

**Comments**

- Assumptions
    - I imagine the CombinedLetters folder wouldn't reside within the application but for simplicity I put it inside the project folder. 
    - I assumed that archiving the files implies moving the day's folder to archive from both Admission and Scholarship, thus when the service runs only the days needing to be processed will be in the respective folders.
    - I would assume that once two files were combined that the individual one should be deleted so they are not processed (mailed) individually, but I did not implement that for simplicity. 