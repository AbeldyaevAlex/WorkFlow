using Asu.Core.Data;
using Asu.Core.Domain.DirectoryOfMaterialCodifiers;
using System.Collections.Generic;
using System.Linq;

namespace Asu.Mapping.Skm
{
    public partial class NmMaterService : INmMaterService
    {
        private readonly IRepository<DirectoryOfMaterialName> _DirectoryOfMaterialNameRepository;
        public NmMaterService(IRepository<DirectoryOfMaterialName> directoryOfMaterialNameRepository)
        {
            _DirectoryOfMaterialNameRepository = directoryOfMaterialNameRepository;
        }
        public IQueryable<DirectoryOfMaterialName> GetAllNameMater()
        {
            return _DirectoryOfMaterialNameRepository.Table;
        }
        public List<DirectoryOfMaterialName> GetAllNameMaterList()
        {
            return _DirectoryOfMaterialNameRepository.Table.ToList();
        }
        public int GetIdFromNameMater(string nameMaterial)
        {
            var nameMaterId = _DirectoryOfMaterialNameRepository.Table.Where(x => x.NameMaterial == nameMaterial).Select(c => c.Id).FirstOrDefault();
            return nameMaterId;
        }
    }
}
