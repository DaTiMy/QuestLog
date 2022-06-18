using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace QuestLog
{
    internal class DMViewVM : INotifyPropertyChanged
    {
        public int SelectedQuestIndex
        {
            get { return Data.QuestSelectedIndex; }
            set
            {
                Data.QuestSelectedIndex = value;
                OnPropertyChanged("SelectedQuestIndex");
            }
        }

        public ObservableCollection<Quest> Quests
        {
            get { return Data.Quests; }
            set
            {
                Data.Quests = value;
                OnPropertyChanged("Quests");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public DMViewVM()
        {
            Data.Quests = Connection.GetQuestList(1);
        }

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
            Quest q = new Quest(2, 5, false, 15, "newQuestchen", -1, -1, 59, new ObservableCollection<SubQuest>());
            q = Connection.AddQuest(MainWindow.Instance.SID, q);
            Data.Quests.Add(q);
        }

        public void EditQuest(object sender, RoutedEventArgs e)
        {
            if (SelectedQuestIndex == -1)
                return;

            new EditQuest().ShowDialog();
        }

        public void RemoveQuest(object sender, RoutedEventArgs e)
        {
            if (SelectedQuestIndex == -1)
                return;

            Connection.RemoveQuest(Data.Quests[SelectedQuestIndex].QID);
            Data.Quests.RemoveAt(SelectedQuestIndex);
        }

        private void OnPropertyChanged(string s)
        {
            if (PropertyChanged != null)
            {
                var e = new PropertyChangedEventArgs(s);
                PropertyChanged(this, e);
            }
        }

        ////Refresh for ListView after adding or removing items
        //public void Refresh()
        //{
        //    DataContext = Data.Quests;
        //    ICollectionView view = CollectionViewSource.GetDefaultView(Data.Quests);
        //    view.Refresh();
        //}

        //public void QuestRefresh()
        //{
        //    Data.Quests = Connection.GetQuestList(MainWindow.Instance.SID);
        //    Refresh();
        //}
    }
}
