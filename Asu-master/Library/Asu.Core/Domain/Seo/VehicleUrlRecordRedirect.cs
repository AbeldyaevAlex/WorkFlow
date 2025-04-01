namespace Asu.Core.Domain.Seo
{
    public class VehicleUrlRecordRedirect : BaseEntity
    {
        public int OldEntityId { get; set; }

        public int? NewEntityId { get; set; }

        public string EntityName { get; set; }

        public int MakeId { get; set; }

        public int? YearId { get; set; }

        public int? ModelId { get; set; }

        public string OldSlug { get; set; }

        public string NewSlug { get; set; }
    }
}
