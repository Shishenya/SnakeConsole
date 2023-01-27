using SnakeConsole.Models;
using System.Collections.Generic;

namespace SnakeConsole.Utilities
{
    public static class LevelConstructUtility
    {

        /// <summary>
        /// Создает тестовые уровни
        /// </summary>
        public static Levels TestLevelConstruct()
        {
            Levels testLevels = new Levels();

            Level level1 = new Level(4,
                                     3,
                                     new List<Point> { new Point(0,0),
                                                       new Point(0,1),
                                                       new Point(0,2),
                                                       new Point(0,3),
                                     },
                                     2,
                                     1,
                                     new Point(1, 2));

            Level level2 = new Level(4,
                                     4,
                                     new List<Point> { new Point(0, 3), new Point(1, 3), new Point(2, 3), new Point(3, 3) },
                                     3,
                                     2,
                                     new Point(2, 2));

            Level level3 = new Level(6,
                                     6,
                                     new List<Point> { new Point(0,0),
                                                       new Point(0,1),
                                                       new Point(0,2),
                                                       new Point(0,3),
                                                       new Point(0,4),
                                                       new Point(0,5),
                                                       new Point(1,0),
                                                       new Point(2,0),
                                                       new Point(3,0),
                                                       new Point(4,0),
                                                       new Point(5,0),
                                                       new Point(5,1),
                                                       new Point(5,2),
                                                       new Point(5,3),
                                                       new Point(5,4),
                                                       new Point(5,5),
                                                       new Point(1,5),
                                                       new Point(2,5),
                                                       new Point(3,5),
                                                       new Point(4,5),
                                     },
                                     4,
                                     3,
                                     new Point(3, 3));

            List<Level> levels = new List<Level> { level1, level2, level3 };
            testLevels.levelList = levels;

            return testLevels;
        }

    }
}
