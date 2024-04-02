using BaumansGateLibrary.Warriors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static BaumansGateLibrary.Buildings.AnimalShop;

namespace BaumansGateLibrary.Buildings
{
    public interface AnimalShopUI
    {
        void ShowMenu();
        AnimalShopActions ChooseMenuAction();
        void ShowListOfUnits(Dictionary<char, Unit> Units);
        char ChooseShortName(Player user);
        void ShowAnimals();
        int ChooseAnimal();
        void BuyComplete();
        void BuyFailNotEnoughtMoney();
        void BuyFailAlreadyHave();
        void ReviveComplete();
    }
}
