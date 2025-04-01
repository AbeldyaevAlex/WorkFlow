using Asu.Core;
using Asu.Core.Caching;
using Asu.Core.Data;
using Asu.Core.Domain.BannerPicture;
using Asu.Core.Domain.Catalog;
using Asu.Services.Stores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asu.Services.BannerPicture
{
    public partial class BannerService : IBannerService
    {
        #region Constants

        /// <summary>
        /// Key for caching
        /// </summary>
        /// <remarks>
        /// {0} : banner ID
        /// </remarks>
        private const string BANNERS_BY_ID_KEY = "Nop.banner.id-{0}";

        /// <summary>
        /// Key for caching
        /// </summary>
        /// <remarks>
        /// {0} : entity ID
        /// {1} : entity name
        /// {2} : is active
        /// {3} : page size
        /// {4} : page index
        /// {5} : current customer ID
        /// {6} : store ID
        /// </remarks>
        private const string BANNERS_ALL_KEY = "Nop.banner.all-{0}-{1}-{2}-{3}-{4}-{5}-{6}";

        /// <summary>
        /// Key for caching
        /// </summary>
        /// <remarks>
        /// {0} : entity ID
        /// {1} : entity name
        /// {2} : store ID
        /// </remarks>
        private const string AUTHORIZE_BANNERS_ALL_KEY = "Nop.banner.authorize.all-{0}-{1}-{2}";

        /// <summary>
        /// Key pattern to clear cache
        /// </summary>
        private const string BANNERS_PATTERN_KEY = "Nop.banner.";

        #endregion

        #region Fields

        private readonly IRepository<Banner> _bannerRepository;
        private readonly ICacheManager _cacheManager;
        private readonly IWorkContext _workContext;
        private readonly IStoreContext _storeContext;
        private readonly IStoreMappingService _storeMappingService;
        private readonly IRepository<Manufacturer> _manufacturerRepository;


        #endregion

        #region Ctor

        public BannerService(IRepository<Banner> bannerRepository,
            ICacheManager cacheManager,
            IWorkContext workContext,
            IStoreContext storeContext,
            IStoreMappingService storeMappingService,
            IRepository<Manufacturer> manufacturerRepository)
        {
            _bannerRepository = bannerRepository;
            _cacheManager = cacheManager;
            _workContext = workContext;
            _storeContext = storeContext;
            _storeMappingService = storeMappingService;
            _manufacturerRepository = manufacturerRepository;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Get banner by identifire
        /// </summary>
        /// <param name="bannerId">Banner identifire</param>
        /// <returns>Banner</returns>
        public virtual Banner GetBannerById(int bannerId)
        {
            if (bannerId <= 0)
                return null;

            string key = string.Format(BANNERS_BY_ID_KEY, bannerId);
            return _cacheManager.Get(key, () => _bannerRepository.GetById(bannerId));
        }

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
        public virtual IPagedList<Banner> GetAllBanners(int entityId = 0, string entityName = "0",
            DateTime? startDate = null, DateTime? endDate = null,
            bool? isActive = null, int pageIndex = 0, int pageSize = int.MaxValue)
        {
            string key = string.Format(BANNERS_ALL_KEY, entityId, entityName, isActive, pageIndex, pageSize, _workContext.CurrentCustomer.Id, _storeContext.CurrentStore.Id);
            return _cacheManager.Get(key, () =>
            {
                var query = _bannerRepository.Table;

                if (entityId > 0)
                    query = query.Where(b => b.EntityId == entityId);

                if (!string.IsNullOrEmpty(entityName) && entityName != "0")
                    query = query.Where(b => b.EntityName == entityName);

                if (isActive != null)
                    query = query.Where(b => b.Published == isActive);

                if (startDate.HasValue)
                    query = query.Where(o => startDate.Value <= o.StartDateTimeUtc);
                if (endDate.HasValue)
                    query = query.Where(o => endDate.Value >= o.EndDateTimeUtc);

                query = query.OrderBy(m => m.DisplayOrder);

                return new PagedList<Banner>(query, pageIndex, pageSize);
            });
        }

        /// <summary>
        /// Get authorize banner
        /// </summary>
        /// <param name="entityId">entity identifier</param>
        /// <param name="entityName">entity name</param>
        /// <returns>Banner</returns>
        public virtual IList<Banner> GetAuthorizeBanners(int entityId, string entityName)
        {
            if (entityId <= 0 || string.IsNullOrEmpty(entityName))
                return null;

            // Get banners in caching 
            string key = string.Format(AUTHORIZE_BANNERS_ALL_KEY, entityId, entityName, _storeContext.CurrentStore.Id);
            var banners = _cacheManager.Get(key, () =>
            {
                var bannerList = new List<Banner>();
                var query = _bannerRepository.Table;

                query = query.Where(b => b.EntityId == entityId && b.EntityName == entityName
                             && b.Published == true);

                query = query.OrderBy(m => m.DisplayOrder);

                foreach (var banner in query.ToList())
                {
                    if (banner.LimitedToStores == true)
                    {
                        if (_storeMappingService.Authorize(banner))
                        {
                            bannerList.Add(banner);
                        }
                    }
                    else
                    {
                        bannerList.Add(banner);
                    }
                }

                return bannerList;
            });

            // Get banners which are valid for only current date
            //specified valid date range
            var nowUtc = DateTime.UtcNow;
            banners = banners.Where(p =>
                                          ((!p.StartDateTimeUtc.HasValue || p.StartDateTimeUtc.Value < nowUtc)
                                          && (!p.EndDateTimeUtc.HasValue || p.EndDateTimeUtc.Value > nowUtc)))
                                         .ToList();

            return banners;
        }

        /// <summary>
        /// Insert banner
        /// </summary>
        /// <param name="banner">Banner</param>
        public virtual void InsertBanner(Banner banner)
        {
            if (banner == null)
                throw new ArgumentNullException(nameof(banner));

            _bannerRepository.Insert(banner);

            //cache
            _cacheManager.RemoveByPattern(BANNERS_PATTERN_KEY);
        }

        /// <summary>
        /// Update banner
        /// </summary>
        /// <param name="banner">Banner</param>
        public virtual void UpdateBanner(Banner banner)
        {
            if (banner == null)
                throw new ArgumentNullException(nameof(banner));

            _bannerRepository.Update(banner);

            //cache
            _cacheManager.RemoveByPattern(BANNERS_PATTERN_KEY);
        }

        /// <summary>
        /// Delete banner
        /// </summary>
        /// <param name="banner">Banner</param>
        public virtual void DeleteBanner(Banner banner)
        {
            if (banner == null)
                throw new ArgumentNullException(nameof(banner));

            _bannerRepository.Delete(banner);

            //cache
            _cacheManager.RemoveByPattern(BANNERS_PATTERN_KEY);
        }

        #endregion
    }
}
