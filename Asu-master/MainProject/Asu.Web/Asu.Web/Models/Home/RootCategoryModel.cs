namespace Asu.Web.Models.Home
{
    using Asu.Framework.Mvc;

    public class HomePageGridEntity : BaseNopModel
    {
        public string Title { get; set; }

        public int Id { get; set; }

        public string Url { get; set; }

        public string CssClass { get; set; }
    }
}