using Asu.Core;
using Asu.Core.Data;
using Asu.Core.Domain.Catalog;
using Asu.Core.Domain.Customers;
using Asu.Core.Domain.DirectoryOfMaterialCodifiers;
using Asu.Core.Domain.Msi;
using Asu.Core.Domain.StatusDirectory;
using Asu.Core.Domain.Stores;
using Asu.Core.Domain.Topics;
using Asu.Services.SprPkp;
using Asu.Services.Events;
using Asu.Services.Stores;
using Asu.Services.Topics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asu.Services.SprPkp
{
    public partial class SprPkpService : ISprPkpService
    {
        #region Fields

        private readonly IRepository<Spr_pkp> _pkpRepository;
        private readonly IRepository<StoreMapping> _storeMappingRepository;
        private readonly IStoreMappingService _storeMappingService;
        private readonly CatalogSettings _catalogSettings;
        private readonly IEventPublisher _eventPublisher;




        #endregion

        #region Ctor

        public SprPkpService(IRepository<Spr_pkp> pkpRepository,
            IRepository<StoreMapping> storeMappingRepository,
            IStoreMappingService storeMappingService,
            CatalogSettings catalogSettings,
            IEventPublisher eventPublisher
            )
        {
            this._pkpRepository = pkpRepository;
            this._storeMappingRepository = storeMappingRepository;
            this._storeMappingService = storeMappingService;
            this._catalogSettings = catalogSettings;
            this._eventPublisher = eventPublisher;
        }



        #endregion

        #region Method
        /// <summary>
        /// Gets all topics
        /// </summary>
        /// <returns>Topics</returns>
        public virtual IList<Spr_pkp> GetAllPkp()
        {
            var query = from p in _pkpRepository.Table
                        select p;
            var Pkp = query.ToList();

            return query.OrderBy(t => t.Pkp).ToList();
        }
        public virtual IList<Spr_pkp> GetAllPkpId()
        {
            var query = from p in _pkpRepository.Table
                        select p;
            var Pkp = query.ToList();

            return query.OrderBy(t => t.Pkp).ToList();
        }
        public void UpdatePkp(Spr_pkp item)
        {
            _pkpRepository.Update(item);
        }
        public void DeletePkp(Spr_pkp item)
        {
            _pkpRepository.Delete(item);
        }

        public Spr_pkp GetPkpById(int pkpId)
        {
            if (pkpId == 0)
                return null;

            return _pkpRepository.GetById(pkpId);
        }

        #endregion
    }
}
