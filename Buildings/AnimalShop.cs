using BaumansGateLibrary.Warriors;
using BaumansGateLibrary.Buffs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace BaumansGateLibrary.Buildings
{
    [Serializable]
    public class AnimalShop : IBuilding
    {
        public static AnimalShopUI MainInterface;
        public string Name { get; set; }
        public string Description { get; set; }
        public int Level { get; set; }
        public List<Resource> PriceToBuild { get; set; }
        public enum AnimalShopActions
        {
            Buy = 1,
            Revive,
            Exit
        }
        public AnimalShop()
        {
            Name = "Дом питомцев";
            Description = "В этом доме работают лучшие ветеринары, которые обеспечат воинам их верного питомца. Цель – снаряжение юнита питомцем";
            PriceToBuild = new List<Resource>();
            PriceToBuild.Add(new Resource(Resource.Types.Wood, 100));
            PriceToBuild.Add(new Resource(Resource.Types.Stone, 20));
            Level = 1;
        }
        public void BuildingEvent(Player user)
        {
            bool ExitFlag = false;
            while (!ExitFlag)
            {
                MainInterface.ShowMenu();
                AnimalShopActions action = MainInterface.ChooseMenuAction();
                switch (action)
                {
                    case AnimalShopActions.Buy:
                        MainInterface.ShowListOfUnits(user.GetUnits());
                        char shortNameOfUnit = MainInterface.ChooseShortName(user);
                        MainInterface.ShowAnimals();
                        int type = MainInterface.ChooseAnimal();
                        bool[] Status = BuyAnimalForUnit(user, shortNameOfUnit, type);
                        if (Status[0])
                            MainInterface.BuyComplete();
                        else if (Status[1])
                            MainInterface.BuyFailNotEnoughtMoney();
                        else
                            MainInterface.BuyFailAlreadyHave();
                        break;
                    case AnimalShopActions.Revive:
                        MainInterface.ShowListOfUnits(user.GetUnits());
                        char shortNameForSell = MainInterface.ChooseShortName(user);
                        Revive(user, shortNameForSell);
                        MainInterface.ReviveComplete();
                        break;
                    case AnimalShopActions.Exit:
                        ExitFlag = true;
                        break;
                }
            }
        }
        bool[] BuyAnimalForUnit(Player user, char shortNameOfUnit, int type)
        {
            Unit temp = user.GetUnits()[shortNameOfUnit];
            bool[] Status = { true, true }; //Status[0] = true - покупка произошла, Status[1] = true - потому что не хватило денег, Status[1] = false - потому что питомец уже есть
            Animal tempAnimal = new Animal();
            switch(type)
            {
                case 0:
                    tempAnimal = new Jaguar();
                    break;
                case 1:
                    tempAnimal = new Hawk();
                    break;
                case 2:
                    tempAnimal = new Bear();
                    break;
            }
            bool UnitAlreadyHaveAnimal = false;
            foreach(Animal A in temp.GetAnimalList())
            {
                if (A.Name == tempAnimal.Name)
                {
                    UnitAlreadyHaveAnimal = true;
                    break;
                }
            }
            if (UnitAlreadyHaveAnimal)
            {
                Status[0] = false;
                Status[1] = false;
                return Status;
            }
            else if (user.GetMoney() < (100-10*Level))
            {
                Status[0] = false;
                Status[1] = true;
                return Status;
            }
            else
            {
                temp.GetAnimalList().Add(tempAnimal);
                user.ChangeMoney(100 - 10 * Level, Player.HowChange.Decrease);
                return Status;
            }
        }
        void Revive(Player user, char unit)
        {
            Unit temp = user.GetUnits()[unit];
            foreach (Animal A in temp.GetAnimalList())
                A.Dead = false;
        }
        public void Upgrade()
        {
            Level++;
        }
        public City.BuildingTypes GetBuildingType()
        {
            return City.BuildingTypes.AnimalShop;
        }
    }
}
