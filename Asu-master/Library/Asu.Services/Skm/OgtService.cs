using Asu.Core.Data;
using Asu.Core.Domain.DirectoryOfMaterialCodifiers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asu.Mapping.Skm
{
    public partial class ExtendedNaimOgt
    {
        public int Id { get; set; }
        public string FullNaimOgt { get; set; }
    }
    public partial class OgtService : IOgtService
    {
        private readonly IRepository<SprOgt> _SprOgtRepository;
        public OgtService(IRepository<SprOgt> SprOgtRepository)
        {
            _SprOgtRepository = SprOgtRepository;
        }

        public int GetIdFromNaimOgt(string NaimOgt)
        {
            var ogtId = _SprOgtRepository.Table.Where(x => x.NaimOgt == NaimOgt).Select(p => p.Id).FirstOrDefault();
            return ogtId;
        }

        public int GetIdFromOgt(int ogt)
        {
            var ogtId = _SprOgtRepository.Table.Where(x => x.OGT == ogt).Select(p => p.Id).FirstOrDefault();
            return ogtId;
        }

        public string GetNaimOgtFromId(int? IdOgt)
        {
            var NaimOgt = _SprOgtRepository.Table.Where(x => x.Id == IdOgt).Select(p => p.NaimOgt).FirstOrDefault();
            return NaimOgt;
        }

        public IList<SprOgt> GetAllOgt()
        {
            var query = _SprOgtRepository.Table.ToList();
            return query;
        }

        public int? GetOgtFromNaimOgt(string NaimOgt)
        {
            var Ogt = _SprOgtRepository.Table.Where(x => x.NaimOgt == NaimOgt).Select(p => p.OGT).FirstOrDefault();
            return Ogt;
        }

        public IList<ExtendedNaimOgt> GetAllNaimOgt()
        {
            var ExtendedDirectoryNaimOgt = (from ogt in _SprOgtRepository.Table
                                                select new ExtendedNaimOgt
                                                {
                                                    Id = ogt.Id,
                                                    FullNaimOgt = ogt.OGT.ToString() + " " + ogt.NaimOgt.ToString()
                                                }).ToList();
            return ExtendedDirectoryNaimOgt;
        }

        public virtual void InsertOgt(SprOgt ogt)
        {
            if (ogt == null)
                throw new ArgumentNullException("Ogt");
            _SprOgtRepository.Insert(ogt);
        }
    }
}
