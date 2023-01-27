using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnakeConsole.Scripts.SaveLoad
{
    public interface ISaveable
    {
        void SaveData<T>(T data);
    }
}
