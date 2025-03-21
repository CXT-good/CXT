
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class OrderDetails
{
    public string ProductName { get; set; }
    public int Quantity { get; set; }
    public decimal Price { get; set; }
    public override bool Equals(object obj)
    {
        if(obj is OrderDetails details)
        {
            return ProductName == details.ProductName && Quantity == details.Quantity && Price == details.Price;

        }
        return false;
    }
    public override string ToString()
    {
        return $"ProductName:{ProductName},Quantity:{Quantity},Price:{Price}";

    }
}

public class Order
{
    public int OrderId { get; set; }
    public string Customer { get; set; }
    public List<OrderDetails> Details { get; set; }
    public decimal TotalAmount => Details.Sum(d => d.Quantity * d.Price);
    public override bool Equals(object obj)
    {
        if(obj is Order order)
        {
            return OrderId == order.OrderId && Customer == order.Customer && Details.SequenceEqual(order.Details);

        }
        return false;
    }
    public override string ToString()
    {
        return $"OrderId:{OrderId},Customer:{Customer},Total Amount:{TotalAmount}\nDetails:\n{string.Join("\n", Details)}";
    }
}

public class OrderService
{
    private List<Order> orders = new List<Order>();

    public void AddOrder(Order order)
    {
        if(orders.Contains(order))
        {
            throw new InvalidOperationException("Order already exists.");
        }
        orders.Add(order);
    }
    public void RemoveOrder(int orderId)
    {
        var order = orders.FirstOrDefault(o => o.OrderId == orderId);
        if(order==null)
        {
            throw new InvalidOperationException("Order not found.");
        }
        orders.Remove(order);
    }
    public void UpdateOrder(Order order)
    {
        var existingOrder = orders.FirstOrDefault(o => o.OrderId == order.OrderId);
        if(existingOrder==null)
        {
            throw new InvalidOperationException("Order not found.");
        }
        orders.Remove(existingOrder);
        orders.Add(order);
    }

    public List<Order> QueryOrders(Func<Order,bool>predicate)
    {
        return orders.Where(predicate).OrderBy(o => o.TotalAmount).ToList();

    }
    public void SortOrders(Comparison<Order> comparison=null)
    {
        if(comparison==null)
        {
            orders.Sort((o1, o2) => o1.OrderId.CompareTo(o2.OrderId));
        }
        else
        {
            orders.Sort(comparison);
        }
    }
}

namespace _5_1
{
    class Program
    {
        static void Main(string[] args)
        {
            OrderService service = new OrderService();

            // 添加一些订单
            var order1 = new Order
            {
                OrderId = 1,
                Customer = "Alice",
                Details = new List<OrderDetails>
                {
                    new OrderDetails { ProductName = "Book", Quantity = 2, Price = 10 },
                    new OrderDetails { ProductName = "Pen", Quantity = 1, Price = 5 }
                }
            };

            var order2 = new Order
            {
                OrderId = 2,
                Customer = "Bob",
                Details = new List<OrderDetails>
                {
                    new OrderDetails { ProductName = "Notebook", Quantity = 3, Price = 8 }
                }
            };

            var order3 = new Order
            {
                OrderId = 3,
                Customer = "Alice",
                Details = new List<OrderDetails>
                {
                    new OrderDetails { ProductName = "Pencil", Quantity = 5, Price = 2 }
                }
            };

            service.AddOrder(order1);
            service.AddOrder(order2);
            service.AddOrder(order3);

            // 查询客户为 "Alice" 的订单并按总金额排序
            var aliceOrders = service.QueryOrders(o => o.Customer == "Alice");

            // 打印查询结果
            Console.WriteLine("Orders for Alice:");
            foreach (var order in aliceOrders)
            {
                Console.WriteLine(order);
                Console.WriteLine();
            }

            // 按订单总金额排序
            service.SortOrders((o1, o2) => o1.TotalAmount.CompareTo(o2.TotalAmount));

            // 打印排序后的所有订单
            Console.WriteLine("All orders sorted by total amount:");
            var allOrders = service.QueryOrders(o => true);
            foreach (var order in allOrders)
            {
                Console.WriteLine(order);
                Console.WriteLine();
            }
        }
    }
        }
