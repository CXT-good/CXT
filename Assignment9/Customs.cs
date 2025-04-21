using System;

public class Class1
{
    public class Customer
    {
        public int id { get; set; }
        public string kindID { get; set; }
        public string Name { get; set; }

        public Customer()
        {
        }

        public Customer(string iD, string name)
        {
            kindID = iD;
            Name = name;
        }

        public override bool Equals(object obj)
        {
            var customer = obj as Customer;
            return customer != null &&
                   kindID == customer.kindID &&
                   Name == customer.Name;
        }

        public override int GetHashCode()
        {
            var hashCode = 1479869798;
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(kindID);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Name);
            return hashCode;
        }
    }
}
