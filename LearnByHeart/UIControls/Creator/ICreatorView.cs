namespace LearnByHeart
{
    public interface ICreatorView
    {

        /// <summary>
        /// Gets current question.
        /// </summary>
        Question GetCurrentQuestion();

        /// <summary>
        /// Shows error with specified message.
        /// </summary>
        /// <param name="message">error message</param>
        void ShowError(string message);

        /// <summary>
        /// Shows question with specified index and count of all questions.
        /// </summary>
        /// <param name="idx">question index</param>
        /// <param name="question">question to show</param>
        /// <param name="count">number of questions</param>
        void ShowQuestion(int idx, Question question, int count);

        /// <summary>
        /// Switches to main window of application.
        /// </summary>
        void SwitchToMainWindow();

        /// <summary>
        /// Asks user whether question set is to be saved and switch to main window.
        /// </summary>
        void SaveAndSwitchToMainWindow();

        /// <summary>
        /// Asks user where to save file.
        /// </summary>
        /// <returns>path or null when user cancel saving file</returns>
        string ChoosePathToSaveFile();

        /// <summary>
        /// Notifies that the file was saved succesfully.
        /// </summary>
        /// <param name="path">path to saved file</param>
        void ShowFileSavedSuccesfully(string path);

        /// <summary>
        /// Show file is dirty (difference between question set on view and saved in file).
        /// </summary>
        /// <param name="path">path to file</param>
        void ShowFileIsDirty(string path);
    }
}