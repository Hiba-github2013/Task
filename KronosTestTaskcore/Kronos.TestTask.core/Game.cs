using System.Collections.Generic;

namespace Kronos.TestTask.businessLayer
{

    public class Game
    {
        public int count { get; set; }
        public object next { get; set; }
        public object previous { get; set; }
        public List<Result> results { get; set; }
    }



}
