using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaumansGateLibrary.Buffs
{
    [Serializable]
    public class Hawk : Animal
    {
        public Hawk()
        {
            Name = "Орёл";
            Description = "Гордый пернатый. Игнорируте штрафы на пермещение от препятствий";
            BuffType = Buff.Types.HawkBuff;
            Dead = false;
        }
    }
}
