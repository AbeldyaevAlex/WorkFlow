using Asu.Core;
using Asu.Core.Domain.BannerPicture;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asu.Services.BannerPicture
{
    public partial interface IBannerService
    {
        /// <summary>
        /// Get banner by identifire
        /// </summary>
        /// <param name="bannerId">Banner identifire</param>
        /// <returns>Banner</returns>
        Banner GetBannerById(int bannerId);

        /// <summary>
        /// Search banner
        /// </summary>
        /// <param name="entityId">entity identifier</param>
        /// <param name="entityName">entity name</param>
        /// <param name="isActive">A value indicating whether to show active records</param>
        /// <param name="startDate">start date</param>
        /// <param name="endDate">end date</param>
        /// <param name="pageIndex">Page index</param>
        /// <param name="pageSize">Page size</param>
        /// <returns>Banner collection</returns>
        IPagedList<Banner> GetAllBanners(int entityId = 0, string entityName = "0",
            DateTime? startDate = null, DateTime? endDate = null,
            bool? isActive = null, int pageIndex = 0, int pageSize = int.MaxValue);

        /// <summary>
        /// Get authorize banner
        /// </summary>
        /// <param name="entityId">entity identifier</param>
        /// <param name="entityName">entity name</param>
        /// <returns>Banner</returns>
        IList<Banner> GetAuthorizeBanners(int entityId, string entityName);

        /// <summary>
        /// Insert banner
        /// </summary>
        /// <param name="banner">Banner</param>
        void InsertBanner(Banner banner);

        /// <summary>
        /// Update banner
        /// </summary>
        /// <param name="banner">Banner</param>
        void UpdateBanner(Banner banner);

        /// <summary>
        /// Delete banner
        /// </summary>
        /// <param name="banner">Banner</param>
        void DeleteBanner(Banner banner);
    }
}
