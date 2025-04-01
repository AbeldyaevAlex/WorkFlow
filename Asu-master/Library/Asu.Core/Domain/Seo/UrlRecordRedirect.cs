namespace Asu.Core.Domain.Seo
{
    public class UrlRecordRedirect : BaseEntity
    {
        public int OldEntityId { get; set; }

        public int? NewEntityId { get; set; }

        public string EntityName { get; set; }

        public string OldSlug { get; set; }

        public string NewSlug { get; set; }

        public bool IsActive { get; set; }
    }
}
