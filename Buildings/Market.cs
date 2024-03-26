using BaumansGateLibrary.Warriors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaumansGateLibrary.Buildings
{
    [Serializable]
    public class Market : IBuilding
    {
        public static MarketUI MainInterface;
        public string Name { get; set; }
        public string Description { get; set; }
        public List<Resource> PriceToBuild { get; set; }
        public enum MarketActions
        {
            Buy = 1,
            Sell,
            Exit
        }
        public Market()
        {
            Name = "Рынок";
            Description = "Рынок - удивительное место. Чего тут только нет! Хотя мы здесь для обмена ресурсами.";
            PriceToBuild = new List<Resource>();
            PriceToBuild.Add(new Resource(Resource.Types.Wood, 100));
            PriceToBuild.Add(new Resource(Resource.Types.Stone, 500));
        }
        public void BuildingEvent(Player user)
        {
            bool ExitFlag = false;
            while (!ExitFlag)
            {
                MainInterface.ShowMenu();
                MarketActions action = MainInterface.ChooseMenuAction();
                switch (action)
                {
                    case MarketActions.Buy:
                        Resource.Types BuyType = MainInterface.ChooseTypeOfResourceInStock(user.GetResourcesInPossession());
                        bool Bought = BuyResource(user, BuyType);
                        if (Bought)
                            MainInterface.BuyComplete();
                        else
                            MainInterface.BuyFail();
                        break;
                    case MarketActions.Sell:
                        Resource.Types SellType = MainInterface.ChooseTypeOfResourceInPossession(user.GetResourcesInPossession());
                        SellResource(user, SellType);
                        MainInterface.SellComplete();
                        break;
                    case MarketActions.Exit:
                        ExitFlag = true;
                        break;
                }
            }
        }
        public bool BuyResource(Player user, Resource.Types type)
        {
            Resource PurchasedResource = new Resource(Resource.Types.Wood);
            switch (type)
            {
                case Resource.Types.Wood:
                    PurchasedResource = new Resource(Resource.Types.Wood, 100);
                    break;
                case Resource.Types.Stone:
                    PurchasedResource = new Resource(Resource.Types.Stone, 20);
                    break;
            }
            if (PurchasedResource.GetCost() <= user.GetMoney())
            {
                user.ChangeMoney(PurchasedResource.GetCost(), Player.HowChange.Decrease);
                foreach(Resource R in user.GetResourcesInPossession())
                {
                    if (R.GetResourceTypeID() == PurchasedResource.GetResourceTypeID())
                    {
                        R.ChangeAmount(PurchasedResource.GetAmount(), Resource.HowChange.Increase);
                        return true;
                    }
                }
                user.GetResourcesInPossession().Add(PurchasedResource);
                return true;
            }
            else
                return false;
        }
        public void SellResource(Player user, Resource.Types type)
        {
            Resource Chosen = new Resource(Resource.Types.Wood);
            foreach (Resource R in user.GetResourcesInPossession())
            {
                if (R.GetResourceTypeID() == type)
                {
                    Chosen = R;
                    break;
                }
            }
            user.ChangeMoney(Chosen.GetCost(), Player.HowChange.Increase);
            user.GetResourcesInPossession().Remove(Chosen);
        }
        public City.BuildingTypes GetBuildingType()
        {
            return City.BuildingTypes.Market;
        }
    }
}
