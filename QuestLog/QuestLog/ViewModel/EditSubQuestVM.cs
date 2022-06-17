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
        private SubQuest SubQuest { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        public string Description
        {
            get { return SubQuest.Description; }
            set
            {
                SubQuest.Description = value;
                OnPropertyChanged("Description");
            }
        }

        public bool Finish
        {
            get { return SubQuest.Finish; }
            set
            {
                SubQuest.Finish = value;
                OnPropertyChanged("Finish");
            }
        }

        public string Name
        {
            get { return SubQuest.Name; }
            set
            {
                SubQuest.Name = value;
                OnPropertyChanged("Name");
            }
        }

        public int OrderNumber
        {
            get { return SubQuest.OrderNumber; }
            set
            {
                SubQuest.OrderNumber = value;
                OnPropertyChanged("OrderNumber");
            }
        }

        public int SQID
        {
            get { return SubQuest.SQID; }
            set
            {
                SubQuest.SQID = value;
                OnPropertyChanged("SQID");
            }
        }

        public EditSubQuestVM()
        {
            SubQuest = Data.Quests[Data.QuestSelectedIndex].Subquests[Data.SubQuestSelectedIndex];
        }

        public void VerifyChanges()
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
