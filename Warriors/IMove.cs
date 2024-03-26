using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BaumansGateLibrary.Battle;

namespace BaumansGateLibrary.Warriors
{
    internal interface IMove
    {
        bool CanMove(Position NextPosition, Grid MainLayout);
        bool Move(Position NextPosition, Grid MainLayout);
    }
}