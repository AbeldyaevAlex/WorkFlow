using Asu.Framework.Mvc;

namespace Asu.Web.Models.Blogs
{
    public partial class BlogPostTagModel : BaseNopModel
    {
        public string Name { get; set; }

        public int BlogPostCount { get; set; }
    }
}