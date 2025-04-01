using Asu.Core.Data;
using Asu.Core.Domain.DirectoryOfMaterialCodifiers;
using Asu.Core.Domain.StatusDirectory;
using Asu.Core.Domain.TypicalTechnologicalOperations;
using Asu.Mapping.Skm;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asu.Mapping.TTO
{
    public partial class FullSkmInfo
    {
        public long Id { get; set; }
        public string FullInfo { get; set; }
    }
    public partial class Group_TTO
    {
        public int KodTTOId { get; set; }
        public int KodKomponentaId { get; set; }
        public string Km { get; set; }
        public string Dbt { get; set; }
        public string Dsh { get; set; }
        public decimal? Ves { get; set; }
        public string nm_mater { get; set; }
        public string marka_mater { get; set; }
        public string gost { get; set; }
        public string krat_naim_eizm { get; set; }
        public int kgr { get; set; }
        public int kod_sklad { get; set; }
        public int link_ogt { get; set; }
        public string pr_km { get; set; }
        public string prpokr { get; set; }
        public string Naimogt { get; set; }
    }
    public partial class TtoService : ITtoService
    {
        private readonly IRepository<Spr_tto> _ttoRepository;
        private readonly IRepository<SprSkm> _sprSkmRepository;
        private readonly IRepository<DocumentStatus> _documentStatusRepository;
        private readonly IRepository<DirectoryOfMaterialName> _directoryOfMaterialNameRepository;
        private readonly IRepository<MarkMater> _markMaterRepository;
        private readonly IRepository<GostMater> _gostMaterRepository;
        private readonly IRepository<SprEizm> _sprEizmRepository;
        private readonly IRepository<SprKgr> _sprKgrRepository;
        private readonly IRepository<SprOgt> _sprOgtRepository;
        private readonly IRepository<SprOts> _sprOtsRepository;
        private readonly IRepository<SprPrKm> _sprPrKmRepository;
        public TtoService(IRepository<Spr_tto> ttoRepository, IRepository<SprSkm> sprSkmRepository, IRepository<DocumentStatus> documentStatusRepository, IRepository<SprEizm> sprEizmRepository,
            IRepository<DirectoryOfMaterialName> directoryOfMaterialNameRepository, IRepository<MarkMater> markMaterRepository, IRepository<GostMater> gostMaterRepository, IRepository<SprKgr> sprKgrRepository,
            IRepository<SprOgt> sprOgtRepository, IRepository<SprOts> sprOtsRepository, IRepository<SprPrKm> sprPrKmRepository)
        {
            _ttoRepository = ttoRepository;
            _sprSkmRepository = sprSkmRepository;
            _documentStatusRepository = documentStatusRepository;
            _directoryOfMaterialNameRepository = directoryOfMaterialNameRepository;
            _markMaterRepository = markMaterRepository;
            _gostMaterRepository = gostMaterRepository;
            _sprEizmRepository = sprEizmRepository;
            _sprKgrRepository = sprKgrRepository;
            _sprOgtRepository = sprOgtRepository;
            _sprOtsRepository = sprOtsRepository;
            _sprPrKmRepository = sprPrKmRepository;
        }
        public IQueryable<Spr_tto> GetAllTto()
        {
            var query = _ttoRepository.Table;
            return query;
        }

        public IList<Spr_tto> GetAllTtoToList()
        {
            var query = _ttoRepository.Table.ToList();
            return query;
        }

        public IEnumerable<FullSkmInfo> GetFullSkmInfo()

        {
            var fullSkmInfoForTTO = (from skm in _sprSkmRepository.Table
                                     join status in _documentStatusRepository.Table
                                     on skm.DocumentStatusId equals status.Id
                                     join nmskm in _directoryOfMaterialNameRepository.Table
                                     on skm.NmSkmId equals nmskm.Id
                                     join marka in _markMaterRepository.Table
                                     on skm.MarkaId equals marka.Id
                                     join gost in _gostMaterRepository.Table
                                     on skm.GostId equals gost.Id
                                     join eizm in _sprEizmRepository.Table
                                     on skm.EizmId equals eizm.Id
                                     where skm.Dsh == "ТТО" && status.Status != "Аннулирован"
                                     select new FullSkmInfo
                                     {
                                         Id = skm.Id,
                                         FullInfo = skm.Km.ToString() + "; " + nmskm.NameMaterial.ToString() + "; " + marka.MarkaMater.ToString() + "; " + skm.Dbt.ToString() + "; "
                                         + skm.Ves.ToString() + "; " + gost.Gost.ToString() + "; " + eizm.KratNaimEizm.ToString()
                                     }).AsEnumerable();
            return fullSkmInfoForTTO;
        }

        public IList<Group_TTO> GetUniQTTO()
        {
            var manager = new DataSettingsManager();
            var settings = manager.LoadSettings();
            SqlConnection connection = new SqlConnection(settings.DataConnectionString);
            connection.Open();
            SqlCommand command = new SqlCommand(@"SELECT * FROM GroupingTto", connection);
            List<Group_TTO> groupingTto = new List<Group_TTO>();
            SqlDataReader dr = command.ExecuteReader();
            while (dr.Read())
            {
                groupingTto.Add(new Group_TTO()
                {
                    KodTTOId = dr.GetInt32(0),
                    Km = dr.GetString(1),
                    Dbt = dr.GetString(2),
                    Dsh = dr.GetString(3),
                    Ves = dr.GetDecimal(4),
                    nm_mater = dr.GetString(5),
                    marka_mater = dr.GetString(6),
                    gost = dr.GetString(7),
                    krat_naim_eizm = dr.GetString(8),
                    kgr = dr.GetInt32(9),
                    kod_sklad = dr.GetInt32(10),
                    link_ogt = dr.GetInt32(11),
                    pr_km = dr.GetString(12),
                    prpokr = dr.GetString(13),
                    Naimogt = dr.GetString(14)
                });
            }
            connection.Close();
            return groupingTto.OrderBy(i => i.Km).ToList();
        }

        public IQueryable<Spr_tto> Get_TTO(object masterRowKey)
        {
            string Id_link_kod_tto = null;
            if (masterRowKey != null)
            {
                Id_link_kod_tto = masterRowKey.ToString();
            }
            else
            {
                Id_link_kod_tto = null;
            }

            int.TryParse(Id_link_kod_tto, out int Id);

            var model = GetAllTto().Where(i => i.KodTTOId == Id);
            return model;
        }
    }
}
