using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaumansGateLibrary.Warriors
{
    //Объектом данного класса является оружние юнита
    [Serializable]
    public struct Weapon
    {
        int AverageAttack;                              //средняя атака оружия
        int DispersionOfAttack;                         //максимальное отклонение атаки от среднего
        public int RangeOfAttack { get; private set; }  //дальность атаки
        public string Name { get; private set; }        //наименование оружия
        public Weapon(string name, int averageAttack, int dispersionOfAttack, int rangeOfAttack)
        {
            Name = name;
            RangeOfAttack = rangeOfAttack;
            DispersionOfAttack = dispersionOfAttack;
            if (dispersionOfAttack < averageAttack)
                AverageAttack = averageAttack;         //если отклонение меньше среднего, то всё хорошо
            else
                AverageAttack = dispersionOfAttack;    //если нет, то, чтобы избеежать отрицательной атаки, принимаем дисперсию за среднее.
        }
        public int GetAverageAttack() {  return AverageAttack; }
        public int GetDispersionOfAttack() {  return DispersionOfAttack; }
        //метод вычисляет диапазон вохможной атаки
        public int[] GetProbableAttack()
        {
            return new int[] { AverageAttack - DispersionOfAttack, AverageAttack + DispersionOfAttack };
        }
        public void IncreaseAttack()
        {
            if (DispersionOfAttack != 0)
                DispersionOfAttack -= 1;
            if (AverageAttack < 15)
                AverageAttack += 1;
        }
        public void ChangeAverage(int amount)
        {
            AverageAttack += amount;
        }
    }
}