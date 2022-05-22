﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
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
    public partial class DMView : UserControl
    {
        public List<Quest> Quests { get; set; }

        public DMView()
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
            Connection.FinishQuest(qid);
        }

        public void CheckedSubQuest(object sender, RoutedEventArgs e)
        {
            int sqid = ((sender as CheckBox).DataContext as SubQuest).SQID;
            Connection.FinishSubQuest(sqid);
        }

        public void AddQuest(object sender, RoutedEventArgs e)
        {
            Quest q = new Quest(2, 5, false, 15, "newQuestchen", -1, -1, 59, new List<SubQuest>());
            Quests.Add(q);
            //Datenbankanbindung
            Connection.AddQuest(MainWindow.Instance.SID, q);
            Refresh();
        }

        public void EditQuest(object sender, RoutedEventArgs e)
        {
            int index = QuestView.SelectedIndex;

            if (index == -1)
                return;

            var editaddWindow = new EditAddQuest();
            editaddWindow.ShowDialog();
        }

        public void RemoveQuest(object sender, RoutedEventArgs e)
        {
            int index = QuestView.SelectedIndex;

            if (index == -1)
                return;

            Connection.RemoveQuest(Quests[index].QID);
            Quests.RemoveAt(index);
            Refresh();
        }

        //Refresh for ListView after adding or removing items
        public void Refresh()
        {
            DataContext = Quests;
            ICollectionView view = CollectionViewSource.GetDefaultView(Quests);
            view.Refresh();
        }
    }
}