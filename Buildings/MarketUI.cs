using BaumansGateLibrary.Warriors;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaumansGateLibrary.Buildings
{
    public interface MarketUI
    {
        void ShowMenu();
        Resource.Types ChooseTypeOfResourceInStock(List<Resource> AlreadyHave);
        Resource.Types ChooseTypeOfResourceInPossession(List<Resource> AlreadyHave);
        void BuyComplete();
        void BuyFail();
        void SellComplete();
        Market.MarketActions ChooseMenuAction();
    }
}
