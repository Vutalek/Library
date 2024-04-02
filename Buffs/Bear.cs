using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaumansGateLibrary.Buffs
{
    [Serializable]
    public class Bear : Animal
    {
        public Bear()
        {
            Name = "Медведь";
            Description = "Огромный медведь. увеличивает атаку на 2";
            BuffType = Buff.Types.BearBuff;
            Dead = false;
        }
    }
}
