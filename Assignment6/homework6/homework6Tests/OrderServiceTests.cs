using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

[TestClass]
public class OrderServiceTests
{
    private OrderService service;
    private Product product1, product2;
    private Customer customer1, customer2;

    [TestInitialize]
    public void Initialize()
    {
        service = new OrderService();

        product1 = new Product { ProductId = "P001", Name = "笔记本电脑", Price = 5999 };
        product2 = new Product { ProductId = "P002", Name = "鼠标", Price = 99 };

        customer1 = new Customer { CustomerId = "C001", Name = "张三", ContactInfo = "13800138000" };
        customer2 = new Customer { CustomerId = "C002", Name = "李四", ContactInfo = "13900139000" };
    }

    [TestMethod]
    public void TestAddOrder()
    {
        var order = new Order
        {
            OrderId = "O001",
            Customer = customer1,
            OrderDate = DateTime.Now,
            Details = new List<OrderDetail>
            {
                new OrderDetail { Product = product1, Quantity = 1 },
                new OrderDetail { Product = product2, Quantity = 2 }
            }
        };

        service.AddOrder(order);
        Assert.AreEqual(1, service.GetAllOrders().Count);
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentException))]
    public void TestAddDuplicateOrder()
    {
        var order1 = new Order { OrderId = "O001", Customer = customer1 };
        var order2 = new Order { OrderId = "O001", Customer = customer2 };

        service.AddOrder(order1);
        service.AddOrder(order2); // 应该抛出异常
    }

    [TestMethod]
    public void TestRemoveOrder()
    {
        var order = new Order { OrderId = "O001", Customer = customer1 };
        service.AddOrder(order);
        service.RemoveOrder("O001");
        Assert.AreEqual(0, service.GetAllOrders().Count);
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentException))]
    public void TestRemoveNonExistOrder()
    {
        service.RemoveOrder("O999"); // 应该抛出异常
    }

    [TestMethod]
    public void TestUpdateOrder()
    {
        var order = new Order { OrderId = "O001", Customer = customer1 };
        service.AddOrder(order);

        var updatedOrder = new Order { OrderId = "O001", Customer = customer2 };
        service.UpdateOrder(updatedOrder);

        var result = service.GetAllOrders().First();
        Assert.AreEqual("李四", result.Customer.Name);
    }

    [TestMethod]
    public void TestQueryByProduct()
    {
        var order1 = new Order
        {
            OrderId = "O001",
            Customer = customer1,
            Details = new List<OrderDetail> { new OrderDetail { Product = product1, Quantity = 1 } }
        };

        var order2 = new Order
        {
            OrderId = "O002",
            Customer = customer2,
            Details = new List<OrderDetail> { new OrderDetail { Product = product2, Quantity = 2 } }
        };

        service.AddOrder(order1);
        service.AddOrder(order2);

        var result = service.QueryByProduct("笔记本");
        Assert.AreEqual(1, result.Count);
        Assert.AreEqual("O001", result[0].OrderId);
    }

    [TestMethod]
    public void TestSortOrders()
    {
        var order1 = new Order { OrderId = "O002", Customer = customer1 };
        var order2 = new Order { OrderId = "O001", Customer = customer2 };

        service.AddOrder(order1);
        service.AddOrder(order2);

        // 默认按订单号排序
        service.SortOrders();
        Assert.AreEqual("O001", service.GetAllOrders()[0].OrderId);

        // 自定义按客户名排序
        service.SortOrders((x, y) => x.Customer.Name.CompareTo(y.Customer.Name));
        Assert.AreEqual("李四", service.GetAllOrders()[0].Customer.Name);
    }
}