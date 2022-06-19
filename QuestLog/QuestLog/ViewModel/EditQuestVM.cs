using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;

namespace QuestLog
{
    public class EditQuestVM : INotifyPropertyChanged
    {
        //Model
        private Quest LoadedQuest { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        public int SubQuestSelectedIndex
        {
            get { return Data.SubQuestSelectedIndex; }
            set
            {
                Data.SubQuestSelectedIndex = value;
                OnPropertyChanged("SubQuestSelectedIndex");
            }
        }

        public int Copper
        {
            get { return LoadedQuest.Copper; }
            set
            {
                LoadedQuest.Copper = value;
                OnPropertyChanged("Copper");
            }
        }

        public int EXP
        {
            get { return LoadedQuest.EXP; }
            set
            {
                LoadedQuest.EXP = value;
                OnPropertyChanged("EXP");
            }
        }

        public bool Finish
        {
            get { return LoadedQuest.Finish; }
            set
            {
                LoadedQuest.Finish = value;
                OnPropertyChanged("Finish");
            }
        }

        public int Gold
        {
            get { return LoadedQuest.Gold; }
            set
            {
                LoadedQuest.Gold = value;
                OnPropertyChanged("Gold");
            }
        }

        public string Name
        {
            get { return LoadedQuest.Name; }
            set
            {
                LoadedQuest.Name = value;
                OnPropertyChanged("Name");
            }
        }

        public int OrderNumber
        {
            get { return LoadedQuest.OrderNumber; }
            set
            {
                LoadedQuest.OrderNumber = value;
                OnPropertyChanged("OrderNumber");
            }
        }

        public int QID
        {
            get { return LoadedQuest.QID; }
            set
            {
                LoadedQuest.QID = value;
                OnPropertyChanged("QID");
            }
        }

        public int Silver
        {
            get { return LoadedQuest.Silver; }
            set
            {
                LoadedQuest.Silver = value;
                OnPropertyChanged("Silver");
            }
        }

        public ObservableCollection<SubQuest> Subquests
        {
            get { return LoadedQuest.Subquests; }
            set
            {
                LoadedQuest.Subquests = value;
                OnPropertyChanged("Subquests");
            }
        }

        public EditQuestVM()
        {
            LoadedQuest = Data.Quests[Data.QuestSelectedIndex];

            if (Subquests == null)
                Subquests = new ObservableCollection<SubQuest>();
        }

        public void VerifyChanges(object sender, RoutedEventArgs e)
        {
            JObject update =
                new JObject(
                    new JProperty("Copper", Copper),
                    new JProperty("EXP", EXP),
                    new JProperty("Gold", Gold),
                    new JProperty("Name", Name),
                    new JProperty("Silver", Silver)
                    );

            var json = update.ToString();
            Connection.EditQuest(QID, json);
        }

        public void AddSubQuest(object sender, RoutedEventArgs e)
        {
            SubQuest sq = new SubQuest("new Description", false, "new Subquest", -1);

            sq = Connection.AddSubQuest(QID, sq);
            Subquests.Add(sq);
        }

        public void EditSubQuest(object sender, RoutedEventArgs e)
        {
            if (Data.SubQuestSelectedIndex == -1)
                return;

            new EditSubQuest().ShowDialog();
            LoadedQuest.SubQuestRefresh();
            //Server response here | Subquests[Data.SubQuestSelectedIndex] = sq;
        }

        public void RemoveSubQuest(object sender, RoutedEventArgs e)
        {
            if (Data.SubQuestSelectedIndex == -1)
                return;

            Connection.RemoveSubQuest(Subquests[Data.SubQuestSelectedIndex].SQID);
            Subquests.RemoveAt(Data.SubQuestSelectedIndex);

            if (Subquests.Count == 0)
                Subquests = new ObservableCollection<SubQuest>();
            //Refresh();
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
