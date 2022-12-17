using System.Collections.Generic;

namespace Kronos.TestTask.Core
{

    public class Game
    {
        public int Count { get; set; }
        public object Next { get; set; }
        public object Previous { get; set; }
        public List<Result> Results { get; set; }
    }



}
