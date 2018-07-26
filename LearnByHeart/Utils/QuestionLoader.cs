using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace LearnByHeart.Utils
{
    /// <summary>
    /// Loads/Saves questions from/to specified file.
    /// </summary>
    public class QuestionLoader
    {

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
            try
            {
                xml = File.ReadAllText(path);
            }
            catch (Exception ex)
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
                Question invalid = questions.Find(q => !IsValid(q));
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

        /// <summary>
        /// Checks if question is valid (contains all needed data).
        /// </summary>
        /// <param name="question">question to check validity</param>
        /// <returns>true when question is valid, false otherwise.</returns>
        private static bool IsValid(Question question)
        {
            return !String.IsNullOrWhiteSpace(question.Content)
                && !String.IsNullOrWhiteSpace(question.Answer);
        }

        /// <summary>
        /// Saves questions to specified file.
        /// Note that file content is overwritten by new content.
        /// </summary>
        /// <param name="questions">question to save</param>
        /// <exception cref="System.ArgumentException">
        ///     When one of question is invalid (does not contain
        ///     needed data)
        /// </exception>
        /// <exception cref="System.IO.IOException">
        ///     Thrown when there is problem with saving file.
        /// </exception>
        public static void Save(String path, List<Question> questions)
        {
            Question invalid = questions.Find(q => !IsValid(q));
            if (invalid != null)
                throw new ArgumentException(
                    String.Format(
                        "Question '{0}' does not contain all needed data"
                        + "(content and answer)"
                        , invalid.ToString()
                    )
                );

            XmlSerializer serializer = new XmlSerializer(
                typeof(List<Question>),
                new XmlRootAttribute("Questions")
            );

            System.IO.FileStream file = null;
            try
            {
                file = System.IO.File.Create(path);
                serializer.Serialize(file, questions);
                file.Close();
            }
            catch (Exception ex)
            {
                throw new IOException(
                    String.Format("Error while saving file: {0}", ex.Message)
                );
            }
            finally
            {
                if (file != null)
                    file.Dispose();
            }
        }

    }
}
