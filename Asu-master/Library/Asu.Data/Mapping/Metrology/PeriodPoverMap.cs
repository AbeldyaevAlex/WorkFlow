using Asu.Core.Domain.Metrology;

namespace Asu.Data.Mapping.Metrology
{
    public partial class PeriodPoverMap : NopEntityTypeConfiguration<Period_pover>
    {
        public PeriodPoverMap() 
        {
            this.ToTable("Period_pover");
            this.HasKey(m => m.Id);

            this.HasRequired(a => a.Customer)
                .WithMany()
                .HasForeignKey(x => x.CustomerId)
                .WillCascadeOnDelete(false);

            this.HasRequired(a => a.DocumentStatus)
                .WithMany()
                .HasForeignKey(x => x.DocumentStatusId)
                .WillCascadeOnDelete(false);
        }
    }
}
