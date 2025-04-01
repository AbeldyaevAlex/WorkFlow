using Asu.Core.Data;
using Asu.Core.Domain.Metrology;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asu.Mapping.Metrology
{
    public partial class ExtendedDirectoryOfPodgrPrib
    {
        public int Id { get; set; }
        public string FullNamePodgrPrib { get; set; }
    }
    public partial class PodgrPribService : IPodgrPribService
    {
        private readonly IRepository<Podgr_prib> _podgrPribRepository;
        private readonly IRepository<Nm_prib> _nmPribRepository;
        public PodgrPribService(IRepository<Podgr_prib> podgrPribRepository, IRepository<Nm_prib> nmPribRepository)
        {
            _podgrPribRepository = podgrPribRepository;
            _nmPribRepository = nmPribRepository;
        }
        public IList<Podgr_prib> GetPodgrPribList()
        {
            var query = _podgrPribRepository.Table;
            return query.ToList();
        }
        public List<ExtendedDirectoryOfPodgrPrib> GetExtendedDirectoryOfPodgrPrib()
        {
            var ExtendedDirectoryOfPodgrPrib = (from podgrprib in _podgrPribRepository.Table
                                                join nmprib in _nmPribRepository.Table
                                                on podgrprib.link_nmprib equals nmprib.Id
                                                select new ExtendedDirectoryOfPodgrPrib
                                                {
                                                    Id = podgrprib.Id,
                                                    FullNamePodgrPrib = podgrprib.n_podgrupp.ToString() + " " + nmprib.nm_prib1
                                                }).ToList();
            return ExtendedDirectoryOfPodgrPrib;
        }
    }
}
