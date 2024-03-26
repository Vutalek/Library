using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaumansGateLibrary.Warriors
{
    //структура хранит информацию о точке
    [Serializable]
    public struct Position
    {
        public int xCoordinate { get; private set; }
        public int yCoordinate { get; private set; }
        public Position(int x = 0, int y = 0)
        {
            xCoordinate = x;
            yCoordinate = y;
        }
    }
}