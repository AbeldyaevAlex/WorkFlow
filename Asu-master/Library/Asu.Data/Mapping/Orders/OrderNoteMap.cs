using Asu.Core.Domain.Orders;

namespace Asu.Data.Mapping.Orders
{
    public partial class OrderNoteMap : NopEntityTypeConfiguration<OrderNote>
    {
        public OrderNoteMap()
        {
            this.ToTable("OrderNote");
            this.HasKey(on => on.Id);
            this.Property(on => on.Note).IsRequired();

            this.HasRequired(on => on.Order)
                .WithMany(o => o.OrderNotes)
                .HasForeignKey(on => on.OrderId);
        }
    }
}