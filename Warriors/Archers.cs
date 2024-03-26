using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaumansGateLibrary.Warriors
{
    [Serializable]
    public class Archers : Unit
    {
        public Archers(int type, char shortName, Position InitialPosition)
        {
            switch (type)
            {
                case 0:
                    Name = "Лучник";
                    ShortName = shortName;
                    MaximumDistanceOfMove = 4;
                    MaxHealth = 25;
                    CurrentHealth = 25;
                    Price = 19;
                    EquipedWeapon = new Weapon("Короткий лук", 3, 2, 3);
                    EquipedArmor = new Armor("Облегчённая кольчуга", 4, 30);
                    CurrentPosition = InitialPosition;
                    SwampPenalty = (float)1.8;
                    HillPenalty = (float)2.2;
                    TreePenalty = 1;
                    break;
                case 1:
                    Name = "Лучник";
                    ShortName = shortName;
                    MaximumDistanceOfMove = 2;
                    MaxHealth = 30;
                    CurrentHealth = 30;
                    Price = 15;
                    EquipedWeapon = new Weapon("Длинный лук", 6, 1, 5);
                    EquipedArmor = new Armor("Кольчуга лучника", 8, 15);
                    CurrentPosition = InitialPosition;
                    SwampPenalty = (float)1.8;
                    HillPenalty = (float)2.2;
                    TreePenalty = 1;
                    break;
                case 2:
                    Name = "Арбалетчик";
                    ShortName = shortName;
                    MaximumDistanceOfMove = 2;
                    MaxHealth = 40;
                    CurrentHealth = 40;
                    Price = 23;
                    EquipedWeapon = new Weapon("Арбалет", 7, 0, 6);
                    EquipedArmor = new Armor("Кольчуга арбалетчика", 3, 10);
                    CurrentPosition = InitialPosition;
                    SwampPenalty = (float)1.8;
                    HillPenalty = (float)2.2;
                    TreePenalty = 1;
                    break;
            }
        }
    }
}