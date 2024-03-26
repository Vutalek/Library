using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaumansGateLibrary.Buildings
{
    public interface TawernUI
    {
        void ShowMenu();
        Tawern.TawernActions ChooseMenuAction();
        void ShowPartyComplete();
        void ShowPartyFail();
    }
}
