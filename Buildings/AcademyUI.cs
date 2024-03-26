using BaumansGateLibrary.Warriors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaumansGateLibrary.Buildings
{
    public interface AcademyUI
    {
        void ShowMenu();
        void ShowListOfUnits(Dictionary<char, Unit> U);
        char ChooseShortName(Player user);
        int ChooseTypeOfUnit();
        void BuyComplete();
        void BuyFail();
        void SellComplete();
        void MaximumUnits();
        Academy.AcademyActions ChooseMenuAction();
    }
}