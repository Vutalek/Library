using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BaumansGateLibrary.Warriors;

namespace BaumansGateLibrary.Battle
{
    public static class WeaponGrabProcessor
    {
        public static bool CanGrab(Unit Robber, Unit Prey)
        {
            if (Grid.DistanceBetweenPoints(Robber.GetCurrentPosition(), Prey.GetCurrentPosition()) == 1)
                return true;
            return false;

        }
        public static bool Grab(Unit Robber, Unit Prey)
        {
            if (!CanGrab(Robber, Prey))
            {
                GameLog.AddLine(Robber.GetShortName().ToString() + " не дотянулся до " + Prey.GetShortName().ToString() + ", чтобы выхватить оружие.\n");
                return false;
            }
            Random ran = new Random();
            int GrabChance = ran.Next(1, 101);
            if (GrabChance <= 70)
            {
                GameLog.AddLine(Robber.GetShortName().ToString() + " попытался выхватить оружие у " + Prey.GetShortName().ToString() + ", но у него не получилось.\n");
                return false;
            }
            else
            {
                GameLog.AddLine(Robber.GetShortName().ToString() + " выхватил оружие у " + Prey.GetShortName().ToString() + ".\n");
                Robber.WeaponChange(Prey.GetEquipedWeapon());
                Prey.WeaponChange(new Weapon("Кулаки", 1, 0, 1));
                return true;
            }
        }
    }
}
