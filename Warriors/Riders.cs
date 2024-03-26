using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace BaumansGateLibrary.Warriors
{
    [Serializable]
    public class Riders : Unit
    {
        public Riders(int type, char shortName, Position InitialPosition)
        {
            switch (type)
            {
                case 0:
                    Name = "Конный лучник";
                    ShortName = shortName;
                    MaximumDistanceOfMove = 5;
                    MaxHealth = 25;
                    CurrentHealth = 25;
                    Price = 25;
                    EquipedWeapon = new Weapon("Лук", 3, 1, 3);
                    EquipedArmor = new Armor("Кольчуга", 2, 30);
                    CurrentPosition = InitialPosition;
                    SwampPenalty = (float)2.2;
                    HillPenalty = (float)1.2;
                    TreePenalty = (float)1.5;
                    break;
                case 1:
                    Name = "Кирасир";
                    ShortName = shortName;
                    MaximumDistanceOfMove = 5;
                    MaxHealth = 50;
                    CurrentHealth = 50;
                    Price = 23;
                    EquipedWeapon = new Weapon("Палаш", 2, 2, 1);
                    EquipedArmor = new Armor("Укреплёные латы", 7, 10);
                    CurrentPosition = InitialPosition;
                    SwampPenalty = (float)2.2;
                    HillPenalty = (float)1.2;
                    TreePenalty = (float)1.5;
                    break;
                case 2:
                    Name = "Рыцарь";
                    ShortName = shortName;
                    MaximumDistanceOfMove = 6;
                    MaxHealth = 30;
                    CurrentHealth = 30;
                    Price = 20;
                    EquipedWeapon = new Weapon("Сабля", 5, 3, 1);
                    EquipedArmor = new Armor("Лёгкие латы", 3, 30);
                    CurrentPosition = InitialPosition;
                    SwampPenalty = (float)2.2;
                    HillPenalty = (float)1.2;
                    TreePenalty = (float)1.5;
                    break;
            }
        }
    }
}
