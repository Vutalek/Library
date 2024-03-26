using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaumansGateLibrary.Warriors
{
    // Объект данной структуры представляет собой броню юнита
    [Serializable]
    public struct Armor
    {
        public string Name { get; private set; }                //наименование брони
        public int Defence { get; private set; }                //показатель защиты брони
        public int EvadeChance { get; private set; }            //шанс уклонения от атаки, указывается в %
        public Armor(string name, int defence, int evadeChance)
        {
            Name = name;
            Defence = defence;
            EvadeChance = evadeChance;
        }
        public int GetDefence() {  return Defence; }
        public void DefenceDecreaser(int Decreaser)             //метод для уменьшения показателя защиты про получении урона
        {
            Defence -= Decreaser;
        }
        public void IncreaseDefence()
        {
            Defence += 1;
        }
    }
}