﻿using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Threading;

namespace LearnByHeart.UIControls
{
    /// <summary>
    /// Interaction logic for MainControl.xaml
    /// </summary>
    public partial class MainUIControl : UserControl, IMainView
    {
        private MainController controller;

        public MainUIControl()
        {
            InitializeComponent();
            controller = new MainController(this);

            // Focusing on control inside UserControl let KeyDown event
            // work (e.g. escape key)
            UIUserControlUtil.SetFocus(OpenFile);
        }

        public void ShowError(string message)
        {
            MessageBox.Show(
                message,
                "Error",
                MessageBoxButton.OK,
                MessageBoxImage.Error
            );
        }

        public void NavigateToExercise(string path, Exercise exercise)
        {
            UIControlSwitcher.SwitchTo(new ExerciseUIControl(path, exercise));
        }

        private void OpenFileDialog_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.AddExtension = true;
            openFileDialog.Filter = "Question set|*.xml|Any|*.*";
            openFileDialog.DefaultExt = "xml";
            if (openFileDialog.ShowDialog() != true)
                return;

            controller.LoadExercise(openFileDialog.FileName);
        }

        private void CloseApp_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult decision = MessageBox.Show(
                "Do you really want to close application?",
                "LearnByHeart",
                MessageBoxButton.YesNo
            );

            if (decision == MessageBoxResult.Yes)
            {
                Application.Current.Shutdown();
            }
        }

        private void CreateFile_Click(object sender, RoutedEventArgs e)
        {
            UIControlSwitcher.SwitchTo(new CreatorUIControl());
        }

        private void Main_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
            {
                CloseApp.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));
            }
            else if (e.KeyboardDevice.Modifiers == ModifierKeys.Control
                        && e.Key == Key.N)
            {
                CreateFile.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));
            }
            else if (e.KeyboardDevice.Modifiers == ModifierKeys.Control
                        && e.Key == Key.E)
            {
                EditFile.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));
            }
            else if (e.KeyboardDevice.Modifiers == ModifierKeys.Control
                        && e.Key == Key.O)
            {
                OpenFile.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));
            }
        }

        private void EditFile_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.AddExtension = true;
            openFileDialog.Filter = "Question set|*.xml|Any|*.*";
            if (openFileDialog.ShowDialog() != true)
                return;

            controller.EditFile(openFileDialog.FileName);
        }

        public void NavigateToEdit(string path, List<Question> questions)
        {
            UIControlSwitcher.SwitchTo(new CreatorUIControl(path, questions));
        }
    }
}
