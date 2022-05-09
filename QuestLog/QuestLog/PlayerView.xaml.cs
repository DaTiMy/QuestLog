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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace QuestLog
{
    /// <summary>
    /// Interaction logic for PlayerView.xaml
    /// </summary>
    public partial class PlayerView : UserControl
    {
        public List<Quest> Quests { get; set; }


        public PlayerView()
        {
            InitializeComponent();

            Quests = Connection.GetQuestList(1);

            DataContext = Quests;
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
            MainWindow.Instance.DragMove();
        }
        #endregion

        public void CheckedQuest(object sender, RoutedEventArgs e)
        {
            int qid = ((sender as CheckBox).DataContext as Quest).QID;
            object header = (((sender as CheckBox).Parent as StackPanel).Children[0] as TreeViewItem).Header;

            if((sender as CheckBox).IsChecked == true)
            {
                TextBlock text = new TextBlock();
                text.Text = (header as TextBlock).Text;
                text.TextDecorations = TextDecorations.Strikethrough;
                (((sender as CheckBox).Parent as StackPanel).Children[0] as TreeViewItem).Header = text;
            }
            else
            {
                TextBlock text = new TextBlock();
                text.Text = (header as TextBlock).Text;
                text.TextDecorations = null;
                (((sender as CheckBox).Parent as StackPanel).Children[0] as TreeViewItem).Header = text;
            }


            Connection.FinishQuest(qid);
        }

        public void CheckedSubQuest(object sender, RoutedEventArgs e)
        {
            int sqid = ((sender as CheckBox).DataContext as SubQuest).SQID;
            Connection.FinishSubQuest(sqid);
        }
    }
}
