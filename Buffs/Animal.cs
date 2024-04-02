using BaumansGateLibrary.Warriors;
using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaumansGateLibrary.Buffs
{
    [Serializable]
    public class Animal
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public bool Dead { get; set; }
        Unit owner;
        protected Buff.Types BuffType;
        public Animal() { }
        public void SetPet(Unit u)
        {
            owner = u;
            u.GetAnimalList().Add(this);
        }
        public Buff.Types GetBuffType() { return BuffType; }
    }
}
