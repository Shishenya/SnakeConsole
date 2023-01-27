using System.Collections.Generic;

namespace SnakeConsole.Models
{
    public  class Snake
    {

        public List<Point> snakePoints = new List<Point>();

        public Snake(Point startPoint)
        {
            snakePoints.Add(startPoint);
        }

        /// <summary>
        /// пересобирает список позиций змейки с учетом ее новой точки движения
        /// Параметр isEat означает, кушала ли змейка еду
        /// </summary>
        public void RebuildSnakePoint(Point newPoint, bool isEat = false)
        {
            if (!isEat)
            {
                snakePoints.RemoveAt(snakePoints.Count - 1);
            }
            snakePoints.Insert(0, newPoint);
        }

    }
}
