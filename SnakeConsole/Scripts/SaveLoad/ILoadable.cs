using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnakeConsole.Scripts.SaveLoad
{
    public interface ILoadable
    {
        T LoadData<T>(ref T data);
    }
}
