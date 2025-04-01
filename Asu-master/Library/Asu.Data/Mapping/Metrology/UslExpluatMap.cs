using Asu.Core.Domain.Metrology;

namespace Asu.Data.Mapping.Metrology
{
    public partial class UslExpluatMap : NopEntityTypeConfiguration<Usl_expluat>
    {
        public UslExpluatMap()
        {
            this.ToTable("Usl_expluat");
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
