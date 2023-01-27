using SnakeConsole.Models;
using System;

namespace SnakeConsole.Scripts
{
    public class MoveSnake
    {

        private Snake _snake;
        private CurrentLevel _currentLevel;

        public event Action OnMoveEvent; // Эвент хода
        public event Action OnEatFoodEvent; // Эвент съеденной еды
        public event Action OnMoveToWallEvent; // Эвент столкновения со стеной 
        public event Action OnMoveToSnakeEvent; // эвент столкновения со змейкой

        public MoveSnake(Snake snake, CurrentLevel currentLevel)
        {
            _snake = snake;
            _currentLevel = currentLevel;

            OnMoveEvent += _currentLevel.Game.IncrementMoveAmout;

            OnEatFoodEvent += _currentLevel.Game.IncrementEatAmount;
            OnEatFoodEvent += _currentLevel.IncrementEatFood;

            OnMoveToWallEvent += _currentLevel.Game.DecrementLife;
            OnMoveToWallEvent += _currentLevel.Game.IncrementDeath;
            OnMoveToWallEvent += _currentLevel.Game.RestrartLevel;

            OnMoveToSnakeEvent+= _currentLevel.Game.DecrementLife;
            OnMoveToSnakeEvent += _currentLevel.Game.IncrementDeath;
            OnMoveToSnakeEvent+= _currentLevel.Game.RestrartLevel;
        }

        public void MoveRight()
        {
            Point newPoint = new Point(_snake.snakePoints[0].x, _snake.snakePoints[0].y + 1);
            Move(newPoint);
        }

        public void MoveLeft()
        {
            Point newPoint = new Point(_snake.snakePoints[0].x, _snake.snakePoints[0].y - 1);
            Move(newPoint);
        }

        public void MoveUp()
        {
            Point newPoint = new Point(_snake.snakePoints[0].x - 1, _snake.snakePoints[0].y);
            Move(newPoint);
        }

        public void MoveDown()
        {
            Point newPoint = new Point(_snake.snakePoints[0].x + 1, _snake.snakePoints[0].y);
            Move(newPoint);
        }

        /// <summary>
        /// Возвращает true, если новая точка была едой
        /// </summary>
        private bool MovePointIsFood(Point newPoint)
        {
            foreach (Point point in _currentLevel.FoodPoints)
            {
                if (newPoint == point) return true;
            }
            return false;
        }

        /// <summary>
        /// Возвращает True, если новая клетка была стена
        /// </summary>
        private bool MovePointIsWall(Point newPoint)
        {
            foreach (Point point in _currentLevel.WallPoints)
            {
                if (point == newPoint) return true;
            }
            return false;
        }

        private bool MovePointIsSnake(Point newPoint)
        {
            foreach (Point point in _snake.snakePoints)
            {
                if (point == newPoint) return true;
            }
            return false;
        }

        /// <summary>
        /// Движение змейки
        /// </summary>
        private void Move(Point newPoint)
        {
            // Проверяем границы
            if (newPoint.y >= _currentLevel.Width || newPoint.y < 0) { return; }
            if (newPoint.x >= _currentLevel.Height || newPoint.x < 0) { return; }

            // перемещение на клетку с едой
            if (MovePointIsFood(newPoint))
            {
                _snake.RebuildSnakePoint(newPoint, true);
                _currentLevel.DeleteFoodPoint(newPoint);
                _currentLevel.InitFood();

                OnMoveEvent?.Invoke();
                OnEatFoodEvent?.Invoke();

                _currentLevel.DrawLevel();
            }
            else
            {
                // клетка была не еда, поверяем, была ли она стеной
                if (MovePointIsWall(newPoint))
                {
                    OnMoveToWallEvent?.Invoke();
                }
                else
                {
                    // Проверяем, столновение с самой змейкой
                    if (MovePointIsSnake(newPoint))
                    {
                        OnMoveToSnakeEvent?.Invoke();
                    }
                    else
                    {
                        _snake.RebuildSnakePoint(newPoint);
                        OnMoveEvent?.Invoke();
                        _currentLevel.DrawLevel();
                    }

                }
            }
        }

    }
}
