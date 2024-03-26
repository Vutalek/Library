using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using BaumansGateLibrary.Buffs;
using BaumansGateLibrary.Warriors;

namespace BaumansGateLibrary.Battle
{
    public class Game
    {
        public static BattleUI BattleInterface;
        Dictionary<char, Unit> FriendlyUnits = new Dictionary<char, Unit>();
        Dictionary<char, Unit> EnemyUnits = new Dictionary<char, Unit>();
        Player User;
        Grid MainLayout;
        public enum Actions
        {
            Move=1,
            Attack,
            Grab
        }
        public Game(Grid BattleLayout, Player user)
        {
            User = user;
            MainLayout = BattleLayout;
            FriendlyUnits = user.GetUnits();
            int AmountOfUnits = FriendlyUnits.Count;
            Random ran = new Random();
            for (int i = 0; i < AmountOfUnits; i++)
            {
                int type = ran.Next(0, 3);
                switch (type)
                {
                    case 0:
                        EnemyUnits.Add(i.ToString().ToCharArray()[0], new Earthlings(ran.Next(0, 3), i.ToString().ToCharArray()[0], new Position(0, i)));
                        break;
                    case 1:
                        EnemyUnits.Add(i.ToString().ToCharArray()[0], new Archers(ran.Next(0, 3), i.ToString().ToCharArray()[0], new Position(0, i)));
                        break;
                    case 2:
                        EnemyUnits.Add(i.ToString().ToCharArray()[0], new Riders(ran.Next(0, 3), i.ToString().ToCharArray()[0], new Position(0, i)));
                        break;
                }
            }
            Place(FriendlyUnits, MainLayout);
            Place();
        }
        public void Place(Dictionary<char, Unit> Units, Grid G)
        {
            foreach(char U in Units.Keys)
            {
                bool flag = true;
                BattleInterface.ShowGrid(G);
                while(flag)
                {
                    Position Next = BattleInterface.ChoosePosition(U);
                    bool FlagUnique = true;
                    foreach (char U2 in Units.Keys)
                        if (Next.xCoordinate == Units[U2].GetCurrentPosition().xCoordinate &&
                            Next.yCoordinate == Units[U2].GetCurrentPosition().yCoordinate)
                            FlagUnique = false;
                    if (FlagUnique)
                    {
                        Units[U].SetPosition(Next);
                        flag = false;
                    }
                    else
                        continue;
                }
            }
        }
        public void Place()
        {
            foreach (char U in FriendlyUnits.Keys)
                MainLayout[FriendlyUnits[U].GetCurrentPosition().xCoordinate, FriendlyUnits[U].GetCurrentPosition().yCoordinate] = FriendlyUnits[U].GetShortName();
            foreach (char U in EnemyUnits.Keys)
                MainLayout[EnemyUnits[U].GetCurrentPosition().xCoordinate, EnemyUnits[U].GetCurrentPosition().yCoordinate] = EnemyUnits[U].GetShortName();
        }
        public void StartGame()
        {
            bool PlayerTurnFlag = true;
            while (!EndOfGame())
            {
                BattleInterface.ShowUnitList(FriendlyUnits);
                BattleInterface.ShowLine();
                BattleInterface.ShowUnitList(EnemyUnits);
                BattleInterface.ShowLog();
                BattleInterface.ShowGrid(MainLayout);
                if (PlayerTurnFlag)
                {
                    PlayerTurn();
                    PlayerTurnFlag = false;
                }
                else
                {
                    BotTurn();
                    PlayerTurnFlag = true;
                }
            }
            if (PlayerWon())
                BattleInterface.ShowVictoryScreen();
            else
                BattleInterface.ShowDefeatedScreen();
            BattleInterface.ShowGrid(MainLayout);
            BattleInterface.ShowLog();
            Dictionary<char, Unit> NewPlayerUnits = new Dictionary<char, Unit>();
            foreach (char U in FriendlyUnits.Keys)
                if (FriendlyUnits[U].GetCurrentHealth() != 0)
                    NewPlayerUnits.Add(U, FriendlyUnits[U]);
            User.AddCycle();
            User.GenerallyUpdateUnits(NewPlayerUnits);
            BuffProcessor.ClearBuffs(NewPlayerUnits);
            GameLog.Clear();
        }
        public bool EndOfGame()
        {
            int SumHelthOfFriendly = 0;
            foreach (char U in FriendlyUnits.Keys)
                SumHelthOfFriendly += FriendlyUnits[U].GetCurrentHealth();
            int SumHelthOfEnemy = 0;
            foreach (char U in EnemyUnits.Keys)
                SumHelthOfEnemy += EnemyUnits[U].GetCurrentHealth();
            if (SumHelthOfFriendly == 0 || SumHelthOfEnemy == 0)
                return true;
            return false;
        }
        public bool PlayerWon()
        {
            int SumHelthOfEnemy = 0;
            foreach (char U in EnemyUnits.Keys)
                SumHelthOfEnemy += EnemyUnits[U].GetCurrentHealth();
            if (SumHelthOfEnemy == 0)
                return true;
            else
                return false;
        }
        public void PlayerTurn()
        {
            BuffProcessor.Process(FriendlyUnits);
            Unit ChosenUnit = BattleInterface.ChooseUnit(FriendlyUnits, true);
            BattleInterface.ShowMenu();
            Actions action = BattleInterface.ChooseAction();
            switch (action)
            {
                case Actions.Move:
                    Position Next = BattleInterface.ChoosePosition(ChosenUnit, MainLayout);
                    ChosenUnit.Move(Next, MainLayout);
                    break;
                case Actions.Attack:
                    Unit ChosenEnemyForAttack = BattleInterface.ChooseUnit(EnemyUnits, false);
                    AttackProcessor.Attack(ChosenUnit, ChosenEnemyForAttack, MainLayout);
                    break;
                case Actions.Grab:
                    Unit ChosenEnemyForRobbery = BattleInterface.ChooseUnit(EnemyUnits, true);
                    WeaponGrabProcessor.Grab(ChosenUnit, ChosenEnemyForRobbery);
                    break;
            }
        }
        public void BotTurn()
        {
            BuffProcessor.Process(EnemyUnits);
            Random ran = new Random();
            List<char> Keyss = new List<char>();
            foreach (char U in EnemyUnits.Keys)
                if (EnemyUnits[U].GetCurrentHealth() != 0)
                    Keyss.Add(U);
            Unit Chosen = EnemyUnits[Keyss[ran.Next(0, Keyss.Count)]];
            bool ChosenCanAttack = false;
            Unit Prey = new Unit();
            foreach (char U in FriendlyUnits.Keys)
            {
                if (AttackProcessor.CanAttack(Chosen, FriendlyUnits[U]))
                {
                    Prey = FriendlyUnits[U];
                    ChosenCanAttack = true;
                    break;
                }
            }
            if (ChosenCanAttack)
                AttackProcessor.Attack(Chosen, Prey, MainLayout);
            else
                for (int i = Chosen.GetMaximumDistanceOfMove(); i > 0; i--)
                    if (Chosen.Move(new Position(Chosen.GetCurrentPosition().xCoordinate, Chosen.GetCurrentPosition().yCoordinate + i), MainLayout))
                        break;
        }
    }
}