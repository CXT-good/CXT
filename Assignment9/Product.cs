using System;

namespace OrderApp
{
    public class Product
    {
        // 将属性名修改为符合 PascalCase 命名规范
        public int Id { get; set; }
        public string KindId { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }

        // 无参构造函数，保持类的实例化灵活性
        public Product()
        {
        }

        // 有参构造函数，用于初始化对象属性
        public Product(int id, string kindId, string name, double price)
        {
            Id = id;
            KindId = kindId;
            Name = name;
            Price = price;
        }

        // 重写 Equals 方法，用于比较两个 Product 对象是否相等
        public override bool Equals(object obj)
        {
            // 使用模式匹配简化类型检查和转换
            if (obj is not Product other)
            {
                return false;
            }
            return KindId == other.KindId && Name == other.Name;
        }

        // 重写 GetHashCode 方法，确保相等的对象具有相同的哈希码
        public override int GetHashCode()
        {
            // 使用 System.HashCode 简化哈希码的生成
            return System.HashCode.Combine(KindId, Name);
        }
    }
}