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

            using (WebClient wc = new WebClient())
            {
                json = wc.DownloadString("https://mdvca3qr4u.eu-west-1.awsapprunner.com/quests/select/sid/" + SID);
            }

            List<Quest> quests = JsonConvert.DeserializeObject<List<Quest>>(json);

            return quests;
        }

        public static void FinishQuest(int QID)
        {
            //Patch request here
        }

        public static void FinishSubQuest(int SQID)
        {
            //Patch request here
        }
    }
}
