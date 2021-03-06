﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearnByHeart
{
    /// <summary>
    /// Main Window View interface.
    /// </summary>
    public interface IMainView
    {
        /// <summary>
        /// Show error with specified message.
        /// </summary>
        /// <param name="message">error message</param>
        void ShowError(string message);

        /// <summary>
        /// Navigates to exercise window.
        /// </summary>
        /// <param name="path">path to exercise</param>
        /// <param name="questions">exercise</param>
        void NavigateToExercise(string path, Exercise exercise);

        /// <summary>
        /// Navigates to edit window.
        /// </summary>
        /// <param name="path">path to file</param>
        /// <param name="questions">questions</param>
        void NavigateToEdit(string path, List<Question> questions);
    }
}
