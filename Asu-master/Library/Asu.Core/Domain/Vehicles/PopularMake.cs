namespace Asu.Core.Domain.Vehicles
{
    public class PopularMake : BaseEntity
    {
        public int Year { get; set; }

        public int MakeId { get; set; }

        public int DisplayOrder { get; set; }
    }
}
