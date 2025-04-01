using Asu.Core.Domain.Metrology;

namespace Asu.Data.Mapping.Metrology
{
    public partial class KonservMap : NopEntityTypeConfiguration<Konserv>
    {
        public KonservMap()
        {
            this.ToTable("Konserv");
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
