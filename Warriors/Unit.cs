using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using BaumansGateLibrary.Battle;
using BaumansGateLibrary.Buffs;

namespace BaumansGateLibrary.Warriors
{
    [Serializable]
    public class Unit : IMove
    {
        protected string Name;
        protected char ShortName;
        protected int CurrentHealth;
        protected int MaxHealth;
        protected Weapon EquipedWeapon;
        protected Armor EquipedArmor;
        protected int MaximumDistanceOfMove;
        protected int Price;
        protected Position CurrentPosition;
        protected float SwampPenalty;
        protected float HillPenalty;
        protected float TreePenalty;
        protected List<Buff> BuffList;
        public enum HowChange
        {
            Decrease,
            Increase
        }
        public Unit(string Name, char ShortName, int Health, Weapon EquipedWeapon, Armor EquipedArmor, int MaximumDistanceOfMove, int Price, Position pos)
        {
            this.Name = Name;
            this.ShortName = ShortName;
            MaxHealth = Health;
            CurrentHealth = Health;
            this.EquipedWeapon = EquipedWeapon;
            this.EquipedArmor = EquipedArmor;
            this.MaximumDistanceOfMove = MaximumDistanceOfMove;
            this.Price = Price;
            CurrentPosition = pos;
            SwampPenalty = 1;
            HillPenalty = 1;
            TreePenalty = 1;
        }
        public Unit() { BuffList = new List<Buff>(); }
        public Weapon GetEquipedWeapon() { return EquipedWeapon; }
        public ref Armor GetEquipedArmor() { return ref EquipedArmor; }
        public int GetCurrentHealth() { return CurrentHealth; }
        public int GetMaxHealth() { return MaxHealth; }
        public Position GetCurrentPosition() { return CurrentPosition; }
        public char GetShortName() { return ShortName; }
        public int GetMaximumDistanceOfMove() { return MaximumDistanceOfMove; }
        public int GetPrice() { return Price; }
        public List<Buff> GetBuffList() { return BuffList; }
        public void ChangeMaximumDistanceOfMove(int amount, HowChange H)
        {
            switch(H)
            {
                case HowChange.Decrease:
                    MaximumDistanceOfMove -= amount;
                    break;
                case HowChange.Increase:
                    MaximumDistanceOfMove += amount;
                    break;
            }
        }
        public void HealthDecreaser(int Decreaser)
        {
            if (Decreaser >= CurrentHealth)
                CurrentHealth = 0;
            else
                CurrentHealth -= Decreaser;
        }
        public void Heal(int Amount)
        {
            CurrentHealth += Amount;
            if (CurrentHealth > MaxHealth)
                CurrentHealth = MaxHealth;
        }
        public void IncreaseMaxHealth(int Amount)
        {
            MaxHealth += Amount;
        }
        public void WeaponChange(Weapon NewWeapon)
        {
            EquipedWeapon = NewWeapon;
        }
        public void ArmorChange(Armor NewArmor)
        {
            EquipedArmor= NewArmor;
        }
        public void SetPosition(Position pos)
        {
            CurrentPosition = pos;
        }
        public bool CanMove(Position NextPosition, Grid MainLayout)
        {
            if (CurrentHealth == 0)
                return false;
            if (NextPosition.yCoordinate > 14 || NextPosition.yCoordinate < 0 || NextPosition.xCoordinate > 14 || NextPosition.xCoordinate < 0)
                return false;
            if (MainLayout[NextPosition.xCoordinate, NextPosition.yCoordinate] != '*')
                return false;
            if ((CurrentPosition.xCoordinate == NextPosition.xCoordinate) == (CurrentPosition.yCoordinate == NextPosition.yCoordinate))
                return false;
            if (CurrentPosition.xCoordinate == NextPosition.xCoordinate)
            {
                int MinIndex = Math.Min(CurrentPosition.yCoordinate, NextPosition.yCoordinate);
                int MaxIndex = Math.Max(CurrentPosition.yCoordinate, NextPosition.yCoordinate);
                float CostOfPath = 0;
                for (int i = MinIndex; i < MaxIndex + 1; i++)
                {
                    char CellInLayout = MainLayout[NextPosition.xCoordinate, i];
                    switch (CellInLayout)
                    {
                        case '*':
                            CostOfPath++;
                            break;
                        case '#':
                            CostOfPath += SwampPenalty;
                            break;
                        case '@':
                            CostOfPath += HillPenalty;
                            break;
                        case '!':
                            CostOfPath += TreePenalty;
                            break;
                    }
                }
                int CalculatedPath = (int)Math.Ceiling(CostOfPath);
                if (CalculatedPath > MaximumDistanceOfMove)
                    return false;
            }
            else
            {
                int MinIndex = Math.Min(CurrentPosition.xCoordinate, NextPosition.xCoordinate);
                int MaxIndex = Math.Max(CurrentPosition.xCoordinate, NextPosition.xCoordinate);
                float CostOfPath = 0;
                for (int i = MinIndex; i < MaxIndex + 1; i++)
                {
                    char CellInLayout = MainLayout[i, NextPosition.yCoordinate];
                    switch (CellInLayout)
                    {
                        case '*':
                            CostOfPath++;
                            break;
                        case '#':
                            CostOfPath += SwampPenalty;
                            break;
                        case '@':
                            CostOfPath += HillPenalty;
                            break;
                        case '!':
                            CostOfPath += TreePenalty;
                            break;
                    }
                }
                int CalculatedPath = (int)Math.Floor(CostOfPath);
                if (CalculatedPath > MaximumDistanceOfMove)
                    return false;
            }
            return true;
        }
        public bool Move(Position NextPosition, Grid MainLayout)
        {
            if (CanMove(NextPosition, MainLayout))
            {
                GameLog.AddLine(ShortName.ToString() + " переместисля из клетки (" + CurrentPosition.xCoordinate + ", " + CurrentPosition.yCoordinate + ") в клетку (" + NextPosition.xCoordinate + ", " + NextPosition.yCoordinate + ").\n");
                MainLayout[CurrentPosition.xCoordinate, CurrentPosition.yCoordinate] = '*';
                MainLayout[NextPosition.xCoordinate, NextPosition.yCoordinate] = ShortName;
                CurrentPosition = NextPosition;
                return true;
            }
            return false;
        }
        public override string ToString()
        {
            string OutputInfo = "";
            OutputInfo += "Информация об юните " + Name + ":\n";
            OutputInfo += "Состояние: " + (CurrentHealth == 0 ? "Мёртв" : "Жив") + "\n";
            OutputInfo += "Здоровье: " + CurrentHealth + "/" + MaxHealth + "\n";
            OutputInfo += "Максимальная дистанция для перемещения: " + MaximumDistanceOfMove + "\n";
            OutputInfo += "Название оружия: " + EquipedWeapon.Name + "\n";
            OutputInfo += "Атака: " + EquipedWeapon.GetProbableAttack()[0] + "-" + EquipedWeapon.GetProbableAttack()[1] + "\n";
            OutputInfo += "Дальность атаки: " + EquipedWeapon.RangeOfAttack + "\n";
            OutputInfo += "Название брони: " + EquipedArmor.Name + "\n";
            OutputInfo += "Защита: " + EquipedArmor.Defence + "\n";
            OutputInfo += "Шанс уклонения: " + EquipedArmor.EvadeChance + "%" + "\n";
            return OutputInfo;
        }
        public string ShortInfo()
        {
            string Output = Name + " " + ShortName + '\n';
            Output += "Защита: " + EquipedArmor.Defence + '\n';
            Output += "Здоровье: " + CurrentHealth + '/' + MaxHealth + '\n';
            return Output;
        }
    }
}