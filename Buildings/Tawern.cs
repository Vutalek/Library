using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BaumansGateLibrary.Buffs;

namespace BaumansGateLibrary.Buildings
{
    [Serializable]
    public class Tawern : IBuilding
    {
        public static TawernUI MainInterface;
        public string Name { get; set; }
        public string Description { get; set; }
        public List<Resource> PriceToBuild { get; set; }
        public enum TawernActions
        {
            Party=1,
            Exit
        }
        public Tawern()
        {
            Name = "Таверна";
            Description = "В таверне проходят различные праздники. Цель - юниты получают повышенные очки перемещения.";
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
                TawernActions action = MainInterface.ChooseMenuAction();
                switch (action)
                {
                    case TawernActions.Party:
                        if (user.GetMoney() >= 10 * user.GetUnits().Count)
                        {
                            foreach (char U in user.GetUnits().Keys)
                                user.GetUnits()[U].GetBuffList().Add(new GoodMood());
                            MainInterface.ShowPartyComplete();
                        }
                        else
                            MainInterface.ShowPartyFail();
                        break;
                    case TawernActions.Exit:
                        ExitFlag = true;
                        break;
                }
            }
        }
        public City.BuildingTypes GetBuildingType()
        {
            return City.BuildingTypes.Tawern;
        }
    }
}
