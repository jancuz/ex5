using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ex5
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Введите матрицу построчно через пробелы (целые числа): \n");
            string str = Console.ReadLine();

            int count = str.Trim().Split(' ').Length;  // по первой строке инициализируем порядок матрицы
            int[,] matrix = new int[count, count];     // инициализируем матрицу по полученным данным

            try
            {                                       // расщифровки       
                var nums = str.Trim().Split(' ');   // первая заполненная строка
                for (int i = 0; i < count; i++)
                {
                    if (!int.TryParse(nums[i], out matrix[0,i]))
                        throw new AggregateException("Неверно введена строка матрицы");
                }

                for (int i = 1; i < count; i++)     // дешифровка всех оставшихся строк
                {
                    str = Console.ReadLine();
                    nums = str.Trim().Split(' ');
                    if (nums.Length != count)
                        throw new AggregateException("Недостаток/избыток чисел в строке матрицы");
                    for (int j = 0; j < count; j++)
                    {
                        if (!int.TryParse(nums[j], out matrix[i, j]))
                            throw new AggregateException("Неверно введена строка матрицы");
                    }
                }
            }
            catch (AggregateException e)
            {
                Console.WriteLine(e.Message);       // неправильный ввод
                return;
            }

            int min = int.MaxValue;

            int pos1 = count / 2;   // строка - вершина треугольника
            int pos2 = pos1;        // столбец - вершина треугольника (левая граница)
            int pos3;               // столбец - вершина треугольника (правая граница)
            if (count % 2 != 0)     // если порядок матрицы нечетный
                pos3 = pos1;        // вершина треугольника одна
            else
                pos3 = pos1 + 1;    // две вершины при четном порядке матрицы
            
            // проход по нижнему треугольнику матрицы
            // поиск минимального элемента
            for (int i = pos1; i < count; i++)
                {
                    for (int j = pos2; j <= pos3; j++)
                    {
                        if (matrix[i,j] < min)
                            min = matrix[i,j];
                    }
                    pos2--;
                    pos3++;
                }

            Console.WriteLine("Минимальный элемент в нижнем треугольнике матрицы: {0}", min);
            Console.ReadLine();
        }
    }
}
