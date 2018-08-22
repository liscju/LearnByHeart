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
            string xml = ReadAllText(path);
            List<Question> questions = Deserialize(xml);

            Question invalid = questions.Find(q => !IsValid(q));
            if (invalid != null)
                throw new FileFormatException(
                    string.Format(
                        "Question {0} does not contain all " +
                        "needed data (content and answer)",
                        questions.IndexOf(invalid) + 1
                    )
                );
            return questions;
        }

        /// <summary>
        /// Deserializes list of questions from xml file content.
        /// </summary>
        /// <param name="xml">xml file content</param>
        /// <returns>deserialized list of questions</returns>
        /// <exception cref="FileFormatException">
        ///     Thrown when xml has invalid format.
        /// </exception>
        private static List<Question> Deserialize(string xml)
        {
            List<Question> questions = null;
            try
            {
                StringReader reader = new StringReader(xml);
                XmlSerializer serializer = new XmlSerializer(
                    typeof(List<Question>),
                    new XmlRootAttribute("Questions")
                );
                questions = (List<Question>)serializer.Deserialize(reader);
            }
            catch (InvalidOperationException ex)
            {
                throw new FileFormatException(
                    string.Format("File format exception: {0}", ex.Message),
                    ex
                );
            }

            return questions;
        }

        /// <summary>
        /// Reads file content.
        /// </summary>
        /// <param name="path">path to file</param>
        /// <returns>path content</returns>
        /// <exception cref="IOException">
        ///     Thrown when problem with reading file
        /// </exception>
        private static string ReadAllText(string path)
        {
            try
            {
                return File.ReadAllText(path);
            }
            catch (Exception ex)
            {
                throw new IOException(
                    string.Format("Problem with reading file: {0}", ex.Message, ex)
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
                        "Question {0} does not contain all needed data"
                        + "(content and answer)"
                        , questions.IndexOf(invalid) + 1
                    )
                );

            Serialize(path, questions);
        }

        /// <summary>
        /// Serializes questions to the file.
        /// </summary>
        /// <param name="path">path of the file</param>
        /// <param name="questions">questions to serialize</param>
        /// <exception cref="IOException">
        ///     Thrown when problem with saving file.
        /// </exception>
        private static void Serialize(string path, List<Question> questions)
        {
            System.IO.FileStream file = null;
            try
            {
                file = System.IO.File.Create(path);
                XmlSerializer serializer = new XmlSerializer(
                    typeof(List<Question>),
                    new XmlRootAttribute("Questions")
                );
                serializer.Serialize(file, questions);
                file.Close();
            }
            catch (Exception ex)
            {
                throw new IOException(ex.Message);
            }
            finally
            {
                if (file != null)
                    file.Dispose();
            }
        }

    }
}
