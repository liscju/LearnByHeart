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

        /// <summary>
        /// Checks if question is valid (contains all needed data).
        /// </summary>
        /// <returns>true when question is valid, false otherwise.</returns>
        private bool IsValid()
        {
            return Content != null && Answer != null;
        }

        /// <summary>
        /// Loads questions from file.
        /// </summary>
        /// <param name="path">Path to questions.</param>
        /// <returns>List of equations</returns>
        /// <exception cref="System.IO.IOException">
        ///     Thrown when there is problem with reading file.
        /// </exception>
        /// <exception cref="System.IO.FileFormatException">
        ///     Thrown when file format is malformed.
        /// </exception>
        public static List<Question> Load(string path)
        {
            XmlSerializer serializer = new XmlSerializer(
                typeof(List<Question>),
                new XmlRootAttribute("Questions")
            );
            string xml;
            try {
                xml = File.ReadAllText(path);
            } catch (Exception ex)
            {
                throw new IOException(
                    string.Format("Problem with reading file: {0}", ex.Message, ex)
                );
            }
            try
            {
                StringReader reader = new StringReader(xml);
                List<Question> questions =
                    (List<Question>)serializer.Deserialize(reader);
                Question invalid = questions.Find(q => !q.IsValid());
                if (invalid != null)
                    throw new FileFormatException(
                        string.Format(
                            "Question {0} does not contain all " +
                            "needed data (content and answer)",
                            questions.IndexOf(invalid)
                        )
                    );
                return questions;
            }
            catch (InvalidOperationException ex)
            {
                throw new FileFormatException(
                    string.Format("File format exception: {0}", ex.Message),
                    ex
                );
            }
        }
    }
}