using Asu.Web.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Asu.Web.Services
{
    public class SprCexService  
    {
        private IRepository<Models.Msi.Spr_cex> _repository;

        public SprCexService(IRepository<Models.Msi.Spr_cex> repository)
        {
            _repository = repository;
        }

        public List<Models.Msi.Spr_cex> GetAll()
        {
            return _repository.GetAll().ToList();
        }

        public Models.Msi.Spr_cex GetById(int id)
        {
            return _repository.Get(id);
        }

        public void Add(Models.Msi.Spr_cex school)
        {
            _repository.Add(school);
        }

        public void Update(Models.Msi.Spr_cex school)
        {
            _repository.Update(school);
        }

        public void Delete(int id)
        {
            Models.Msi.Spr_cex school = _repository.Get(id);
            if (school != null) _repository.Delete(school);
        }
    }
}