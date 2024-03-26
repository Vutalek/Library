using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BaumansGateLibrary.Warriors;

namespace BaumansGateLibrary.Buildings
{
    public interface DoctorHouseUI
    {
        void ShowMenu();
        char ChooseShortName(Player user);
        void ShowListOfUnits(Dictionary<char, Unit> U);
        DoctorHouse.DoctorHouseActions ChooseMenuAction();
        void HealComplete();
        void UpgradeComplete();
        void UpgradeFailAlreadyMax();
        void UpgradeFailNotEnoughtMoney();
    }
}