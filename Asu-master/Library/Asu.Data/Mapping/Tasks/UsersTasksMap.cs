using Asu.Core.Domain.Tasks;

namespace Asu.Data.Mapping.Tasks
{
    public partial class UsersTasksMap : NopEntityTypeConfiguration<UsersTasks>
    {
        public UsersTasksMap()
        {
            this.ToTable("UsersTasks");
            this.HasKey(m => m.Id);

            this.HasRequired(a => a.Customer).WithMany().HasForeignKey(x => x.CreatorId).WillCascadeOnDelete(false);
            this.HasRequired(a => a.DocumentStatus).WithMany().HasForeignKey(x => x.DocumentStatusId).WillCascadeOnDelete(false);
        }
    }
}
