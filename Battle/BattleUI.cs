using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BaumansGateLibrary.Warriors;
using BaumansGateLibrary.Buffs;

namespace BaumansGateLibrary.Battle
{
    public interface BattleUI
    {
        void ShowUnitList(Dictionary<char, Unit> UnitsDictionary);
        void ShowLine();
        Unit ChooseUnit(Dictionary<char, Unit> Units, bool ShowAdditionInfo = true);
        Position ChoosePosition(Unit U, Grid MainLayout);
        Position ChoosePosition(char U);
        Animal ChooseAnimal(Player user, char U);
        void ShowMenu();
        Game.Actions ChooseAction();
        void ShowVictoryScreen();
        void ShowDefeatedScreen();
        void ShowGrid(Grid Layout);
        void ShowLog();
    }
}
