namespace Asu.Services.Catalog
{
    /// <summary>
    /// WC. BreadCrumb
    /// </summary>
    public class BreadCrumb
    {
        public string Controller { get; set; }
        public string SeName { get; set; }
        public string Name { get; set; }
        public bool IsLastBreadCrumb { get; set; }
    }
}
