using BaumansGateLibrary.Battle;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaumansGateLibrary
{
    public interface UI
    {
        Player CreatePLayer();
        void ShowMenu();
        void Done();
        string ChoosePath();
        BGApplication.MenuActions ChooseMenuAction();
        BGApplication.PlayerMenuActions ChoosePlayerMenuAction();
        BGApplication.CityMenuActions ChooseCityMenuAction();
        int ChooseNumberOfBuilding(Player user);
        BGApplication.MapMenuActions ChooseMapMenuAction();
        int ChooseMapName();
        void ShowMapInformation(int  mapName);
        void LoadMapFromFile();
        void ShowBattleMenu(Player user);
        void DeleteMap(int DeleteName);
        BGApplication.BattleMenuActions ChooseBattleMenuAction();
        bool ChooseIfMapIsRandom();
        Grid ChooseMapToBattle();
        void ShowPlayerMenu();
        void ShowCityMenu();
        void ShowCity(Player user);
        void ShowPlayerInformation(Player user);
        void ShowMapMenu(Player user);
    }
}
