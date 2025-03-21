using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tests
{
    [TestClass()]
    public class OrderDetailsTests
    {
        [TestMethod()]
        public void EqualsTest_ShouldReturnTrue_WhenDetailsAreIdentical()
        {
            var detail1 = new OrderDetails { ProductName = "Bool", Quantity = 2, Price = 10 };
            var detail2 = new OrderDetails { ProductName = "Book", Quantity = 2, Price = 10 };
            bool result = detail1.Equals(detail2);
            Assert.IsTrue(result);
        }
        [TestMethod()]
        public void EqualsTest_ShouldReturnFalse_WhenDetailsAreDifferent()
        {
            var detail1 = new OrderDetails { ProductName = "Book", Quantity = 2, Price = 10 };
            var detail2 = new OrderDetails { ProductName = "Pen", Quantity = 1, Price = 5 };
            bool result = detail1.Equals(detail2);
            Assert.IsFalse(result);
        }

        [TestMethod()]
        public void EqualsTest_ShouldReturnFalse_WhenComparedWithNull()
        {
            var detail1 = new OrderDetails { ProductName = "Book", Quantity = 2, Price = 10 };
            bool result = detail1.Equals(null);
            Assert.IsFalse(result);
        }

        [TestMethod()]
        public void EqualsTest_ShouldReturnFalse_WhenComparedWithDifferentType()
        {
            var detail1 = new OrderDetails { ProductName = "Book", Quantity = 2, Price = 10 };
            var otherObject = new object();
            bool result = detail1.Equals(otherObject);
            Assert.IsFalse(result);
        }

        [TestMethod()]
        public void ToStringTest_ShouldReturnCorrentFormat()
        {
            var detail1 = new OrderDetails { ProductName = "Book", Quantity = 2, Price = 10 };
            string expected = "Product:Book,Quantity:2,Price:10";
            string result = detail1.ToString();
            Assert.AreEqual(expected, result);
        }
    }
}
