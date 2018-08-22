namespace LearnByHeart
{
    public interface ICreatorView
    {

        /// <summary>
        /// Triggers updating current question content in memory.
        /// </summary>
        void TriggerCurrentQuestionUpdate();

        /// <summary>
        /// Shows error with specified message.
        /// </summary>
        /// <param name="message">error message</param>
        void ShowError(string message);

    }
}