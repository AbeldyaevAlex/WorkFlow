using Asu.Core;
using Asu.Core.Data;
using Asu.Core.Domain.Blogs;
using Asu.Core.Domain.Customers;
using Asu.Core.Domain.DirectoryOfMaterialCodifiers;
using Asu.Core.Domain.Msi;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Asu.Mapping.Skm
{
    public partial class DirectoryOfMaterialCodifiersService : IDirectoryOfMaterialCodifiersService
    {
        private readonly IWorkContext _workContext;
        private readonly IRepository<SprSkm> _SkmRepository;
        private readonly IRepository<SprPrKm> _SprPrKmRepository;

        public DirectoryOfMaterialCodifiersService(IWorkContext workContext, IRepository<SprSkm> SkmRepository, IRepository<SprPrKm> sprPrKmRepository)
        {
            _workContext = workContext;
            _SkmRepository = SkmRepository;
            _SprPrKmRepository = sprPrKmRepository;
        }

        public IQueryable<SprSkm> GetAllKm(SprSkm param)
        {
            if (_workContext.CurrentCustomer.IsAdmin())
            {
                var query = _SkmRepository.Table.Where(x => x.Id != 1);
                return query;
            }
            else
            {
                var query = _SkmRepository.Table.Where(x => x.Id != 1);
                return query;
            }
        }

        public IList<SprPrKm> GetAllPrKms()
        {
           var query =  _SprPrKmRepository.Table.ToList();
            return query;
        }

        public virtual void InsertKm(SprSkm km)
        {
            if (km == null)
                throw new ArgumentNullException("km");

            _SkmRepository.Insert(km);
        }
    }
}
