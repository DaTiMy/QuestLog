using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuestLog.Model
{   
    [Serializable]

    public class Quest
    {
        [JsonProperty("copper")]
        public int Copper { get; set; }

        [JsonProperty("exp")]
        public int EXP { get; set; }

        [JsonProperty("finish")]
        public bool Finish { get; set; }

        [JsonProperty("gold")]
        public int Gold { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("ordernumber")]
        public int OrderNumber { get; set; }

        [JsonProperty("qid")]
        public int QID { get; set; }

        [JsonProperty("silver")]
        public int Silver { get; set; }

        [JsonProperty("subquests")]
        public List<SubQuest> Subquests { get; set; }

        public Quest(int copper, int exp, bool finish, int gold, string name, int orderNumber, int qid, int silver, List<SubQuest> subquests)
        {
            Copper = copper;
            EXP = exp;
            Finish = finish;
            Gold = gold;
            Name = name;
            OrderNumber = orderNumber;
            QID = qid;
            Silver = silver;
            Subquests = subquests;
        }
    }
}
