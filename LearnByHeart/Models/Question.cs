using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;

namespace LearnByHeart
{
    /// <summary>
    /// Represents single question with answer.
    /// </summary>
    public class Question
    {
        public string Content { get; set; }
        public string Answer { get; set; }

        /// <summary>
        /// Used only for serialization/deserialization.
        /// </summary>
        private Question() { }

        /// <summary>
        /// Initializes question with specified content and answer.
        /// </summary>
        /// <param name="content">content of the question</param>
        /// <param name="answer">correct answer for question</param>
        public Question(string content, string answer)
        {
            this.Content = content;
            this.Answer = answer;
        }

        /// <summary>
        /// Indicates if given answer is correct for question.
        /// </summary>
        /// <param name="answer">answer to check</param>
        /// <returns>true when answer is correct, false otherwise.</returns>
        public bool IsCorrect(string answer)
        {
            return Answer.Equals(answer);
        }

        /// <inheritdoc/>
        public override string ToString()
        {
            return Content + " => " + Answer;
        }

    }
}