using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArrayMax
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("请输入整数数组的长度：");
            int length = int.Parse(Console.ReadLine());
            int[] a = new int[length];
            for(int i=0;i<length;i++)
            {
                Console.Write("请输入数组的第{0}个元素", i + 1);
                int temp = int.Parse(Console.ReadLine());
                a[i] = temp;
            }
            int maxn=int.MinValue;
            int minn=int.MaxValue;
            int sum = 0;
            double average ;
            for(int i=0;i<length;i++)
            {
                if (a[i] < minn) minn = a[i];
                if (a[i] > maxn) maxn = a[i];
                sum = sum + a[i];
            }
            average = (double)sum / length;
            Console.WriteLine("最大值：{0}", maxn);
            Console.WriteLine("最小值：{0}", minn);
            Console.WriteLine("平均值：{0}", average);
            Console.WriteLine("所有数组元素的总和：{0}", sum);
        }
    }
}
