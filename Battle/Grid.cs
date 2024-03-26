using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BaumansGateLibrary.Warriors;

namespace BaumansGateLibrary.Battle
{
    /*
        * Объект хранит информацию об поле
        * 
        * пример поля и нумерация координат
        *  
        *  y
        *  
        *  14 *  *  *  *  *  *  *  *  *  *  *  *  *  *  *
        *  13 *  *  *  *  *  *  *  *  *  *  *  *  *  *  *
        *  12 *  *  !  !  *  *  *  *  *  *  *  *  *  *  *
        *  11 *  *  !  !  !  *  *  *  *  *  *  *  *  *  *
        *  10 *  *  *  *  *  *  *  *  *  *  *  *  *  *  *
        *  9  *  *  *  *  *  *  *  *  *  *  *  *  *  *  *
        *  8  *  *  *  *  *  *  *  *  *  *  *  *  *  *  *
        *  7  *  *  *  *  *  *  *  *  *  *  *  *  *  *  *
        *  6  *  *  *  *  *  #  *  #  *  #  *  *  *  *  *
        *  5  *  *  *  *  *  *  *  #  #  *  *  *  *  *  *
        *  4  *  *  *  *  @  *  *  *  *  *  *  *  *  *  *
        *  3  *  *  *  @  @  *  *  *  *  *  *  *  *  *  *
        *  2  *  *  @  @  @  *  *  *  *  *  *  *  *  *  *
        *  1  *  *  *  *  *  *  *  *  *  *  *  *  *  *  *
        *  0  *  *  *  *  *  *  *  *  *  *  *  *  *  *  *
        *     0  1  2  3  4  5  6  7  8  9  10 11 12 13 14  x
        *     
        *  Легенда карты:
        *  * - обычная клетка
        *  # - болото
        *  @ - холм
        *  ! - дерево
        *  1 2 3 - союзные юниты
        *  a b c - вражеские юниты
        */
    [Serializable]
    public class Grid
    {
        char[,] Layout = new char[15, 15];
        public Grid(char[,] layout)
        {
            Layout = layout;
        }
        public Grid()
        {
            //заполнение всего поля *
            for (int i = 0; i < 15; i++)
                for (int j = 0; j < 15; j++)
                    this[i, j] = '*';
            Random ran = new Random();
            //случайное заполнение преградами кроме первых и последних двух строк
            for (int y = 2; y < 13; y++)
            {
                int symbol = ran.Next(0, 3);
                char obstacle = '*';
                switch (symbol)
                {
                    case 0:
                        obstacle = '#';
                        break;
                    case 1:
                        obstacle = '@';
                        break;
                    case 2:
                        obstacle = '!';
                        break;
                }
                for (int i = 0; i < 5; i++)
                {
                    int x = ran.Next(0, 15);
                    this[x, y] = obstacle;
                }
            }
        }
        //индексатор для обращения к элементам поля
        public char this[int xCoordinate, int yCoordinate]
        {
            get
            {
                return Layout[14 - yCoordinate, xCoordinate];
            }
            set
            {
                Layout[14 - yCoordinate, xCoordinate] = value;
            }
        }
        //вычисление дистанции между точками
        public static int DistanceBetweenPoints(Position First, Position Second)
        {
            return (int)Math.Ceiling(
                        Math.Sqrt(
                            Math.Pow(First.xCoordinate - Second.xCoordinate, 2)
                            +
                            Math.Pow(First.yCoordinate - Second.yCoordinate, 2)));
        }
        public override string ToString()
        {
            string Output = "";
            for (int y = 14; y >= 0; y--) //генерируем строку
            {
                if (y < 10)
                    Output += " " + y;
                else
                    Output += y;
                Output += " ";
                for (int x = 0; x < 15; x++)
                    Output += this[x, y] + "  ";
                Output += "\n";
            }
            Output += "   0  1  2  3  4  5  6  7  8  9  10 11 12 13 14\n";
            return Output;
        }
        //метод для выставления юнита на поле
        public void Place(Unit U)
        {
            this[U.GetCurrentPosition().xCoordinate, U.GetCurrentPosition().yCoordinate] = U.GetShortName();
        }
    }
}
