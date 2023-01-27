using System.Collections.Generic;

namespace SnakeConsole.Models
{
    [System.Serializable]
    public class Levels
    {
        public List<Level> levelList { get; set; }

        public Levels() { levelList = new List<Level>();  }
    }
}
