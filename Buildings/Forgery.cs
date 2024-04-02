using BaumansGateLibrary.Warriors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaumansGateLibrary.Buildings
{
    [Serializable]
    public class Forgery : IBuilding
    {
        public static ForgeryUI MainInterface;
        public string Name { get; set; }
        public string Description { get; set; }
        public int Level { get; set; }
        public List<Resource> PriceToBuild { get; set; }
        public enum ForgeryActions
        {
            Upgrade = 1,
            Exit
        }
        public Forgery()
        {
            Name = "Кузница";
            Description = "В Кузнице живёт Кузнец. Цель - увеличение атаки юнитов";
            PriceToBuild = new List<Resource>();
            PriceToBuild.Add(new Resource(Resource.Types.Wood, 100));
            PriceToBuild.Add(new Resource(Resource.Types.Stone, 500));
            Level = 1;
        }
        public void BuildingEvent(Player user)
        {
            bool ExitFlag = false;
            while (!ExitFlag)
            {
                MainInterface.ShowMenu();
                ForgeryActions action = MainInterface.ChooseMenuAction();
                switch (action)
                {
                    case ForgeryActions.Upgrade:
                        MainInterface.ShowListOfUnits(user.GetUnits());
                        char shortNameForUpgrade = MainInterface.ChooseShortName(user);
                        bool[] Status = UpgradeAttack(user, shortNameForUpgrade);
                        if (Status[0])
                            MainInterface.UpgradeComplete();
                        else if (Status[1])
                            MainInterface.UpgradeFailNotEnoughtMoney();
                        else
                            MainInterface.UpgradeFailAlreadyMax();
                        break;
                    case ForgeryActions.Exit:
                        ExitFlag = true;
                        break;
                }
            }
        }
        public bool[] UpgradeAttack(Player user, char shortName)
        {
            Unit temp = user.GetUnits()[shortName];
            Weapon tempWeapon = temp.GetEquipedWeapon();
            bool[] Status = { true, true }; //Status[0] = true - улучшение произошло, Status[1] = true - потому что не хватило денег, Status[1] = false - потму что и так максимальная атака
            if (tempWeapon.GetAverageAttack() == 15 && tempWeapon.GetDispersionOfAttack() == 0)
            {
                Status[0] = false;
                Status[1] = false;
                return Status;
            }
            else if (user.GetMoney() < 10-Level)
            {
                Status[0] = false;
                Status[1] = true;
                return Status;
            }
            else
            {
                tempWeapon.IncreaseAttack();
                temp.WeaponChange(tempWeapon);
                user.ChangeMoney(10 - Level, Player.HowChange.Decrease);
                return Status;
            }
        }
        public void Upgrade()
        {
            Level++;
        }
        public City.BuildingTypes GetBuildingType()
        {
            return City.BuildingTypes.Forgery;
        }
    }
}
