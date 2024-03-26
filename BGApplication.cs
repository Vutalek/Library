using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using BaumansGateLibrary.Battle;
using BaumansGateLibrary.Buildings;
using BaumansGateLibrary.Warriors;

namespace BaumansGateLibrary
{
    public class BGApplication
    {
        UI MainInterface;
        Player User;
        public static int SlotNumber = 0;
        public enum MenuActions
        {
            Player=1,
            City,
            Map,
            Battle,
            Exit
        }
        public enum PlayerMenuActions
        {
            Save=1,
            Exit
        }
        public enum CityMenuActions
        {
            ChooseBuilding=1,
            Build,
            Exit
        }
        public enum MapMenuActions
        {
            ShowMap=1,
            LoadMap,
            DeleteMap,
            Exit
        }
        public enum BattleMenuActions
        {
            StartNewGame=1,
            Exit
        }
        public BGApplication(UI U, BattleUI B, CityUI C, AcademyUI A, ArsenalUI Ar, DoctorHouseUI D, ForgeryUI F, MarketUI M, TawernUI T, WorkshopUI W)
        {
            Directory.CreateDirectory("maps");
            Directory.CreateDirectory("saves");

            MainInterface = U;
            Game.BattleInterface = B;
            City.MainInterface = C;
            User = MainInterface.CreatePLayer();

            Academy.MainInterface = A;
            Arsenal.MainInterface = Ar;
            DoctorHouse.MainInterface = D;
            Forgery.MainInterface = F;
            Market.MainInterface = M;
            Tawern.MainInterface = T;
            Workshop.MainInterface = W;
        }
        public void Start()
        {
            bool ExitFlag = false;
            while (!ExitFlag)
            {
                MainInterface.ShowMenu();
                MenuActions action = MainInterface.ChooseMenuAction();
                switch (action)
                {
                    case MenuActions.Player:
                        PlayerMenu();
                        break;
                    case MenuActions.City:
                        CityMenu();
                        break;
                    case MenuActions.Map:
                        MapMenu();
                        break;
                    case MenuActions.Battle:
                        BattleMenu();
                        break;
                    case MenuActions.Exit:
                        ExitFlag = true;
                        break;
                }
            }
        }
        public void PlayerMenu() 
        {
            MainInterface.ShowPlayerInformation(User);
            bool ExitFlag = false;
            while(!ExitFlag)
            {
                MainInterface.ShowPlayerMenu();
                PlayerMenuActions action = MainInterface.ChoosePlayerMenuAction();
                switch (action)
                {
                    case PlayerMenuActions.Save:
                        string path = MainInterface.ChoosePath();
                        BasicSerializer<Player>.SaveObject(path, User);
                        MainInterface.Done();
                        break;
                    case PlayerMenuActions.Exit:
                        ExitFlag = true;
                        break;
                }
            }
        }
        public void CityMenu() 
        {
            bool ExitFlag = false;
            while (!ExitFlag)
            {
                MainInterface.ShowCity(User);
                MainInterface.ShowCityMenu();
                CityMenuActions action = MainInterface.ChooseCityMenuAction();
                switch (action)
                {
                    case CityMenuActions.ChooseBuilding:
                        int index = MainInterface.ChooseNumberOfBuilding(User);
                        User.GetCity().GetBuildings()[index].BuildingEvent(User);
                        break;
                    case CityMenuActions.Build:
                        User.GetCity().Build(User);
                        break;
                    case CityMenuActions.Exit:
                        ExitFlag = true;
                        break;
                }
            }
        }
        public void MapMenu() 
        {
            bool ExitFlag = false;
            while (!ExitFlag)
            {
                MainInterface.ShowMapMenu(User);
                MapMenuActions action = MainInterface.ChooseMapMenuAction();
                switch (action)
                {
                    case MapMenuActions.ShowMap:
                        int FileName = MainInterface.ChooseMapName();
                        if (FileName == -1)
                            break;
                        MainInterface.ShowMapInformation(FileName);
                        break;
                    case MapMenuActions.LoadMap:
                        MainInterface.LoadMapFromFile();
                        break;
                    case MapMenuActions.DeleteMap:
                        int DeleteName = MainInterface.ChooseMapName();
                        if (DeleteName == -1)
                            break;
                        MainInterface.DeleteMap(DeleteName);
                        break;
                    case MapMenuActions.Exit:
                        ExitFlag = true;
                        break;
                }
            }
        }
        public void BattleMenu()
        {
            bool ExitFlag = false;
            while (!ExitFlag)
            {
                MainInterface.ShowBattleMenu(User);
                BattleMenuActions action = MainInterface.ChooseBattleMenuAction();
                switch (action)
                {
                    case BattleMenuActions.StartNewGame:
                        bool RandomMap = MainInterface.ChooseIfMapIsRandom();
                        Grid MainMap;
                        if (RandomMap)
                            MainMap = new Grid();
                        else
                            MainMap = MainInterface.ChooseMapToBattle();
                        Game MainGame = new Game(MainMap, User);
                        MainGame.StartGame();
                        break;
                    case BattleMenuActions.Exit:
                        ExitFlag = true;
                        break;
                }
            }
        }
    }
}
