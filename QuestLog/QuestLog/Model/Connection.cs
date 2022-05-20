using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace QuestLog
{
    internal static class Connection
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

        public static void AddQuest(int SID, Quest q)
        {
            var client = new RestClient("https://mdvca3qr4u.eu-west-1.awsapprunner.com/quests/add/quest/" + SID);
            var request = new RestRequest();

            DBQuest dbQuest = new DBQuest(q.Copper, q.EXP, q.Gold, q.Name, q.Silver);

            string json = JsonConvert.SerializeObject(dbQuest);

            request.AddJsonBody(json);

            client.PostAsync(request).Wait();           
        }

        public static void RemoveQuest(int QID)
        {
            var client = new RestClient("https://mdvca3qr4u.eu-west-1.awsapprunner.com/quests/delete/quest/" + QID);
            var request = new RestRequest();

            client.DeleteAsync(request).Wait();
        }
    }
}
