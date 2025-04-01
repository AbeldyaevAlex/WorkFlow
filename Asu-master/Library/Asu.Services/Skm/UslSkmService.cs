using Asu.Core.Data;
using Asu.Core.Domain.DirectoryOfMaterialCodifiers;
using Asu.Core.Domain.TypicalTechnologicalOperations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asu.Mapping.Skm
{
    public partial class UslSkmService : IUslSkmService
    {
        private readonly IRepository<UslSkm> _UslSkmRepository;

        public UslSkmService(IRepository<UslSkm> UslSkmRepository)
        {
            _UslSkmRepository = UslSkmRepository;
        }
        public IQueryable<UslSkm> GetAllUsl()
        {
            var query = _UslSkmRepository.Table;
            return query;
        }
    }
}
