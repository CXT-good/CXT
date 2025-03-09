using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System;

public interface IShape
{
    double Area { get; }
    bool IsValid { get; }
}

// 定义一个抽象形状类，实现 IShape 接口
public abstract class Shape : IShape
{
    public abstract double Area { get; }
    public abstract bool IsValid { get; }
}

// 长方形类，继承自 Shape 抽象类
public class Rectangle : Shape
{
    public double Length { get; set; }
    public double Width { get; set; }

    public Rectangle(double length, double width)
    {
        Length = length;
        Width = width;
    }

    public override double Area
    {
        get
        {
            if (IsValid)
            {
                return Length * Width;
            }
            return 0;
        }
    }

    public override bool IsValid
    {
        get
        {
            return Length > 0 && Width > 0;
        }
    }
}

// 正方形类，继承自 Shape 抽象类
public class Square : Shape
{
    public double Side { get; set; }

    public Square(double side)
    {
        Side = side;
    }

    public override double Area
    {
        get
        {
            if (IsValid)
            {
                return Side * Side;
            }
            return 0;
        }
    }

    public override bool IsValid
    {
        get
        {
            return Side > 0;
        }
    }
}

// 三角形类，继承自 Shape 抽象类
public class Triangle : Shape
{
    public double SideA { get; set; }
    public double SideB { get; set; }
    public double SideC { get; set; }

    public Triangle(double sideA, double sideB, double sideC)
    {
        SideA = sideA;
        SideB = sideB;
        SideC = sideC;
    }

    public override double Area
    {
        get
        {
            if (IsValid)
            {
                double s = (SideA + SideB + SideC) / 2;
                return Math.Sqrt(s * (s - SideA) * (s - SideB) * (s - SideC));
            }
            return 0;
        }
    }

    public override bool IsValid
    {
        get
        {
            return SideA > 0 && SideB > 0 && SideC > 0 &&
                   SideA + SideB > SideC &&
                   SideA + SideC > SideB &&
                   SideB + SideC > SideA;
        }
    }
}

public class ShapeFactory
{
    // 静态实例确保在整个程序运行期间只创建一个 Random 对象，避免每次调用时生成相似的随机数序列
    private static Random random = new Random();//创建一个Random类的静态实例，用于生成随机数
    //用于随机创建一个形状对象
    public static Shape CreateRandomShape()
    {
        //生成一个0到2之间的随机整数
        //0代表长方形，1代表正方形，2代表三角形
        int shapeType = random.Next(3);
        switch(shapeType)
        {
            case 0:
                // random.NextDouble() 生成一个 0.0 到 1.0 之间的随机小数
                // 乘以 10 后范围变为 0.0 到 10.0，再加上 1 范围变为 1.0 到 11.0
                double length = random.NextDouble() * 10 + 1;
                double width = random.NextDouble() * 10 + 1;
                return new Rectangle(length, width);
            case 1:
                double side = random.NextDouble() * 10 + 1;
                return new Square(side);
            case 2:
                double sideA, sideB, sideC;
                do
                {
                    sideA = random.NextDouble() * 10 + 1;
                    sideB = random.NextDouble() * 10 + 1;
                    sideC = random.NextDouble() * 10 + 1;
                } while (sideA + sideB <= sideC || sideA + sideC <= sideB || sideB + sideC <= sideA);
                return new Triangle(sideA, sideB, sideC);
            default:
                throw new ArgumentException("无效的形状类型");
        }
    }    
}

namespace _3_1
{
    class Program
    {
        static void Main(string[] args)
        {
            double totalArea = 0;
            for(int i=0;i<10;i++)
            {
                try
                {
                    Shape shape = ShapeFactory.CreateRandomShape();
                    totalArea += shape.Area;
                    Console.WriteLine($"第{i + 1}个形状的面积：{shape.Area}");
                }
                catch(ArgumentException ex)
                {
                    Console.WriteLine($"创建形状时出错：{ex.Message}");
                }
            }
            Console.WriteLine($"10个形状的面积之和：{totalArea}");
        }
    }
}
