using BaumansGateLibrary.Buffs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace BaumansGateLibrary.Buildings
{
    [Serializable]
    public class Workshop : IBuilding
    {
        public static WorkshopUI MainInterface;
        public string Name { get; set; }
        public string Description { get; set; }
        public int Level {  get; set; }
        public List<Resource> PriceToBuild { get; set; }
        public int Cycles;
        public enum WorkshopActions
        {
            Harvest = 1,
            Exit
        }
        public Workshop()
        {
            Name = "Ремесленная мастерская";
            Description = "Ремесленники любят Ваш город почти также сильно, как и Вы сами. Они с удовольствием поселятся у Вас и будут платить некоторую сумму золотыми за аренду.";
            PriceToBuild = new List<Resource>();
            PriceToBuild.Add(new Resource(Resource.Types.Wood, 100));
            PriceToBuild.Add(new Resource(Resource.Types.Stone, 500));
            Cycles = 0;
            Level = 1;
        }
        public void BuildingEvent(Player user)
        {
            bool ExitFlag = false;
            while (!ExitFlag)
            {
                MainInterface.ShowMenu();
                WorkshopActions action = MainInterface.ChooseMenuAction();
                switch (action)
                {
                    case WorkshopActions.Harvest:
                        if (Cycles == 0)
                            MainInterface.HarvestFail();
                        else
                        {
                            user.ChangeMoney(Cycles * 100 * Level, Player.HowChange.Increase);
                            ClearCycles();
                            MainInterface.HarvestComplete();
                        }
                        break;
                    case WorkshopActions.Exit:
                        ExitFlag = true;
                        break;
                }
            }
        }
        public void IncreaseCycle()
        {
            Cycles++;
        }
        public void ClearCycles()
        {
            Cycles = 0;
        }
        public void Upgrade()
        {
            Level++;
        }
        public City.BuildingTypes GetBuildingType()
        {
            return City.BuildingTypes.Workshop;
        }
    }
}
