using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BaumansGateLibrary.Warriors;

namespace BaumansGateLibrary.Buildings
{
    [Serializable]
    public class DoctorHouse : IBuilding
    {
        public static DoctorHouseUI MainInterface;
        public string Name { get; set; }
        public string Description { get; set; }
        public List<Resource> PriceToBuild { get; set; }
        public enum DoctorHouseActions
        {
            Heal = 1,
            Upgrade,
            Exit
        }
        public DoctorHouse()
        {
            Name = "Дом Лекаря";
            Description = "В доме лекаря исследуются новые алхимические элементы. Цель - лечение и повышение HP юнитов.";
            PriceToBuild = new List<Resource>();
            PriceToBuild.Add(new Resource(Resource.Types.Wood, 10));
            PriceToBuild.Add(new Resource(Resource.Types.Stone, 400));
        }   
        public void BuildingEvent(Player user)
        {
            bool ExitFlag = false;
            while (!ExitFlag)
            {
                MainInterface.ShowMenu();
                DoctorHouseActions action = MainInterface.ChooseMenuAction();
                switch (action)
                {
                    case DoctorHouseActions.Heal:
                        HealAllUnits(user);
                        MainInterface.HealComplete();
                        break;
                    case DoctorHouseActions.Upgrade:
                        MainInterface.ShowListOfUnits(user.GetUnits());
                        char shortNameForUpgrade = MainInterface.ChooseShortName(user);
                        bool[] Status = UpgradeMaxHealth(user, shortNameForUpgrade);
                        if (Status[0])
                            MainInterface.UpgradeComplete();
                        else if (Status[1])
                            MainInterface.UpgradeFailNotEnoughtMoney();
                        else
                            MainInterface.UpgradeFailAlreadyMax();
                        break;
                    case DoctorHouseActions.Exit:
                        ExitFlag = true;
                        break;
                }
            }
        }
        public void HealAllUnits(Player user)
        {
            foreach (char U in user.GetUnits().Keys)
                user.GetUnits()[U].Heal(user.GetUnits()[U].GetMaxHealth());
        }
        public bool[] UpgradeMaxHealth(Player user, char shortName)
        {
            Unit temp = user.GetUnits()[shortName];
            bool[] Status = { true, true }; //Status[0] = true - улучшение произошло, Status[1] = true - потому что не хватило денег, Status[1] = false - потму что и так максиумм здоровья
            if (temp.GetMaxHealth() == 100)
            {
                Status[0] = false;
                Status[1] = false;
                return Status;
            }
            else if (user.GetMoney() < 10)
            {
                Status[0] = false;
                Status[1] = true;
                return Status;
            }
            else
            {
                temp.IncreaseMaxHealth(5);
                user.ChangeMoney(10, Player.HowChange.Decrease);
                return Status;
            }
        }
        public City.BuildingTypes GetBuildingType()
        {
            return City.BuildingTypes.DoctorHouse;
        }
    }
}