using BaumansGateLibrary.Warriors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaumansGateLibrary.Buildings
{
    public interface ArsenalUI
    {
        void ShowMenu();
        Arsenal.ArsenalActions ChooseMenuAction();
        void ShowListOfUnits(Dictionary<char, Unit> U);
        char ChooseShortName(Player user);
        void UpgradeComplete();
        void UpgradeFailNotEnoughtMoney();
        void UpgradeFailAlreadyMax();
    }
}
