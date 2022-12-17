using System;

namespace Kronos.TestTask.Core
{
    public class Result
    {
        public int ID { get; set; }
        public string Gamename { get; set; }
        public Round Round { get; set; }
        public object Pool { get; set; }
        public Hmteam Hmteam { get; set; }
        public Awteam Awteam { get; set; }
        public int HmsCore { get; set; }
        public int AwsCore { get; set; }
        public DateTime Gamedate { get; set; }
        public int Location { get; set; }
        public int Competition { get; set; }
        public DateTime Gamedateend { get; set; }
        public Elimformatgame Elimformatgame { get; set; }
        public string Status { get; set; }
    }



}
