using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace assf
{
    class Program
    {

        static void Main(string[] args)
        {
            Console.WriteLine("输出2-100的所有素数：");
            bool[] isPrime = new bool[101];
            for (int i = 2; i <= 100; i++)
            {
                isPrime[i] = true;
            }
            for(int i=2;i*i<=100;i++)
            {
                if (isPrime[i]==true)
                {
                    for(int j=i*i;j<=100;j=j+i)//i*i以前的值已经被标记过
                    {
                        isPrime[j] =false;
                    }
                }
            }
            for(int i=2;i<=100;i++)
            {
                if (isPrime[i] == true) Console.WriteLine(i);
            }
        }
    }
}
