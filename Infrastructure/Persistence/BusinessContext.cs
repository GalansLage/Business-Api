using Domain.Entities.DailySalesReportEntity;
using Domain.Entities.ProductEntity;
using Domain.Entities.ProductItemEntity;
using Domain.Entities.ProductTransactionEntity;
using Domain.Entities.ProviderEntity;
using Domain.ValueObjects.MoneyValueObjects;
using Domain.ValueObjects.ProductValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
namespace Infrastructure.Persistence
{
    public class BusinessContext:DbContext
    {
        public BusinessContext(DbContextOptions<BusinessContext> options) : base(options) { }

        public DbSet<Product> Products { get; set; }
        public DbSet<ProductTransaction> ProductTransactions { get; set; }
        public DbSet<Provider> Providers { get; set; }
        public DbSet<ProductItem> ProductItems { get; set; }
        public DbSet<DailySalesReport> DailySalesReports { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<Product>().ToTable("Product");
            modelBuilder.Entity<ProductTransaction>().ToTable("ProductTransaction");
            modelBuilder.Entity<Provider>().ToTable("Provider");
            modelBuilder.Entity<ProductItem>().ToTable("ProductItem");
            modelBuilder.Entity<DailySalesReport>().ToTable("DailySalesReport");


            modelBuilder.Entity<DailySalesReport>(daily =>
                daily.HasQueryFilter(dsr => !dsr.IsDeleted)
            );

            modelBuilder.Entity<ProductItem>(productItem =>
            {
                productItem.HasQueryFilter(pi => !pi.IsDeleted);

                productItem.Property(p => p.ProductCode).HasConversion(
                    product_code => product_code.Code,
                    value => new ProductCode(value)
                    );
                productItem.Property(p => p.InTime).HasConversion(
                    in_time => in_time.InTimeVO,
                    value => new InTime(value)
                    );

                productItem.Property(p => p.ProductState).HasConversion(
                    product_state => product_state.State,
                    value => new ProductState(value)
                    );

                productItem.HasIndex(pi => pi.ProductCode);

            });

            modelBuilder.Entity<Product>(product =>
            {
                product.HasQueryFilter(p => !p.IsDeleted);
                product.Property(p => p.Id).ValueGeneratedOnAdd();

                product.Property(p => p.ProductName).HasConversion(
                    product_name => product_name.Name,
                    value => new ProductName(value)
                    ).
                    HasColumnName("ProductName")
                    .IsRequired();
                
                product.OwnsOne(p => p.Price, priceBuilder =>
                {
                    priceBuilder.OwnsOne(price => price.PriceVO, moneyBuilder =>
                    {
                        moneyBuilder.Property(m => m.Amaunt).HasColumnName("price_amount");
                        moneyBuilder.Property(m => m.CurrencyVO).HasConversion(c => c.CurrencyVO, currency =>
                        new Currency(currency))
                        .HasColumnName("price_currency")
                        .HasMaxLength(3);
                    });
                });
                product.OwnsOne(p => p.Cost, costBuilder =>
                {
                    costBuilder.OwnsOne(price => price.CostVO, moneyBuilder =>
                    {
                        moneyBuilder.Property(m => m.Amaunt).HasColumnName("cost_amount");
                        moneyBuilder.Property(m => m.CurrencyVO).HasConversion(c => c.CurrencyVO, currency =>
                        new Currency(currency))
                        .HasColumnName("cost_currency")
                        .HasMaxLength(3);
                    });
                });

                product.Property(p => p.Category).HasConversion(
                    category => category.CategoryVO,
                    value => new Category(value)
                    );
                product.HasIndex("ProductName").IsUnique();

            });

            modelBuilder.Entity<ProductTransaction>(productTransaction => {

                productTransaction.HasQueryFilter(pt => !pt.IsDeleted);
                productTransaction.Property(p => p.Id).ValueGeneratedOnAdd();

                productTransaction.Property(pt => pt.Amount).HasConversion(
                    amount=>amount.AmountVO,
                    value=>new Domain.ValueObjects.ProductTransactionValueObject.Amount(value)
                    );

                productTransaction.Property(pt => pt.Date).HasConversion(
                    date => date.InTimeVO,
                    value => new InTime(value)
                    );
            });

            modelBuilder.Entity<Provider>(provider => {

                provider.HasQueryFilter(pr => !pr.IsDeleted);
                provider.Property(p => p.Id).ValueGeneratedOnAdd();

                provider.Property(pr => pr.ProviderName).HasConversion(
                    provider_name => provider_name.Name,
                    value => new Domain.ValueObjects.ProviderValueObjects.ProviderName(value)
                    );

                provider.Property(pr => pr.ProviderLastName).HasConversion(
                    provider_last_name => provider_last_name.Name,
                    value => new Domain.ValueObjects.ProviderValueObjects.ProviderName(value)
                    );

                provider.Property(pr => pr.CI).HasConversion(
                    ci => ci.CIVO,
                    value => new Domain.ValueObjects.ProviderValueObjects.CI(value)
                    );

                provider.Property(pr => pr.Number).HasConversion(
                    number => number.NumberVO,
                    value => new Domain.ValueObjects.ProviderValueObjects.Number(value)
                    );
            });
        }
    }
    
}
