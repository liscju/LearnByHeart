using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearnByHeart
{
    /// <summary>
    /// Exercise for specified bag of questions.
    /// </summary>
    public class Exercise
    {
        public List<Question> Answered { get; }
        public List<Question> Unanswered { get; }
        private DateTime begin;
        private Random random;

        /// <summary>
        /// Initializes exercise with specified questions.
        /// </summary>
        /// <param name="questions">Questions to be exercised</param>
        public Exercise(List<Question> questions, DateTime begin)
        {
            this.Unanswered = questions;
            this.Answered = new List<Question>();
            this.begin = begin;

            this.random = new Random();
        }

        /// <summary>
        /// Chooses next question for exercise.
        /// </summary>
        /// <returns>
        ///     Next question or null when all question are answered
        /// </returns>
        public Question NextQuestion()
        {
            if (!Unanswered.Any())
                return null;
            int index = random.Next(Unanswered.Count);
            return Unanswered[index];
        }

        /// <summary>
        ///     Indicates if the exercise has ended (all questions is answered).
        /// </summary>
        /// <returns>
        ///     True when exercise has ended, false otherwise.
        /// </returns>
        public bool HasEnded()
        {
            return !Unanswered.Any();
        }

        /// <summary>
        /// Calculates exercise statistics.
        /// </summary>
        /// <param name="now">current time</param>
        /// <returns>exercise statistics</returns>
        public ExerciseStatistics CalculateStatistics()
        {
            int answered = Answered.Count;
            int all = Unanswered.Count + Answered.Count;
            return new ExerciseStatistics(answered, all);
        }

        /// <summary>
        ///     Tries to answer specified question with given answer
        /// </summary>
        /// <param name="question">Question to answer</param>
        /// <param name="answer">Answer to question</param>
        /// <returns>
        ///     True when answer is correct, false otherwise.
        /// </returns>
        /// <exception cref="System.ArgumentException">
        ///     Thrown when question is not part of exercise.
        /// </exception>
        public bool Answer(Question question, string answer)
        {
            if (!Unanswered.Contains(question))
                throw new ArgumentException(
                    string.Format("Question {0} is not part of exercise", question.Content));
            if (!question.IsCorrect(answer))
            {
                Question head = Unanswered.First();
                Unanswered.RemoveAt(0);
                Unanswered.Add(head);
                return false;
            }
            Answered.Add(question);
            Unanswered.Remove(question);
            return true;
        }
    }

    /// <summary>
    /// Statistics of exercise.
    /// </summary>
    public class ExerciseStatistics
    {
        public int AnsweredCount { get; }
        public int AllCount { get; }

        /// <summary>
        /// Constructs exercise statistics from specified values.
        /// </summary>
        /// <param name="answeredCount">number of answered questions</param>
        /// <param name="allCount">number of all questions</param>
        public ExerciseStatistics(int answeredCount, int allCount)
        {
            this.AnsweredCount = answeredCount;
            this.AllCount = allCount;
        }
    }
}
