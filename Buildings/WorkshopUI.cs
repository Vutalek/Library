using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaumansGateLibrary.Buildings
{
    public interface WorkshopUI
    {
        void ShowMenu();
        Workshop.WorkshopActions ChooseMenuAction();
        void HarvestFail();
        void HarvestComplete();
    }
}
