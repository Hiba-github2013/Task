using System;

namespace Kronos.TestTask.businessLayer
{
    public class Result
    {
        public int id { get; set; }
        public string gamename { get; set; }
        public Round round { get; set; }
        public object pool { get; set; }
        public Hmteam hmteam { get; set; }
        public Awteam awteam { get; set; }
        public int hmscore { get; set; }
        public int awscore { get; set; }
        public DateTime gamedate { get; set; }
        public int location { get; set; }
        public int competition { get; set; }
        public DateTime gamedateend { get; set; }
        public Elimformatgame elimformatgame { get; set; }
        public string status { get; set; }
    }



}
