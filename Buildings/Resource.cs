using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace BaumansGateLibrary.Buildings
{
    [Serializable]
    public class Resource
    {
        public enum Types
        {
            Wood,
            Stone
        }
        public string Name { get; private set; }
        public Types ResourceTypeID { get; private set; }
        public int Amount { get; private set; }
        public int Price {  get; private set; }
        public Resource(Types type, int amount = 0) 
        {
            ResourceTypeID = type;
            switch(type)
            {
                case Types.Wood:
                    Name = "Дерево";
                    Price = 1;
                    break;
                case Types.Stone:
                    Name = "Камень";
                    Price = 2;
                    break;
            }
            Amount = amount;
        }
        public Types GetResourceTypeID() {  return ResourceTypeID; }
        public int GetAmount() {  return Amount; }
        public enum HowChange
        {
            Decrease,
            Increase
        }
        public void ChangeAmount(int amount, HowChange H)
        {
            switch (H)
            {
                case HowChange.Decrease:
                    Amount -= amount;
                    break;
                case HowChange.Increase:
                    Amount += amount;
                    break;
            }
        }
        public bool Enough(Resource R)
        {
            if (Amount >= R.Amount)
                return true;
            else
                return false;
        }
        public int GetCost()
        {
            return Price * Amount;
        }
    }
}