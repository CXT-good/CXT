using homework6;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

// 商品类
public class Product
{
    public string ProductId { get; set; } = Guid.NewGuid().ToString();
    public string Name { get; set; }
    public decimal Price { get; set; }

    public override string ToString()
    {
        return $"商品ID: {ProductId}, 名称: {Name}, 价格: {Price:C}";
    }
}

// 客户类
public class Customer
{
    public string CustomerId { get; set; }
    public string Name { get; set; }
    public string ContactInfo { get; set; }

    public override string ToString()
    {
        return $"客户ID: {CustomerId}, 姓名: {Name}, 联系方式: {ContactInfo}";
    }
}

// 订单明细类
public class OrderDetail
{
    private Product _product = new Product();
    public Product Product
    {
        get => _product;
        set => _product = value ?? new Product();
    }
    public int Quantity { get; set; }

    public decimal TotalAmount => Product.Price * Quantity;

    public override bool Equals(object obj)
    {
        if (obj == null || !(obj is OrderDetail other))
            return false;

        // 安全比较（处理Product为null的情况）
        return (Product?.ProductId == other.Product?.ProductId) &&
               (Quantity == other.Quantity);
    }

    public override int GetHashCode()
    {
        unchecked // 允许算术溢出
        {
            int hash = 17;
            hash = hash * 23 + (Product?.ProductId?.GetHashCode() ?? 0);
            hash = hash * 23 + Quantity.GetHashCode();
            return hash;
        }
    }

    public override string ToString()
    {
        return $"{Product} × {Quantity}, 小计: {TotalAmount:C}";
    }
}

// 订单类
public class Order : IComparable<Order>
{
    public string OrderId { get; set; }
    public Customer Customer { get; set; }
    public DateTime OrderDate { get; set; }
    public List<OrderDetail> Details { get; set; } = new List<OrderDetail>();

    public decimal TotalAmount => Details.Sum(d => d.TotalAmount);

    public override bool Equals(object obj)
    {
        if (obj == null || GetType() != obj.GetType())
            return false;

        Order other = (Order)obj;
        return OrderId == other.OrderId &&
               Customer.CustomerId == other.Customer.CustomerId &&
               Details.SequenceEqual(other.Details);
    }

    public override int GetHashCode()
    {
        unchecked // 允许算术溢出
        {
            int hash = 17;
            hash = hash * 23 + (OrderId?.GetHashCode() ?? 0);
            hash = hash * 23 + (Customer?.CustomerId?.GetHashCode() ?? 0);

            // 遍历 Details 集合计算哈希码
            foreach (var detail in Details)
            {
                hash = hash * 23 + (detail?.GetHashCode() ?? 0);
            }
            return hash;
        }
    }

    public int CompareTo(Order other)
    {
        if (other == null) return 1;
        return OrderId.CompareTo(other.OrderId);
    }

    public override string ToString()
    {
        string details = string.Join("\n  ", Details);
        return $"订单号: {OrderId}\n客户: {Customer}\n下单时间: {OrderDate}\n订单明细:\n  {details}\n总金额: {TotalAmount:C}";
    }
}

public class OrderService
{
    private List<Order> orders = new List<Order>();

    // 添加订单
    public void AddOrder(Order order)
    {
        if (order == null)
            throw new ArgumentNullException(nameof(order), "订单不能为空");

        if (orders.Any(o => o.OrderId == order.OrderId))
            throw new ArgumentException($"订单号 {order.OrderId} 已存在");

        orders.Add(order);
    }

    // 删除订单
    public void RemoveOrder(string orderId)
    {
        var order = orders.FirstOrDefault(o => o.OrderId == orderId);
        if (order == null)
            throw new ArgumentException($"订单号 {orderId} 不存在");

        orders.Remove(order);
    }

    // 修改订单
    public void UpdateOrder(Order order)
    {
        if (order == null)
            throw new ArgumentNullException(nameof(order), "订单不能为空");

        var existingOrder = orders.FirstOrDefault(o => o.OrderId == order.OrderId);
        if (existingOrder == null)
            throw new ArgumentException($"订单号 {order.OrderId} 不存在");

        orders.Remove(existingOrder);
        orders.Add(order);
    }

    // 查询所有订单
    public List<Order> GetAllOrders()
    {
        return orders.OrderBy(o => o.OrderId).ToList();
    }

    // 按订单号查询
    public List<Order> QueryByOrderId(string orderId)
    {
        return orders.Where(o => o.OrderId.Contains(orderId))
                     .OrderBy(o => o.TotalAmount)
                     .ToList();
    }

    // 按客户查询
    public List<Order> QueryByCustomer(string customerName)
    {
        return orders.Where(o => o.Customer.Name.Contains(customerName))
                     .OrderBy(o => o.TotalAmount)
                     .ToList();
    }

    // 按商品名称查询
    public List<Order> QueryByProduct(string productName)
    {
        return orders.Where(o => o.Details.Any(d => d.Product.Name.Contains(productName)))
                     .OrderBy(o => o.TotalAmount)
                     .ToList();
    }

    // 按金额范围查询
    public List<Order> QueryByAmountRange(decimal min, decimal max)
    {
        return orders.Where(o => o.TotalAmount >= min && o.TotalAmount <= max)
                     .OrderBy(o => o.TotalAmount)
                     .ToList();
    }

    // 排序方法
    public void SortOrders(Comparison<Order> comparison = null)
    {
        if (comparison == null)
            orders.Sort();
        else
            orders.Sort(comparison);
    }

    // 导出订单到文件
    public void ExportToFile(string filePath)
    {
        try
        {
            System.IO.File.WriteAllText(filePath, string.Join("\n\n", orders));
        }
        catch (Exception ex)
        {
            throw new Exception("导出文件失败: " + ex.Message);
        }
    }

    // 从文件导入订单
    public void ImportFromFile(string filePath)
    {
        // 实际项目中需要实现更复杂的导入逻辑
        throw new NotImplementedException();
    }
}

class Program
{
    [STAThread]
    static void Main(string[] args)
    {
        Application.EnableVisualStyles();
        Application.SetCompatibleTextRenderingDefault(false);
        Application.Run(new MainForm()); // 确保这里调用了你的主窗体
    }
}