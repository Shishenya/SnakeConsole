using SnakeConsole.Scripts;
using System;
using System.Collections.Generic;

namespace SnakeConsole.Models
{
    public class CurrentLevel
    {

        private Level _defaultLevel; // Уровень по умолчанию
        private Game _game;

        private int _currentAmountFoodToEat = 0; // сколько съедено еды
        private string[,] _viewLevels; // отображение уровня
        private List<Point> _foodsPoints = new List<Point>(); // точки еды на уровне
        private Snake _snake; // наша змея
        private MoveSnake _moveSnake; // контроллер движения змейки

        public Snake Snake { get => _snake; }
        public MoveSnake MoveSnake { get => _moveSnake; }
        public List<Point> FoodPoints { get => _foodsPoints; }
        public List<Point> WallPoints { get => _defaultLevel.wallPoints;  }
        public Game Game { get => _game; }
        public int Width { get => _defaultLevel.widht;  }
        public int Height { get => _defaultLevel.height;  }

        public event Action OnCompleteLevel; // эвент завершения уровня
        public event Action OnDeathSnake; // эвент смерти змейки



        public CurrentLevel(Level defaultLevel, Game game)
        {
            Console.Clear();
            
            _defaultLevel = defaultLevel;
            _game = game;
            _viewLevels = new string[_defaultLevel.height, _defaultLevel.widht]; // Создаем визуал

            // Создаем новую змейку
            _snake = new Snake(_defaultLevel.startPositionSnake);
            // Создаем ее контроллер
            _moveSnake = new MoveSnake(_snake, this);

            SetAllCellEmpty();
            SetWallPoints();
            SetSnakePoint();

            InitFood();

            DrawLevel();

            OnCompleteLevel += _game.LoadNextLevel;

        }


        /// <summary>
        /// Установка в массиве значений змеи
        /// </summary>
        public void SetSnakePoint()
        {
            int index = 0;
            foreach (Point point in _snake.snakePoints)
            {
                if (index == 0)
                {
                    _viewLevels[point.x, point.y] = "O";
                }
                else
                {
                    _viewLevels[point.x, point.y] = "o";
                }
                index++;
            }
        }

        /// <summary>
        /// Установка стен
        /// </summary>
        private void SetWallPoints()
        {
            foreach (Point point in _defaultLevel.wallPoints)
            {
                _viewLevels[point.x, point.y] = "#";
            }
        }

        /// <summary>
        /// Установка пустых ячеек
        /// </summary>
        private void SetAllCellEmpty()
        {
            for (int i = 0; i < _defaultLevel.height; i++)
            {
                for (int j = 0; j < _defaultLevel.widht; j++)
                {
                    _viewLevels[i, j] = ".";
                }

            }
        }

        /// <summary>
        /// Рисование стартового представления
        /// </summary>
        public void DrawLevel()
        {

            if (_currentAmountFoodToEat >= _defaultLevel.howMuchFoodToEat) return;
            Console.Clear();

            DrawInfoLevel();

            SetAllCellEmpty();
            SetWallPoints();
            SetSnakePoint();
            SetFoodPoint();

            for (int i = 0; i < _defaultLevel.height; i++)
            {
                for (int j = 0; j < _defaultLevel.widht; j++)
                {
                    Console.Write(_viewLevels[i, j]);
                }
                Console.WriteLine();
            }

        }

        /// <summary>
        /// Выводит информацию об уровне
        /// </summary>
        public void DrawInfoLevel()
        {
            Console.WriteLine($"Уровень: {_game.CurrentIndexlevelByDraw}; " + $"Жизней: {_game.Life}; " +
                              $"Съедено еды: {_currentAmountFoodToEat}; " +
                              $"Необходисо съесть еды: {_defaultLevel.howMuchFoodToEat}");
            Console.WriteLine("______________________________");
        }

        /// <summary>
        ///  Инициализация еды на уровне
        /// </summary>
        public void InitFood()
        {

            // Вообще "топорный" метод
            // Но в принципе все равно так или иначе почти весь массив надо перебирать
            // Либо идти по спискам "стен"+"змеи"+"еды" и потом случайно выбирать нужную клетку

            // Делаем пока на уровне не будет нужное количество еды
            while (_foodsPoints.Count < _defaultLevel.howMuchFoodIsThere)
            {
                bool isFoodCompleted = false; // Флаг, что еда положилась
                int maxIteration = _defaultLevel.widht * _defaultLevel.height * 5; // максимальное количество итераций
                int currentIteration = 0; // текущая итерация

                // делаем пока не поставим еду
                while (!isFoodCompleted)
                {
                    Random rnd = new Random();
                    int rndX = rnd.Next(0, _defaultLevel.height);
                    int rndY = rnd.Next(0, _defaultLevel.widht);
                    if (_viewLevels[rndX, rndY] == ".")
                    {
                        _foodsPoints.Add(new Point(rndX, rndY));
                        _viewLevels[rndX, rndY] = "s";
                        isFoodCompleted = true;
                    }

                    currentIteration++;
                    if (currentIteration > maxIteration) break;
                }
            }
        }

        /// <summary>
        /// Установка точек еды
        /// </summary>
        private void SetFoodPoint()
        {
            foreach (Point point in _foodsPoints)
            {
                _viewLevels[point.x, point.y] = "s";
            }
        }

        /// <summary>
        /// Удаление точки с едой
        /// </summary>
        public void DeleteFoodPoint(Point deletePoint)
        {
            foreach (Point point in _foodsPoints)
            {
                if (point.Equals(deletePoint))
                {
                    _foodsPoints.Remove(point);
                    break;
                }
            }
        }

        /// <summary>
        /// Увеличение количества еды, которых сьел игрок
        /// </summary>
        public void IncrementEatFood()
        {
            _currentAmountFoodToEat++;
            if (_currentAmountFoodToEat == _defaultLevel.howMuchFoodToEat)
            {
                OnCompleteLevel?.Invoke();
            }
        }

    }
}
