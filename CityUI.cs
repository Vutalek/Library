using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaumansGateLibrary
{
    public interface CityUI
    {
        void ShowMenu();
        City.BuildActions ChooseMenuAction();
        void ShowListOfBuildings();
        City.BuildingTypes ChooseBuilding();
        void BuildComplete();
        void BuildFail();
    }
}
