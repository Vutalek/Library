using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BaumansGateLibrary.Buildings;
using BaumansGateLibrary.Warriors;

namespace BaumansGateLibrary
{
    [Serializable]
    public class City
    {
        public static CityUI MainInterface;
        public enum BuildingTypes
        {
            Academy=1,
            Arsenal,
            DoctorHouse,
            Forgery,
            Market,
            Tawern,
            Workshop
        }
        List<IBuilding> Buildings = new List<IBuilding>();
        public City()
        {
            Buildings.Add(new Academy());
            Buildings.Add(new Market());
            Buildings.Add(new Workshop());
            Buildings.Add(new DoctorHouse());
        }
        public List<IBuilding> GetBuildings()
        {
            return Buildings;
        }
        public enum BuildActions
        {
            Build=1,
            Exit
        }
        public void Build(Player user)
        {
            bool ExitFlag = false;
            while (!ExitFlag)
            {
                MainInterface.ShowMenu();
                BuildActions action = MainInterface.ChooseMenuAction();
                switch (action)
                {
                    case BuildActions.Build:
                        MainInterface.ShowListOfBuildings();
                        BuildingTypes type= MainInterface.ChooseBuilding();
                        IBuilding Builded = new Academy();
                        switch(type)
                        {
                            case BuildingTypes.Academy:
                                Builded = new Academy();
                                break;
                            case BuildingTypes.Arsenal:
                                Builded = new Arsenal();
                                break;
                            case BuildingTypes.DoctorHouse:
                                Builded = new DoctorHouse();
                                break;
                            case BuildingTypes.Forgery:
                                Builded = new Forgery();
                                break;
                            case BuildingTypes.Market:
                                Builded = new Market();
                                break;
                            case BuildingTypes.Tawern:
                                Builded = new Tawern();
                                break;
                            case BuildingTypes.Workshop:
                                Builded = new Workshop();
                                break;
                        }
                        int CanBuild = 0;
                        foreach (Resource Need in Builded.PriceToBuild)
                            foreach (Resource R in user.GetResourcesInPossession())
                            {
                                if (!R.Enough(Need))
                                    CanBuild--;
                                else
                                    CanBuild++;
                            }
                        if(CanBuild <= 0)
                        {
                            MainInterface.BuildFail();
                        }
                        else
                        {
                            foreach (Resource Need in Builded.PriceToBuild)
                                foreach (Resource R in user.GetResourcesInPossession())
                                    if (R.ResourceTypeID == Need.ResourceTypeID)
                                        R.ChangeAmount(Need.Amount, Resource.HowChange.Decrease);
                            user.GetCity().GetBuildings().Add(Builded);
                            MainInterface.BuildComplete();
                        }
                        break;
                    case BuildActions.Exit:
                        ExitFlag = true;
                        break;
                }
            }
        }
    }
}
