using System.Collections.Generic;

namespace Asu.Web.Models.Boards
{
    public partial class ActiveDiscussionsModel
    {
        public ActiveDiscussionsModel()
        {
            ForumTopics = new List<ForumTopicRowModel>();
        }

        public IList<ForumTopicRowModel> ForumTopics { get; private set; }

        public bool ViewAllLinkEnabled { get; set; }

        public bool ActiveDiscussionsFeedEnabled { get; set; }

        public int PostsPageSize { get; set; }
    }
}