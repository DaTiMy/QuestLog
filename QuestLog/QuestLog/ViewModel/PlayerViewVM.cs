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
    internal class PlayerViewVM : INotifyPropertyChanged
    {
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

        public PlayerViewVM()
        {
            //insert sid here, passed from session
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

        private void OnPropertyChanged(string s)
        {
            if (PropertyChanged != null)
            {
                var e = new PropertyChangedEventArgs(s);
                PropertyChanged(this, e);
            }
        }
    }
}
