using Asu.Core.Domain.Customers;
using System.ComponentModel.DataAnnotations.Schema;

namespace Asu.Data.Mapping.Customers
{
    public partial class CustomerMap : NopEntityTypeConfiguration<Customer>
    {
        public CustomerMap()
        {
            this.ToTable("Customer");
            this.HasKey(c => c.Id);
            //this.Property(c => c.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);
            this.Property(u => u.Username).HasMaxLength(1000);
            this.Property(u => u.Email).HasMaxLength(1000);

            this.Ignore(u => u.PasswordFormat);

            this.HasMany(c => c.CustomerRoles)
                .WithMany()
                .Map(m => m.ToTable("Customer_CustomerRole_Mapping"));

            this.HasMany(c => c.Addresses)
                .WithMany()
                .Map(m => m.ToTable("CustomerAddresses"));

            this.HasMany(c => c.UsersTask)
                .WithMany()
                .Map(m => m.ToTable("Customer_CustomerTask_Mapping"));

            this.HasMany(c => c.UsersWorkshop)
                .WithMany()
                .Map(m => m.ToTable("Customer_CustomerWorkshop_Mapping"));

            this.HasOptional(c => c.BillingAddress);
            this.HasOptional(c => c.ShippingAddress);

            this.HasMany(c => c.AmazonPaymentsAdvanced)
                .WithRequired(apa => apa.Customer);
        }
    }
}