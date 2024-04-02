using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaumansGateLibrary.Buffs
{
    [Serializable]
    public class Jaguar : Animal
    {
        public Jaguar() 
        {
            Name = "Ягуар";
            Description = "Могучий кошачий. Увеличивает дальность перемещения на 1";
            BuffType = Buff.Types.JaguarBuff;
            Dead = false;
        }
    }
}
