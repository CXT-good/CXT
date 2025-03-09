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

namespace _3_1
{
    class Program
    {
        static void Main(string[] args)
        {
            // 创建长方形对象
            Rectangle rectangle1 = new Rectangle(5, 3);
            Rectangle rectangle2 = new Rectangle(1, -1);
            Console.WriteLine($"Rectangle Area: {rectangle1.Area}, Is Valid: {rectangle1.IsValid}");
            Console.WriteLine($"Rectangle Area: {rectangle2.Area}, Is Valid: {rectangle2.IsValid}");

            // 创建正方形对象
            Square square1 = new Square(4);
            Square square2 = new Square(-1);
            Console.WriteLine($"Square Area: {square1.Area}, Is Valid: {square1.IsValid}");
            Console.WriteLine($"Square Area: {square2.Area}, Is Valid: {square2.IsValid}");
            // 创建三角形对象
            Triangle triangle1 = new Triangle(3, 4, 5);
            Triangle triangle2 = new Triangle(1,1,4);
            Console.WriteLine($"Triangle Area: {triangle1.Area}, Is Valid: {triangle1.IsValid}");
            Console.WriteLine($"Triangle Area: {triangle2.Area}, Is Valid: {triangle2.IsValid}");
        }
    }
}
