using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using BaumansGateLibrary.Warriors;

namespace BaumansGateLibrary.Battle
{
    //статический класс, осуществляющий обработку атак
    public static class AttackProcessor
    {
        //метод проверки на возможность провести атаку
        public static bool CanAttack(Unit AttackingUnit, Unit DefencingUnit)
        {
            int RangeBetweenUnits = Grid.DistanceBetweenPoints(AttackingUnit.GetCurrentPosition(), DefencingUnit.GetCurrentPosition());
            if (DefencingUnit.GetCurrentHealth() != 0                                   //Если цель мертва - атаковать можно
                &&
                AttackingUnit.GetCurrentHealth() != 0                                   //Если атакующий мёртв - атаковать можно
                &&
                RangeBetweenUnits <= AttackingUnit.GetEquipedWeapon().RangeOfAttack)    //если позволяет дистанция
                return true;
            else
                return false;
        }
        //метод, непосредственно обрабатывающий атаку
        public static bool Attack(Unit AttackingUnit, Unit DefencingUnit, Grid MainLayout)
        {
            if (!CanAttack(AttackingUnit, DefencingUnit))
            {
                GameLog.AddLine(AttackingUnit.GetShortName().ToString() + " не дотянулся до " + DefencingUnit.GetShortName().ToString() + ", чтобы атаковать.\n");
                return false;
            }

            Random ran = new Random();
            int ChanceToAttack = ran.Next(1, 101);
            //проверяем, не уклонилась ли цель
            if (ChanceToAttack <= DefencingUnit.GetEquipedArmor().EvadeChance)
            {
                GameLog.AddLine(AttackingUnit.GetShortName().ToString() + " попытался атаковать " + DefencingUnit.GetShortName().ToString() + ", но тот увернулся от атаки.\n");
                return false;
            }
            else
            {
                int[] ProbableAttack = AttackingUnit.GetEquipedWeapon().GetProbableAttack();
                //вычисляем наносимый урон
                int ActualAttack = ran.Next(ProbableAttack[0], ProbableAttack[1] + 1);
                GameLog.AddLine(AttackingUnit.GetShortName().ToString() + " атаковал " + DefencingUnit.GetShortName().ToString() + " и нанёс ему " + ActualAttack.ToString() + " урона.\n");
                //если защита больше либо равна урону, то изменяем только показатель защиты
                if (DefencingUnit.GetEquipedArmor().Defence >= ActualAttack)
                    DefencingUnit.GetEquipedArmor().DefenceDecreaser(ActualAttack);
                //иначе отнимаем ещё и здоровье
                else
                {
                    ActualAttack -= DefencingUnit.GetEquipedArmor().Defence;
                    DefencingUnit.GetEquipedArmor().DefenceDecreaser(DefencingUnit.GetEquipedArmor().Defence);
                    DefencingUnit.HealthDecreaser(ActualAttack);
                    if (DefencingUnit.GetCurrentHealth() == 0)
                    {
                        GameLog.AddLine(AttackingUnit.GetShortName().ToString() + " убил " + DefencingUnit.GetShortName().ToString() + "!\n");
                        MainLayout[DefencingUnit.GetCurrentPosition().xCoordinate, DefencingUnit.GetCurrentPosition().yCoordinate] = '*';
                    }
                }
                return true;
            }
        }
    }
}
