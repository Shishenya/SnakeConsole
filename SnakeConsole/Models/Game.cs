using System;
using System.Collections.Generic;

namespace SnakeConsole.Models
{
    public class Game
    {

        private int _life = 3; // жизней
        private int _currentLevelIndex = 0; // текущий уровень
        private int _currentFoodEat = 0; // количество съеденной еды
        private int _currentMoveTurn = 0; // сделано ходов
        private int _currentDeath = 0;
        private GameState _gameState;

        private List<Level> _levels = new List<Level>(); // уровни игры
        private CurrentLevel _currentLevel;

        public CurrentLevel currentLevel { get => _currentLevel; }
        public int CurrentIndexlevelByDraw { get => _currentLevelIndex + 1; }
        public int Life { get => _life; }

        public Game(List<Level> levels)
        {

            _levels = levels; // загружаем уровни
            _gameState = GameState.game; 

            // Делаем рестарт игры
            RestartGame();

            ReadkeyMoveSnake();

        }

        /// <summary>
        /// перезапуск игры
        /// </summary>
        public void RestartGame()
        {
            StartGameParameters();

            if (_levels.Count > 0)
            {
                _currentLevel = new CurrentLevel(_levels[_currentLevelIndex], this);
            }
            else
            {
                Console.WriteLine("Error! No load level!");
            }

        }

        /// <summary>
        /// перезапуск уровня
        /// </summary>
        public void RestrartLevel()
        {
            _currentLevel = new CurrentLevel(_levels[_currentLevelIndex], this);
        }

        /// <summary>
        /// Установка значений для рестарта/новой игры (без сброса счетчиков)
        /// </summary>
        private void StartGameParameters()
        {
            _life = 3;
            _currentLevelIndex = 0;
        }

        /// <summary>
        /// загрузка следующего уровня
        /// </summary>
        public void LoadNextLevel()
        {
            _currentLevelIndex++;
            Console.Clear();
            if (_currentLevelIndex < _levels.Count)
            {
                _currentLevel = new CurrentLevel(_levels[_currentLevelIndex], this);
            }
            else
            {
                _gameState = GameState.gameWin;
                ShowMessageWin();
            }
        }

        /// <summary>
        /// Метод вывода сообщения о выиграше со статистикой
        /// </summary>
        private void ShowMessageWin()
        {
            Console.WriteLine("Вы выиграли!");
            Console.WriteLine($"Сделано ходов: {_currentMoveTurn} \n" +
                              $"Съедено еды: {_currentFoodEat} \n" +
                              $"Умер {_currentDeath} раз(а)");
        }

        /// <summary>
        /// Увеличение количества ходов
        /// </summary>
        public void IncrementMoveAmout()
        {
            _currentMoveTurn++;
        }

        /// <summary>
        /// Увеличение съеденной еды
        /// </summary>
        public void IncrementEatAmount()
        {
            _currentFoodEat++;
        }

        /// <summary>
        /// Уменьшение жизней
        /// </summary>
        public void DecrementLife()
        {
            _life--;
            if (_life <= 0) RestartGame();
        }

        public void IncrementDeath()
        {
            _currentDeath++;
        }

        /// <summary>
        /// Ожидание нажатие кнопки
        /// </summary>
        public void ReadkeyMoveSnake()
        {

            ConsoleKeyInfo key;
            key = Console.ReadKey();
            while (key.Key != ConsoleKey.Escape && _gameState == GameState.game)
            {                

                if (key.Key == ConsoleKey.W || key.Key == ConsoleKey.UpArrow)
                {
                    _currentLevel.MoveSnake.MoveUp();
                }

                if (key.Key == ConsoleKey.A || key.Key == ConsoleKey.LeftArrow)
                {
                    _currentLevel.MoveSnake.MoveLeft();
                }

                if (key.Key == ConsoleKey.D || key.Key == ConsoleKey.RightArrow)
                {
                    _currentLevel.MoveSnake.MoveRight();
                }

                if (key.Key == ConsoleKey.S || key.Key == ConsoleKey.DownArrow)
                {
                    _currentLevel.MoveSnake.MoveDown();
                }
                key = Console.ReadKey();
            }
        }

    }
}
