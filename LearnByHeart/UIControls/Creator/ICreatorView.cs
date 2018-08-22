namespace LearnByHeart
{
    public interface ICreatorView
    {

        /// <summary>
        /// Gets current question.
        /// </summary>
        Question getCurrentQuestion();

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

    }
}