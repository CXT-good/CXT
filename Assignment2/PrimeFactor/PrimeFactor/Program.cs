
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrimeFactor
{
    class Program
    {
        static bool IsPrime(int number)
        {
            if (number <= 1) return false;
            else
            {
                for(int i=2;i*i<=number;i++)
                {
                    if (number % i == 0) return false;
                }
                return true;
            }
        }
        static void GetPrimeFactors(int n)
        {
            Console.WriteLine("{0}的所有素数因子是：", n);
            for(int i=2;i<=n;i++)
            {
                while(n%i==0&&IsPrime(i))
                {
                    Console.WriteLine(i);
                    n = n / i;
                }
            }
        }
        static void Main(string[] args)
        {
            Console.WriteLine("请输入一个数字：");
                int number = int.Parse(Console.ReadLine());
            GetPrimeFactors(number);
        }
    }
}
