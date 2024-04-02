using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaumansGateLibrary.Buildings
{
    public interface IBuilding
    {
        string Name { get; set; }
        string Description { get; set; }
        int Level { get; set; }
        List<Resource> PriceToBuild { get; set; }
        void BuildingEvent(Player user);
        City.BuildingTypes GetBuildingType();
    }
}