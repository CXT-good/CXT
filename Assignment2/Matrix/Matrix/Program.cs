using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Matrix
{
    class Program
    {
        static bool IsToeplitzMatrix(int[,] matrix)
        {
            int rows = matrix.GetLength(0);
            int cols = matrix.GetLength(1);
            for(int i=0;i<rows-1;i++)
            {
                for(int j=0;j<cols-1;j++)
                {
                    if (matrix[i, j] != matrix[i + 1, j + 1]) return false;

                }
            }
            return true;
        }
        static void Main(string[] args)
        {
            Console.Write("请输入矩阵的行数：");
            int rows = int.Parse(Console.ReadLine());
            Console.Write("请输入矩阵的列数：");
            int cols = int.Parse(Console.ReadLine());
            int[,] matrix = new int[rows, cols];
            for(int i=0;i<rows;i++)
            {
                string[] line = Console.ReadLine().Split();//读取用户输入的一行，并按空格分割成字符串数组
                for(int j=0;j<cols;j++)
                {
                    matrix[i, j] = int.Parse(line[j]);

                }
            }
            Console.WriteLine("输入的矩阵是否是托普利茨矩阵：{0}",IsToeplitzMatrix(matrix));
        }
    }
}
