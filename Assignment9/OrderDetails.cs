using System;

using System;

namespace YourNamespace
{
    public class OrderDetail
    {
        // 序号
        public int ID { get; set; }

        public Product ProductItem { get; set; }

        // 使用属性的完整写法，增加注释说明
        /// <summary>
        /// 获取商品名称，如果商品项为空则返回空字符串
        /// </summary>
        public string ProductName
        {
            get
            {
                if (ProductItem != null)
                {
                    return ProductItem.Name;
                }
                return string.Empty;
            }
        }

        /// <summary>
        /// 获取商品单价，如果商品项为空则返回 0.0
        /// </summary>
        public double UnitPrice
        {
            get
            {
                if (ProductItem != null)
                {
                    return ProductItem.Price;
                }
                return 0.0;
            }
        }

        public int Quantity { get; set; }

        public OrderDetail()
        {
        }

        public OrderDetail(int index, Product goods, int quantity)
        {
            ID = index;
            ProductItem = goods;
            Quantity = quantity;
        }

        /// <summary>
        /// 获取订单详情的总价，如果商品项为空则返回 0.0
        /// </summary>
        public double TotalPrice
        {
            get
            {
                if (ProductItem == null)
                {
                    return 0.0;
                }
                return ProductItem.Price * Quantity;
            }
        }

        public override string ToString()
        {
            return $"[No.:{ID},product:{ProductName},quantity:{Quantity},totalPrice:{TotalPrice}]";
        }

        public override bool Equals(object obj)
        {
            // 使用 is 关键字进行类型检查，更简洁
            if (obj is OrderDetail item)
            {
                return ProductName == item.ProductName;
            }
            return false;
        }

        public override int GetHashCode()
        {
            // 使用 System.HashCode 来简化哈希码的生成
            return HashCode.Combine(ID, ProductName, Quantity);
        }
    }
}
