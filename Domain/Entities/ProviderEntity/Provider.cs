
using Domain.Core.Utils;
using Domain.Entities.ProductEntity;
using Domain.ValueObjects.ProviderValueObjects;

namespace Domain.Entities.ProviderEntity
{
     public class Provider : Entity<int>
    {
        public ProviderName ProviderName { get; private set; }
        public ProviderName ProviderLastName { get; private set; }
        public CI CI { get; private set; }
        public Number Number { get; private set; }
        public List<Product> Products { get; private set; }
        protected Provider() { }
        public Provider(string providerName, string providerLastName, string cI, string number,List<Product> products)
        {
            ProviderName = new ProviderName(ToTitleCase.TitleCase(providerName));
            ProviderLastName =new ProviderName(ToTitleCase.TitleCase(providerLastName));
            CI = new CI(cI);
            Number = new Number(number);
            Products = products;
        }

        public bool Update(string providerName, string providerLastName, string cI, string number)
        {
            ProviderName = new ProviderName(providerName);
            ProviderLastName = new ProviderName(providerLastName);
            CI = new CI(cI);
            Number = new Number(number);
            return true;
        }

        public bool AddProduct(Product product)
        {
            Products.Add(product);
            return true;
        }

        public bool RemoveProduct(Product product)
        {
            Products.Remove(product);
            return true;
        }
    }
 
}
