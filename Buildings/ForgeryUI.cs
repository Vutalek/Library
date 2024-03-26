using BaumansGateLibrary.Warriors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static BaumansGateLibrary.Buildings.Forgery;

namespace BaumansGateLibrary.Buildings
{
    public interface ForgeryUI
    {
        void ShowMenu();
        ForgeryActions ChooseMenuAction();
        void ShowListOfUnits(Dictionary<char, Unit> U);
        char ChooseShortName(Player user);
        void UpgradeComplete();
        void UpgradeFailNotEnoughtMoney();
        void UpgradeFailAlreadyMax();
    }
}
