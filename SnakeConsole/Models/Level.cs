using System.Collections.Generic;

namespace SnakeConsole.Models
{
    [System.Serializable]
    public class Level
    {
        public int widht { get; set; } // M
        public int height { get; set; } // N
        public List<Point> wallPoints { get; set; } // стены
        public int howMuchFoodToEat { get; set; } // сколько еды надо съесть для перехода на следующий уровень
        public int howMuchFoodIsThere { get; set; } // сколько еды существует в любой момент времени
        public Point startPositionSnake { get; set; } // стартовая позция змейки

        public Level(int widht, int height, List<Point> wallPoints, int howMuchFoodToEat, int howMuchFoodIsThere, Point startPositionSnake)
        {
            this.widht = widht;
            this.height = height;
            this.wallPoints = wallPoints;
            this.howMuchFoodToEat = howMuchFoodToEat;
            this.howMuchFoodIsThere = howMuchFoodIsThere;
            this.startPositionSnake = startPositionSnake;
        }

        public Level() { }

    }
}
