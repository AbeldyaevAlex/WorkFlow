using Asu.Core.Data;
using Asu.Core.Domain.Metrology;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;

namespace Asu.Mapping.Metrology
{
    public partial class MetrologyService : IMetrologyService
    {
        private readonly IRepository<Spr_metrol> _sprMetrolRepository;
        private readonly IRodPoverkService _RodPoverkService;

        public MetrologyService(IRepository<Spr_metrol> sprMetrolRepository, IRodPoverkService rodPoverkService)
        {
            _sprMetrolRepository = sprMetrolRepository;
            _RodPoverkService = rodPoverkService;
        }

        public List<Spr_metrol> GetMetrologyDirectory(int workshopId, string rodPoverk)
        {
            var rodPoverkId = _RodPoverkService.GetRodPoverkId(rodPoverk);
            var metrologyDirectory = (from nm_task in _sprMetrolRepository.Table
                                      where nm_task.link_slugba == workshopId && rodPoverkId.Contains((int)nm_task.link_rod_poverk)
                                      select nm_task).ToList();
            return metrologyDirectory;
        }

        public List<Spr_metrol> GetMetrologyDirectory()
        {
            var metrologyDirectory = _sprMetrolRepository.Table.ToList();
            return metrologyDirectory;
        }
    }
}
