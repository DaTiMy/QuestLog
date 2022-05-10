using Newtonsoft.Json;
using RestSharp;
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
            var client = new RestClient("https://mdvca3qr4u.eu-west-1.awsapprunner.com/quests/finish/quest/" + QID);
            var request = new RestRequest();
            client.PatchAsync(request).Wait();  
        }

        public static void FinishSubQuest(int SQID)
        {
            var client = new RestClient("https://mdvca3qr4u.eu-west-1.awsapprunner.com/quests/finish/subquest/" + SQID);
            var request = new RestRequest();
            client.PatchAsync(request).Wait();
        }
    }
}
