using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace QuestLog
{
    internal class Connection
    {
        public static List<Quest> GetQuestList(int SID)
        {
            string json = "";
            List<Quest> q = new List<Quest>();

            using (WebClient wc = new WebClient())
            {
                json = wc.DownloadString("http://172.16.47.13:5000/quests/select/sid/" + SID);
            }

            

            return q;
        }

        public static List<Quest> GetQuestListTEST(int SID)
        {
            List<Quest> quests = Enumerable.Range(1, 3)
            .Select(x => new Quest()
            {
                Name = "Quest " + x,
                OrderNumber = x,
                EXP = 100,
                Subquests = Enumerable.Range(1, 2)
                            .Select(y => new SubQuest()
                            {
                                Name = "SubQuest " + y,
                                Nr = y
                            }).ToList()
            }).ToList();

            string json = JsonConvert.SerializeObject(quests, Formatting.Indented);

            return quests;

        }
    }
}
