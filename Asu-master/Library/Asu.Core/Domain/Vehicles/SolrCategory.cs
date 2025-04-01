namespace Asu.Core.Domain.Vehicles
{
    public class SolrCategory : BaseEntity
    {
        public int ParentId { get; set; }

        public string Name { get; set; }

        public string ParentName { get; set; }

        public int Level { get; set; }
    }
}
