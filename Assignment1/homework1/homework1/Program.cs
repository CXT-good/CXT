using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace homework1
{
    class Program
    {
        static void Main(string[] args)
        {
            // 提示用户输入第一个数字
            Console.Write("请输入第一个数字: "); // 使用Console类的Write方法向控制台输出提示信息，不换行。
            string input1 = Console.ReadLine(); // 读取用户输入的一行文本，并存储在字符串变量input1中。
            double num1; // 声明一个double类型的变量num1，用于存储转换后的数字。
            while (!double.TryParse(input1, out num1)) // 使用TryParse方法尝试将input1转换为double类型，如果转换失败，则进入循环。
            {
                Console.Write("输入无效，请输入一个有效的数字: "); // 输出错误信息，提示用户重新输入。
                input1 = Console.ReadLine(); // 再次读取用户输入。
            }

            // 提示用户输入运算符
            Console.Write("请输入运算符 (+, -, *, /): "); // 输出提示信息。
            char operatorChar = Console.ReadKey().KeyChar; // 读取用户按下的单个键，并获取其字符值作为运算符。
            Console.WriteLine(); // 输出一个空行，用于格式化输出。

            // 提示用户输入第二个数字
            // 这部分代码与输入第一个数字的代码类似，因此不再逐行解释。
            Console.Write("请输入第二个数字: ");
            string input2 = Console.ReadLine();
            double num2;
            while (!double.TryParse(input2, out num2))
            {
                Console.Write("输入无效，请输入一个有效的数字: ");
                input2 = Console.ReadLine();
            }

            // 根据运算符计算结果
            double result = 0; // 声明一个double类型的变量result，用于存储计算结果。
            bool validOperation = true; // 声明一个bool类型的变量validOperation，用于标记操作是否有效。
            switch (operatorChar) // 使用switch语句根据运算符执行不同的计算。
            {
                case '+': // 如果运算符是加号，则执行加法运算。
                    result = num1 + num2;
                    break;
                case '-': // 如果运算符是减号，则执行减法运算。
                    result = num1 - num2;
                    break;
                case '*': // 如果运算符是乘号，则执行乘法运算。
                    result = num1 * num2;
                    break;
                case '/': // 如果运算符是除号，则执行除法运算前需要检查除数是否为零。
                    if (num2 == 0) // 检查第二个数字是否为零。
                    {
                        Console.WriteLine("错误: 除数不能为零。"); // 如果除数为零，则输出错误信息。
                        validOperation = false; // 将validOperation设置为false，标记操作无效。
                    }
                    else
                    {
                        result = num1 / num2; // 如果除数不为零，则执行除法运算。
                    }
                    break;
                default: // 如果运算符不是上述四个之一，则执行default分支。
                    Console.WriteLine("错误: 无效的运算符。"); // 输出错误信息。

                    validOperation = false; // 将validOperation设置为false，标记操作无效。
                    break;
            }

            // 如果操作有效，打印结果
            if (validOperation) // 检查validOperation是否为true。
            {
                Console.WriteLine($"结果: {num1} {operatorChar} {num2} = {result}"); // 如果操作有效，则输出计算结果。
            }

            // 等待用户按键后退出
            Console.WriteLine("按任意键退出..."); // 输出提示信息。
            Console.ReadKey(); // 等待用户按下任意键。
        }
    }
}
