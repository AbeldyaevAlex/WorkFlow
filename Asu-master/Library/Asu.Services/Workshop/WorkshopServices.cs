using System.Collections.Generic;
using System.Linq;
using Asu.Core.Domain.Msi;
using Asu.Services.Workshop;

namespace Asu.Services
{
    public class WorkshopService
    {
        private IWoksopRepository<Spr_cex> _repository;

        public WorkshopService(IWoksopRepository<Spr_cex> repository)
        {
            _repository = repository;
        }

        public List<Spr_cex> GetAll()
        {
            return _repository.GetAll().ToList();
        }

        public Spr_cex GetById(int id)
        {
            return _repository.Get(id);
        }

        public void Add(Spr_cex school)
        {
            _repository.Add(school);
        }

        public void Update(Spr_cex school)
        {
            _repository.Update(school);
        }

        public void Delete(int id)
        {
            Spr_cex school = _repository.Get(id);
            if (school != null) _repository.Delete(school);
        }
    }
}
