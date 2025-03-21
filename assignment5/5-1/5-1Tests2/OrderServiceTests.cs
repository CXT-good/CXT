using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tests
{
    [TestClass()]
    public class OrderServiceTests
    {
        [TestMethod()]
        public void AddOrder_ShouldAddOrder_WhenOrderIsUnique()
        {
            // Arrange
            var service = new OrderService();
            var order = new Order { OrderId = 1, Customer = "Alice" };

            // Act
            service.AddOrder(order);

            // Assert
            var result = service.QueryOrders(o => o.OrderId == 1);
            Assert.AreEqual(1, result.Count);
        }

        [TestMethod()]
        [ExpectedException(typeof(InvalidOperationException))]
        public void AddOrder_ShouldThrowException_WhenOrderIsDuplicate()
        {
            // Arrange
            var service = new OrderService();
            var order = new Order { OrderId = 1, Customer = "Alice" };
            service.AddOrder(order);

            // Act
            service.AddOrder(order); // 重复添加
        }
        [TestMethod()]
        public void RemoveOrder_ShouldRemoveOrder_WhenOrderExists()
        {
            // Arrange
            var service = new OrderService();
            var order = new Order { OrderId = 1, Customer = "Alice" };
            service.AddOrder(order);

            // Act
            service.RemoveOrder(1);

            // Assert
            var result = service.QueryOrders(o => o.OrderId == 1);
            Assert.AreEqual(0, result.Count);
        }

        [TestMethod()]
        [ExpectedException(typeof(InvalidOperationException))]
        public void RemoveOrder_ShouldThrowException_WhenOrderDoesNotExist()
        {
            // Arrange
            var service = new OrderService();

            // Act
            service.RemoveOrder(999); // 删除不存在的订单
        }
        [TestMethod()]
        public void UpdateOrder_ShouldUpdateOrder_WhenOrderExists()
        {
            // Arrange
            var service = new OrderService();
            var order = new Order { OrderId = 1, Customer = "Alice" };
            service.AddOrder(order);

            var updatedOrder = new Order { OrderId = 1, Customer = "Bob" };

            // Act
            service.UpdateOrder(updatedOrder);

            // Assert
            var result = service.QueryOrders(o => o.OrderId == 1).Single();
            Assert.AreEqual("Bob", result.Customer);
        }

        [TestMethod()]
        [ExpectedException(typeof(InvalidOperationException))]
        public void UpdateOrder_ShouldThrowException_WhenOrderDoesNotExist()
        {
            // Arrange
            var service = new OrderService();
            var order = new Order { OrderId = 999, Customer = "Alice" };

            // Act
            service.UpdateOrder(order); // 更新不存在的订单
        }
        [TestMethod()]
        public void QueryOrders_ShouldReturnFilteredAndSortedOrders()
        {
            // Arrange
            var service = new OrderService();
            var order1 = new Order { OrderId = 1, Customer = "Alice", Details = new List<OrderDetails> { new OrderDetails { ProductName = "Book", Quantity = 2, Price = 10 } } };
            var order2 = new Order { OrderId = 2, Customer = "Bob", Details = new List<OrderDetails> { new OrderDetails { ProductName = "Pen", Quantity = 1, Price = 5 } } };
            var order3 = new Order { OrderId = 3, Customer = "Alice", Details = new List<OrderDetails> { new OrderDetails { ProductName = "Notebook", Quantity = 3, Price = 8 } } };

            service.AddOrder(order1);
            service.AddOrder(order2);
            service.AddOrder(order3);

            // Act
            var result = service.QueryOrders(o => o.Customer == "Alice");

            // Assert
            Assert.AreEqual(2, result.Count);
            Assert.AreEqual(3, result[0].OrderId); // 总金额 24
            Assert.AreEqual(1, result[1].OrderId); // 总金额 20
        }
        [TestMethod()]
        public void SortOrders_ShouldSortOrdersById_WhenNoComparisonIsProvided()
        {
            // Arrange
            var service = new OrderService();
            var order1 = new Order { OrderId = 2, Customer = "Alice" };
            var order2 = new Order { OrderId = 1, Customer = "Bob" };
            var order3 = new Order { OrderId = 3, Customer = "Charlie" };

            service.AddOrder(order1);
            service.AddOrder(order2);
            service.AddOrder(order3);

            // Act
            service.SortOrders();

            // Assert
            var result = service.QueryOrders(o => true);
            Assert.AreEqual(1, result[0].OrderId);
            Assert.AreEqual(2, result[1].OrderId);
            Assert.AreEqual(3, result[2].OrderId);
        }

        [TestMethod()]
        public void SortOrders_ShouldSortOrdersByTotalAmount_WhenComparisonIsProvided()
        {
            // Arrange
            var service = new OrderService();
            var order1 = new Order { OrderId = 1, Customer = "Alice", Details = new List<OrderDetails> { new OrderDetails { ProductName = "Book", Quantity = 2, Price = 10 } } };
            var order2 = new Order { OrderId = 2, Customer = "Bob", Details = new List<OrderDetails> { new OrderDetails { ProductName = "Pen", Quantity = 1, Price = 5 } } };
            var order3 = new Order { OrderId = 3, Customer = "Charlie", Details = new List<OrderDetails> { new OrderDetails { ProductName = "Notebook", Quantity = 3, Price = 8 } } };

            service.AddOrder(order1);
            service.AddOrder(order2);
            service.AddOrder(order3);

            // Act
            service.SortOrders((o1, o2) => o1.TotalAmount.CompareTo(o2.TotalAmount));

            // Assert
            var result = service.QueryOrders(o => true);
            Assert.AreEqual(2, result[0].OrderId); // 总金额 5
            Assert.AreEqual(1, result[1].OrderId); // 总金额 20
            Assert.AreEqual(3, result[2].OrderId); // 总金额 24
        }
    }
}