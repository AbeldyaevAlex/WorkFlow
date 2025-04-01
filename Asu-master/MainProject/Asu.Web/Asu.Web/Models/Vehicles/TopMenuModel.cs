using System.Collections.Generic;
using Asu.Framework.Mvc;

namespace Asu.Web.Models.Vehicles
{
    public class TopMenuModel : BaseNopModel
    {
        public TopMenuModel()
        {
            this.SubMenu = new List<TopMenuModel>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Link { get; set; }
        public string Class { get; set; }
        public string PictureUrl { get; set; }
        public List<TopMenuModel> SubMenu { get; set; }
    }
}