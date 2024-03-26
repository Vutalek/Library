using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace BaumansGateLibrary.Warriors
{
    [Serializable]
    public class Earthlings : Unit
    {
        public Earthlings(int type, char shortName, Position InitialPosition)
        {
            switch (type)
            {
                case 0:
                    Name = "Топорщик";
                    ShortName = shortName;
                    MaximumDistanceOfMove = 4;
                    MaxHealth = 45;
                    CurrentHealth = 45;
                    Price = 20;
                    EquipedWeapon = new Weapon("Топор", 9, 3, 1);
                    EquipedArmor = new Armor("Лёгкие латы", 3, 12);
                    CurrentPosition = InitialPosition;
                    SwampPenalty = (float)1.5;
                    HillPenalty = 2;
                    TreePenalty = (float)1.2;
                    break;
                case 1:
                    Name = "Копейщик";
                    ShortName = shortName;
                    MaximumDistanceOfMove = 3;
                    MaxHealth = 35;
                    CurrentHealth = 35;
                    Price = 15;
                    EquipedWeapon = new Weapon("Копьё", 3, 1, 1);
                    EquipedArmor = new Armor("Латы копейщика", 4, 10);
                    CurrentPosition = InitialPosition;
                    SwampPenalty = (float)1.5;
                    HillPenalty = 2;
                    TreePenalty = (float)1.2;
                    break;
                case 2:
                    Name = "Мечник";
                    ShortName = shortName;
                    MaximumDistanceOfMove = 3;
                    MaxHealth = 50;
                    CurrentHealth = 50;
                    Price = 10;
                    EquipedWeapon = new Weapon("Меч", 5, 2, 1);
                    EquipedArmor = new Armor("Рыцарские Латы", 8, 10);
                    CurrentPosition = InitialPosition;
                    SwampPenalty = (float)1.5;
                    HillPenalty = 2;
                    TreePenalty = (float)1.2;
                    break;
            }
        }
    }
}