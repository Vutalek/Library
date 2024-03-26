using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BaumansGateLibrary.Warriors;

namespace BaumansGateLibrary.Buildings
{
    [Serializable]
    public class Academy : IBuilding
    {
        public static AcademyUI MainInterface;
        public string Name { get; set; }
        public string Description { get; set; }
        public List<Resource> PriceToBuild { get; set; }
        public enum AcademyActions
        {
            Buy = 1,
            Sell,
            Exit
        }
        public Academy()
        {
            Name = "Академия";
            Description = "В Академии лучшие умы города, тактики и стратеги, разрабатывают новейшие приспособления для различных задач. Цель – создание новых Юнитов";
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
                AcademyActions action = MainInterface.ChooseMenuAction();
                switch (action)
                {
                    case AcademyActions.Buy:
                        if (user.GetUnits().Count == 10)
                        {
                            MainInterface.MaximumUnits();
                            break;
                        }
                        MainInterface.ShowListOfUnits(user.GetUnits());
                        int type = MainInterface.ChooseTypeOfUnit();
                        char shortNameForBuy = MainInterface.ChooseShortName(user);
                        bool Bought = BuyUnit(user, type, shortNameForBuy);
                        if (Bought)
                            MainInterface.BuyComplete();
                        else
                            MainInterface.BuyFail();
                        break;
                    case AcademyActions.Sell:
                        MainInterface.ShowListOfUnits(user.GetUnits());
                        char shortNameForSell = MainInterface.ChooseShortName(user);
                        SellUnit(user, shortNameForSell);
                        MainInterface.SellComplete();
                        break;
                    case AcademyActions.Exit:
                        ExitFlag = true;
                        break;
                }
            }
        }
        public bool BuyUnit(Player user, int type, char shortName)
        {
            Unit PurchasedUnit = new Unit();
            switch (type)
            {
                case 0:
                    PurchasedUnit = new Earthlings(0, shortName, new Position());
                    break;
                case 1:
                    PurchasedUnit = new Earthlings(1, shortName, new Position());
                    break;
                case 2:
                    PurchasedUnit = new Earthlings(2, shortName, new Position());
                    break;
                case 3:
                    PurchasedUnit = new Archers(0, shortName, new Position());
                    break;
                case 4:
                    PurchasedUnit = new Archers(1, shortName, new Position());
                    break;
                case 5:
                    PurchasedUnit = new Archers(2, shortName, new Position());
                    break;
                case 6:
                    PurchasedUnit = new Riders(0, shortName, new Position());
                    break;
                case 7:
                    PurchasedUnit = new Riders(1, shortName, new Position());
                    break;
                case 8:
                    PurchasedUnit = new Riders(2, shortName, new Position());
                    break;
            }
            if (PurchasedUnit.GetPrice() <= user.GetMoney())
            {
                user.ChangeMoney(PurchasedUnit.GetPrice(), Player.HowChange.Decrease);
                user.GetUnits().Add(shortName, PurchasedUnit);
                return true;
            }
            else
                return false;
        }
        public void SellUnit(Player user, char shortName)
        {
            Unit temp = user.GetUnits()[shortName];
            user.ChangeMoney(temp.GetPrice(), Player.HowChange.Increase);
            user.GetUnits().Remove(shortName);
        }
        public City.BuildingTypes GetBuildingType()
        {
            return City.BuildingTypes.Academy;
        }
    }
}