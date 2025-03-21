using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tests
{
    [TestClass()]
    public class OrderTests
    {
        [TestMethod()]
        public void EqualsTest_ShouldReturnTrue_WhenOrdersAreIdentical()
        {
            // Arrange
            var order1 = new Order
            {
                OrderId = 1,
                Customer = "Alice",
                Details = new List<OrderDetails> { new OrderDetails { ProductName = "Book", Quantity = 2, Price = 10 } }
            };
            var order2 = new Order
            {
                OrderId = 1,
                Customer = "Alice",
                Details = new List<OrderDetails> { new OrderDetails { ProductName = "Book", Quantity = 2, Price = 10 } }
            };

            // Act
            bool result = order1.Equals(order2);

            // Assert
            Assert.IsTrue(result);
        }

        [TestMethod()]
        public void EqualsTest_ShouldReturnFalse_WhenOrdersAreDifferent()
        {
            // Arrange
            var order1 = new Order
            {
                OrderId = 1,
                Customer = "Alice",
                Details = new List<OrderDetails> { new OrderDetails { ProductName = "Book", Quantity = 2, Price = 10 } }
            };
            var order2 = new Order
            {
                OrderId = 2,
                Customer = "Bob",
                Details = new List<OrderDetails> { new OrderDetails { ProductName = "Pen", Quantity = 1, Price = 5 } }
            };

            // Act
            bool result = order1.Equals(order2);

            // Assert
            Assert.IsFalse(result);
        }

        [TestMethod()]
        public void EqualsTest_ShouldReturnFalse_WhenComparedWithNull()
        {
            // Arrange
            var order = new Order
            {
                OrderId = 1,
                Customer = "Alice",
                Details = new List<OrderDetails> { new OrderDetails { ProductName = "Book", Quantity = 2, Price = 10 } }
            };

            // Act
            bool result = order.Equals(null);

            // Assert
            Assert.IsFalse(result);
        }

        [TestMethod()]
        public void EqualsTest_ShouldReturnFalse_WhenComparedWithDifferentType()
        {
            // Arrange
            var order = new Order
            {
                OrderId = 1,
                Customer = "Alice",
                Details = new List<OrderDetails> { new OrderDetails { ProductName = "Book", Quantity = 2, Price = 10 } }
            };
            var otherObject = new object();

            // Act
            bool result = order.Equals(otherObject);

            // Assert
            Assert.IsFalse(result);
        }
    }
}