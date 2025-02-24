using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace homework2._3
{
    public partial class Form1 : Form
    {
        char @operator='\0';
        public Form1()
        {
            InitializeComponent();
        }
        
        private void button1_Click(object sender, EventArgs e)
        {
            @operator = '+';
        }

        private void button2_Click(object sender, EventArgs e)
        {
            @operator = '-';
        }

        private void button3_Click(object sender, EventArgs e)
        {
            @operator = '*';
        }

        private void button4_Click(object sender, EventArgs e)
        {
            @operator = '/';
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (double.TryParse(textBox1.Text, out double num1) &&
                double.TryParse(textBox2.Text, out double num2))
            {

                if (@operator != '\0')
                {
                    double result = PerformOperation(num1, num2, @operator);
                    label1.Text = "结果: " + result.ToString();
                }
                else
                {
                    MessageBox.Show("请选择运算符！");
                }
            }
            else
            {
                MessageBox.Show("请输入有效的数字！");
            }
        }
        private double PerformOperation(double num1, double num2, char @operator)
        {
            switch (@operator)
            {
                case '+':
                    return num1 + num2;
                case '-':
                    return num1 - num2;
                case '*':
                    return num1 * num2;
                case '/':
                    if (num2 == 0)
                    {
                        MessageBox.Show("除数不能为零！");
                        throw new DivideByZeroException(); // 这里实际上不会抛出，因为我们在显示消息后就退出了，但为了完整性还是加上。
                    }
                    return num1 / num2;
                default:
                    throw new ArgumentException("无效的运算符！");
            }
        }
    }
    }
