namespace Asu.Core.Domain.Vehicles
{
    public class PopularModel : BaseEntity
    {
        public int Year { get; set; }

        public int MakeId { get; set; }

        public int ModelId { get; set; }

        public int DisplayOrder { get; set; }
    }
}
