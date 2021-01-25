﻿using GalaSoft.MvvmLight.Messaging;
using MicrosoftToDo.WPF.Models;
using MicrosoftToDo.WPF.ViewModel;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;

namespace MicrosoftToDo.WPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            this.MouseDown += (sender, e) =>
            {
                if (e.LeftButton == MouseButtonState.Pressed)
                    this.DragMove();
            };
            Messenger.Default.Register<TaskInfo>(this, "Expand", ExpandColumn);
            this.DataContext = new MainViewModel();
        }

        private void TextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                string inputValue = inputText.Text;
                if (inputValue == "") return;

                var vm = this.DataContext as MainViewModel;
                vm.AddTaskInfo(inputValue);
                inputText.Text = string.Empty;
            }
        }

        private void ExpandColumn(TaskInfo task)
        {
            var cdf = grc.ColumnDefinitions;
            if (cdf[1].Width == new GridLength(0))
            {
                cdf[1].Width = new GridLength(280);
                btnmin.Foreground = new SolidColorBrush(Colors.Black);
                btnmax.Foreground = new SolidColorBrush(Colors.Black);
                btnclose.Foreground = new SolidColorBrush(Colors.Black);
            }
            else
            {
                cdf[1].Width = new GridLength(0);
                btnmin.Foreground = new SolidColorBrush(Colors.White);
                btnmax.Foreground = new SolidColorBrush(Colors.White);
                btnclose.Foreground = new SolidColorBrush(Colors.White);
            }


        }


        private void btnminclick(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        private void btnmaxclick(object sender, RoutedEventArgs e)
        {
            if (this.WindowState == WindowState.Maximized)
                this.WindowState = WindowState.Normal;
            else
                this.WindowState = WindowState.Maximized;
        }

        private void btncloseclick(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
