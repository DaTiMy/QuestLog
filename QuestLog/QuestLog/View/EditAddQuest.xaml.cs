﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace QuestLog
{
    /// <summary>
    /// Interaction logic for EditAddQuest.xaml
    /// </summary>
    public partial class EditAddQuest : Window
    {
        public EditAddQuest()
        {
            InitializeComponent();
        }

        #region Toolbar functionality
        public void ExitApplication(object sender, RoutedEventArgs e)
        {
            Application.Current.MainWindow.Close();
        }
        public void MaximizeApplication(object sender, RoutedEventArgs e)
        {
            if (Application.Current.MainWindow.WindowState == WindowState.Normal)
                Application.Current.MainWindow.WindowState = WindowState.Maximized;
            else
                Application.Current.MainWindow.WindowState = WindowState.Normal;
        }
        public void MinimizeApplication(object sender, RoutedEventArgs e)
        {
            Application.Current.MainWindow.WindowState = WindowState.Minimized;
        }

        public void Drag(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }
        #endregion
    }
}