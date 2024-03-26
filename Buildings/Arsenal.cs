using BaumansGateLibrary.Warriors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaumansGateLibrary.Buildings
{
    [Serializable]
    public class Arsenal : IBuilding
    {
        public static ArsenalUI MainInterface;
        public string Name { get; set; }
        public string Description { get; set; }
        public List<Resource> PriceToBuild { get; set; }
        public enum ArsenalActions
        {
            Upgrade = 1,
            Exit
        }
        public Arsenal()
        {
            Name = "Арсенал";
            Description = "В арсенале изготавливают новые виды брони. Цель - увеличение брони юнитов.";
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
                ArsenalActions action = MainInterface.ChooseMenuAction();
                switch (action)
                {
                    case ArsenalActions.Upgrade:
                        MainInterface.ShowListOfUnits(user.GetUnits());
                        char shortNameForUpgrade = MainInterface.ChooseShortName(user);
                        bool[] Status = UpgradeDefence(user, shortNameForUpgrade);
                        if (Status[0])
                            MainInterface.UpgradeComplete();
                        else if (Status[1])
                            MainInterface.UpgradeFailNotEnoughtMoney();
                        else
                            MainInterface.UpgradeFailAlreadyMax();
                        break;
                    case ArsenalActions.Exit:
                        ExitFlag = true;
                        break;
                }
            }
        }
        public bool[] UpgradeDefence(Player user, char shortName)
        {
            Unit temp = user.GetUnits()[shortName];
            Armor tempArmor= temp.GetEquipedArmor();
            bool[] Status = { true, true }; //Status[0] = true - улучшение произошло, Status[1] = true - потому что не хватило денег, Status[1] = false - потму что и так максимальная броня
            if (tempArmor.GetDefence() == 20)
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
                tempArmor.IncreaseDefence();
                temp.ArmorChange(tempArmor);
                return Status;
            }
        }
        public City.BuildingTypes GetBuildingType()
        {
            return City.BuildingTypes.Arsenal;
        }
    }
}
