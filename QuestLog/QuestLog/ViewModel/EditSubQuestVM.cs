using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace QuestLog
{
    public class EditSubQuestVM : INotifyPropertyChanged
    {
        private SubQuest subQuest;

        public event PropertyChangedEventHandler PropertyChanged;

        public string Description
        {
            get { return subQuest.Description; }
            set
            {
                subQuest.Description = value;
                OnPropertyChanged("Description");
            }
        }

        public bool Finish
        {
            get { return subQuest.Finish; }
            set
            {
                subQuest.Finish = value;
                OnPropertyChanged("Finish");
            }
        }

        public string Name
        {
            get { return subQuest.Name; }
            set
            {
                subQuest.Name = value;
                OnPropertyChanged("Name");
            }
        }

        public int OrderNumber
        {
            get { return subQuest.OrderNumber; }
            set
            {
                subQuest.OrderNumber = value;
                OnPropertyChanged("OrderNumber");
            }
        }

        public int SQID
        {
            get { return subQuest.SQID; }
            set
            {
                subQuest.SQID = value;
                OnPropertyChanged("SQID");
            }
        }

        public EditSubQuestVM()
        {
            subQuest = Data.Quests[Data.QuestSelectedIndex].Subquests[Data.SubQuestSelectedIndex];
        }

        public void VerifyChanges(object sender, RoutedEventArgs e)
        {
            JObject update =
                new JObject(
                    new JProperty("Name", Name),
                    new JProperty("Description", Description));

            var json = update.ToString();
            Connection.EditSubQuest(SQID, json);
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
