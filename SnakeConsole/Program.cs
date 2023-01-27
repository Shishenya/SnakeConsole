using SnakeConsole.Models;
using SnakeConsole.Scripts.SaveLoad;

namespace SnakeConsole
{
    internal class Program
    {

        private static ISaveable _saveSystem;
        private static ILoadable _loadSystem;

        static void Main(string[] args)
        {

            #region BEGIN CREATE TEST LEVELS
            // Levels saveLevels = LevelConstructUtility.TestLevelConstruct();
            #endregion END BEGIN CREATE TEST LEVELS

            #region BEGIN Loading SaveLoadSystem
            JsonSaveLoad SaveLoadJsonSystem = new JsonSaveLoad();
            _saveSystem = SaveLoadJsonSystem;
            _loadSystem = SaveLoadJsonSystem;
            #endregion END Loading SaveLoadSystem

            #region BEGIN SAVE Levels to FILE
            // _saveSystem.SaveData(saveLevels);
            #endregion END SAVE Levels to FILE

            #region BEGIN LOAD LEVELS
            Levels levelsToLoad = new Levels();
            _loadSystem.LoadData(ref levelsToLoad);
            #endregion END LOAD LEVELS

            #region BEGIN Start Game
            Game game = new Game(levelsToLoad.levelList);
            #endregion END Game

        }
    }
}
