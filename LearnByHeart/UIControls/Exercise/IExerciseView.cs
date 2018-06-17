namespace LearnByHeart.UIControls
{
    public interface IExerciseView
    {
        /// <summary>
        /// Returns to main window.
        /// </summary>
        void BackToMain();

        /// <summary>
        /// Shows specified question.
        /// </summary>
        /// <param name="question">question to show</param>
        void ShowQuestion(Question question);

        /// <summary>
        /// Shows exercise statistics.
        /// </summary>
        /// <param name="statistics">exercise statistics</param>
        void ShowStatistics(ExerciseStatistics statistics);

        /// <summary>
        /// Shows that exercise has ended.
        /// </summary>
        void ShowExerciseHasEnded();

        /// <summary>
        /// Shows that answer to question was correct.
        /// </summary>
        void ShowAnswerIsCorrect();

        /// <summary>
        /// Shows that answer to question was incorrect.
        /// </summary>
        /// <param name="correctAnswer">correct answer</param>
        void ShowAnswerIsIncorrect(string correctAnswer);
    }
}