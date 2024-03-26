using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BaumansGateLibrary.Battle;
using BaumansGateLibrary.Buildings;
using BaumansGateLibrary.Warriors;

namespace BaumansGateLibrary
{
    [Serializable]
    public class Player
    {
        string HeroName;
        int Money;
        City Town;
        Dictionary<char, Unit> UnitsInPossession = new Dictionary<char, Unit>();
        List<Resource> ResourcesInPossession = new List<Resource>();
        public Player(string name) 
        {
            HeroName = name;
            Money = 100;
            Town = new City();
        }
        public string GetHeroName() { return HeroName; }
        public int GetMoney() {  return Money; }
        public City GetCity() { return Town; }
        public Dictionary<char, Unit> GetUnits() { return UnitsInPossession; }
        public List<Resource> GetResourcesInPossession() {  return ResourcesInPossession; }
        public void GenerallyUpdateUnits(Dictionary<char, Unit> NewDictionary)
        {
            UnitsInPossession = NewDictionary;
        }
        public enum HowChange
        {
            Decrease,
            Increase
        }
        public void ChangeMoney(int amount, HowChange H)
        {
            switch(H)
            {
                case HowChange.Decrease:
                    Money -= amount;
                    break;
                case HowChange.Increase:
                    Money += amount; 
                    break;
            }
        }
        public void AddCycle()
        {
            foreach(IBuilding B in Town.GetBuildings())
            {
                if(B.GetBuildingType() == City.BuildingTypes.Workshop)
                {
                    ((Workshop)B).IncreaseCycle();
                    break;
                }
            }
        }
    }
}
