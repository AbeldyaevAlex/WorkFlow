using Asu.Core.Domain.Messages;

namespace Asu.Data.Mapping.Messages
{
    public partial class SendGridMessageTemplateMap : NopEntityTypeConfiguration<SendGridMessageTemplate>
    {
        public SendGridMessageTemplateMap()
        {
            this.ToTable("WCS_SendGridTemplate");
            this.HasKey(mt => mt.Id);

            this.Property(mt => mt.Name).IsRequired().HasMaxLength(255);
            this.Property(mt => mt.TemplateId).IsRequired().HasMaxLength(500);
            this.Property(mt => mt.Email).IsRequired().HasMaxLength(255);
            this.Property(mt => mt.StoreId).IsRequired();
        }
    }
}