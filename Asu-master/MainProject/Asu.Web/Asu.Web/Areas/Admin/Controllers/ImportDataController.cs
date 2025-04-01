using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Asu.Web.Models.ContextDb;
using ClosedXML.Excel;
using Asu.Core.Data;
using Asu.Core.Domain.Msi;
using Asu.Core.Domain.Pvi;
using Asu.Core.Domain.DirectoryOfMaterialCodifiers;
using Asu.Core.Domain.TypicalTechnologicalOperations;
using Asu.Core.Domain.Work;
using Asu.Core;
using Asu.Core.Domain.Metrology;

namespace Asu.Web.Areas.Admin.Controllers
{
    public class AllDirectory
    {
        public int Id { get; set; }
        public string Directory { get; set; }
    }

    public class ImportDataController : Controller
    {
        const string Operation = "Выгрузка данных из БД ОИСП";

        private readonly IWorkContext _workContext;
        private readonly IRepository<Spr_pvi> _pviRepository;
        private readonly IRepository<Spr_tem> _temRepository;
        private readonly IRepository<Spr_tematik> _tematikRepository;
        private readonly IRepository<Spr_Tehnizg> _tehnizgRepository;
        private readonly IRepository<Spr_Perizd> _perizdRepository;
        private readonly IRepository<Spr_Zakaz> _zakazRepository;
        private readonly IRepository<Spr_mash> _mashRepository;
        private readonly IRepository<Spr_mash_sg> _mashsgRepository;
        private readonly IRepository<DokumObosnov> _dokumObosnovRepository;
        private readonly IRepository<GostMater> _gostMaterRepository;
        private readonly IRepository<MarkMater> _markMaterRepository;
        private readonly IRepository<DirectoryOfMaterialName> _materialNameRepository;
        private readonly IRepository<PredprPostav> _predprPostavRepository;
        private readonly IRepository<SortMater> _sortMaterRepository;
        private readonly IRepository<SprBalSch> _balSchrRepository;
        private readonly IRepository<SprEizm> _eizmRepository;
        private readonly IRepository<SprGrMater> _grMaterRepository;
        private readonly IRepository<SprKgr> _kgrMaterRepository;
        private readonly IRepository<SprOgt> _ogtMaterRepository;
        private readonly IRepository<SprPrKm> _prKmMaterRepository;
        private readonly IRepository<SprOts> _otsMaterRepository;
        private readonly IRepository<SprSortam> _sortamRepository;
        private readonly IRepository<UslSkm> _uslSkmRepository;
        private readonly IRepository<SprSkm> _skmRepository;
        private readonly IRepository<SprCenMater> _cenMaterRepository;
        private readonly IRepository<Spr_cex> _cexRepository;
        private readonly IRepository<Spr_prpokr> _prpokrRepository;
        private readonly IRepository<Spr_tto> _ttoRepository;
        private readonly IRepository<Raz_det> _razDetRepository;
        private readonly IRepository<Spr_Razd_DSE> _razDseRepository;
        private readonly IRepository<Spr_Razd_Izd> _razIzdRepository;
        private readonly IRepository<Spr_cex_prizn> _cexPriznRepository;
        private readonly IRepository<spr_grup_prim> _grupPrimRepository;
        private readonly IRepository<Spr_kompl> _komplRepository;
        private readonly IRepository<Spr_mater> _materRepository;
        private readonly IRepository<Spr_nmdet> _nmDetRepository;
        private readonly IRepository<Spr_obozn> _oboznRepository;
        private readonly IRepository<Spr_pkp> _pkpRepository;
        private readonly IRepository<Spr_kdan> _kdanRepository;
        private readonly IRepository<Spr_agr> _agrRepository;
        private readonly IRepository<Spr_obozn_mater> _oboznMaterRepository;
        private readonly IRepository<GR_RAZDIZD> _GrRazdIzdRepository;
        private readonly IRepository<Spr_rascex> _rascexRepository;
        private readonly IRepository<Spr_poln_rascex> _polnRascexRepository;
        private readonly IRepository<Spr_rasc_vert> _rascVertRascexRepository;
        private readonly IRepository<Spr_specif> _specifRepository;
        private readonly IRepository<Spr_prim_dse> _primDseRepository;
        private readonly IRepository<DirectiveWork> _DirectiveWorkRepository;
        private readonly IRepository<ExceptionForWork> _ExceptionForWorkRepository;
        private readonly IRepository<Gos_ree> _gosReeRepository;
        private readonly IRepository<Konserv> _KonservRepository;
        private readonly IRepository<Nazn_prib> _NaznPribRepository;
        private readonly IRepository<Nm_prib> _NmPribRepository;
        private readonly IRepository<Nm_vidiz> _NmVidizRepository;
        private readonly IRepository<Period_pover> _PeriodPoverRepository;
        private readonly IRepository<Predpr_izg> _PredprIzgRepository;
        private readonly IRepository<Rod_poverk> _RodPoverkRepository;
        private readonly IRepository<Spr_cena_del> _SprCenaDelRepository;
        private readonly IRepository<Spr_klass_tochn> _SprKlassTochnRepository;
        private readonly IRepository<Spr_predel> _SprPredelRepository;
        private readonly IRepository<Spr_stan> _SprStanRepository;
        private readonly IRepository<Usl_expluat> _UslExpluatRepository;
        private readonly IRepository<Tip_pribora> _TipPriboraRepository;
        private readonly IRepository<Vid_izmer> _VidizmerRepository;
        private readonly IRepository<Podgr_prib> _PodgrPribRepository;
        private readonly IRepository<Spr_metrol> _SprMetrolRepository;

        public ImportDataController(IWorkContext workContext, IRepository<Spr_pvi> pviRepository, IRepository<Spr_tem> temRepository, IRepository<Spr_tematik> tematikRepository, IRepository<Spr_Tehnizg> tehnizgRepository,
            IRepository<Spr_Perizd> perizdRepository, IRepository<Spr_Zakaz> zakazRepository, IRepository<Spr_mash> mashRepository, IRepository<Spr_mash_sg> mashsgRepository, IRepository<DokumObosnov> dokumObosnovRepository,
            IRepository<GostMater> gostMaterRepository, IRepository<MarkMater> markMaterRepository, IRepository<DirectoryOfMaterialName> materialNameRepository, IRepository<PredprPostav> predprPostavRepository, IRepository<SortMater> sortMaterRepository,
            IRepository<SprBalSch> balSchrRepository, IRepository<SprEizm> eizmRepository, IRepository<SprGrMater> grMaterRepository, IRepository<SprKgr> kgrMaterRepository, IRepository<SprOgt> ogtMaterRepository, IRepository<SprPrKm> prKmMaterRepository,
            IRepository<SprOts> otsMaterRepository, IRepository<SprSortam> sortamRepository, IRepository<UslSkm> uslSkmRepository, IRepository<SprSkm> skmRepository, IRepository<SprCenMater> cenMaterRepository, IRepository<Spr_prim_dse> primDseRepository,
            IRepository<Spr_cex> cexRepository, IRepository<Spr_prpokr> prpokrRepository, IRepository<Spr_tto> ttoRepository, IRepository<Raz_det> razDetRepository, IRepository<Spr_Razd_DSE> razDseRepository, IRepository<Spr_specif> specifRepository,
            IRepository<Spr_Razd_Izd> razIzdRepository, IRepository<Spr_cex_prizn> cexPriznRepository, IRepository<spr_grup_prim> grupPrimRepository, IRepository<Spr_kompl> komplRepository, IRepository<Spr_rasc_vert> rascVertRascexRepository,
            IRepository<Spr_mater> materRepository, IRepository<Spr_nmdet> nmDetRepository, IRepository<Spr_obozn> oboznRepository, IRepository<Spr_pkp> pkpRepository, IRepository<Spr_kdan> kdanRepository, IRepository<Spr_agr> agrRepository,
            IRepository<Spr_obozn_mater> oboznMaterRepository, IRepository<GR_RAZDIZD> GrRazdIzdRepository, IRepository<Spr_rascex> rascexRepository, IRepository<Spr_poln_rascex> polnRascexRepository, IRepository<DirectiveWork> DirectiveWorkRepository, IRepository<ExceptionForWork> ExceptionForWorkRepository,
            IRepository<Gos_ree> gosReeRepository, IRepository<Konserv> KonservRepository, IRepository<Nazn_prib> NaznPribRepository, IRepository<Nm_prib> NmPribRepository, IRepository<Nm_vidiz> NmVidizRepository, IRepository<Period_pover> PeriodPoverRepository, IRepository<Predpr_izg> PredprIzgRepository,
            IRepository<Rod_poverk> RodPoverkRepository, IRepository<Spr_cena_del> SprCenaDelRepository, IRepository<Spr_klass_tochn> SprKlassTochnRepository, IRepository<Spr_predel> sprPredelRepository, IRepository<Spr_stan> SprStanRepository, IRepository<Usl_expluat> UslExpluatRepository, IRepository<Tip_pribora> tipPriboraRepository,
            IRepository<Vid_izmer> VidizmerRepository, IRepository<Podgr_prib> podgrPribRepository, IRepository<Spr_metrol> SprMetrolRepository)
        {
            _pviRepository = pviRepository;
            _temRepository = temRepository;
            _tematikRepository = tematikRepository;
            _tehnizgRepository = tehnizgRepository;
            _perizdRepository = perizdRepository;
            _zakazRepository = zakazRepository;
            _mashRepository = mashRepository;
            _mashsgRepository = mashsgRepository;
            _dokumObosnovRepository = dokumObosnovRepository;
            _gostMaterRepository = gostMaterRepository;
            _markMaterRepository = markMaterRepository;
            _materialNameRepository = materialNameRepository;
            _predprPostavRepository = predprPostavRepository;
            _sortMaterRepository = sortMaterRepository;
            _balSchrRepository = balSchrRepository;
            _eizmRepository = eizmRepository;
            _grMaterRepository = grMaterRepository;
            _kgrMaterRepository = kgrMaterRepository;
            _ogtMaterRepository = ogtMaterRepository;
            _prKmMaterRepository = prKmMaterRepository;
            _otsMaterRepository = otsMaterRepository;
            _sortamRepository = sortamRepository;
            _uslSkmRepository = uslSkmRepository;
            _skmRepository = skmRepository;
            _cenMaterRepository = cenMaterRepository;
            _cexRepository = cexRepository;
            _prpokrRepository = prpokrRepository;
            _ttoRepository = ttoRepository;
            _razDetRepository = razDetRepository;
            _razDseRepository = razDseRepository;
            _razIzdRepository = razIzdRepository;
            _cexPriznRepository = cexPriznRepository;
            _grupPrimRepository = grupPrimRepository;
            _komplRepository = komplRepository;
            _materRepository = materRepository;
            _nmDetRepository = nmDetRepository;
            _oboznRepository = oboznRepository;
            _pkpRepository = pkpRepository;
            _kdanRepository = kdanRepository;
            _agrRepository = agrRepository;
            _oboznMaterRepository = oboznMaterRepository;
            _GrRazdIzdRepository = GrRazdIzdRepository;
            _rascexRepository = rascexRepository;
            _polnRascexRepository = polnRascexRepository;
            _rascVertRascexRepository = rascVertRascexRepository;
            _specifRepository = specifRepository;
            _primDseRepository = primDseRepository;
            _DirectiveWorkRepository = DirectiveWorkRepository;
            _ExceptionForWorkRepository = ExceptionForWorkRepository;
            _workContext = workContext;
            _gosReeRepository = gosReeRepository;
            _KonservRepository = KonservRepository;
            _NaznPribRepository = NaznPribRepository;
            _NmPribRepository = NmPribRepository;
            _NmVidizRepository = NmVidizRepository;
            _PeriodPoverRepository = PeriodPoverRepository;
            _PredprIzgRepository = PredprIzgRepository;
            _RodPoverkRepository = RodPoverkRepository;
            _SprCenaDelRepository = SprCenaDelRepository;
            _SprKlassTochnRepository = SprKlassTochnRepository;
            _SprPredelRepository = sprPredelRepository;
            _SprStanRepository = SprStanRepository;
            _UslExpluatRepository = UslExpluatRepository;
            _TipPriboraRepository = tipPriboraRepository;
            _VidizmerRepository = VidizmerRepository;
            _PodgrPribRepository = podgrPribRepository;
            _SprMetrolRepository = SprMetrolRepository;
        }



        public string Valid_Cell(string param)
        {
            if (param == "0" || (string.IsNullOrEmpty(param) || string.IsNullOrWhiteSpace(param)))
            {
                return null;
            }
            else
            {
                return param;
            }

        }

        AsuAviaDbContext con_str = new AsuAviaDbContext();


        public ActionResult ImportData()
        {
            return View();
        }
        [HttpPost]
        public JsonResult ImportData(HttpPostedFileBase fileExcel, string directory)
        {

            List<AllDirectory> dir = new List<AllDirectory>
            {
                ////////////////Tem////////////////////////////
                ///
                ////new AllDirectory() { Id = 29, Directory = "Spr_pvi"},
                //new AllDirectory() { Id = 1, Directory = "TEMATIK" },
                //new AllDirectory() { Id = 4, Directory = "S_ZAKAZ"},
                //new AllDirectory() { Id = 8, Directory = "SPR_MASH"},
                //new AllDirectory() { Id = 7, Directory = "SPERIZD"},
                //new AllDirectory() { Id = 23, Directory = "S_MASHSG"},
                //new AllDirectory() { Id = 3, Directory = "SPR_TEM"},               
                //new AllDirectory() { Id = 2, Directory = "TEHN_IZG" },


                ////////////////Msi////////////////////////////

                //new AllDirectory() { Id = 12, Directory = "RAZ_DET"},
                //new AllDirectory() { Id = 11, Directory = "RAZD_DSE"},
                //new AllDirectory() { Id = 10, Directory = "RAZD_IZD"},
                //new AllDirectory() { Id = 9, Directory = "S_CEXPR"},
                //new AllDirectory() { Id = 24, Directory = "S_GRPRIM"},
                //new AllDirectory() { Id = 18, Directory = "S_KOMPL"},
                //new AllDirectory() { Id = 16, Directory = "S_MATER"},
                //new AllDirectory() { Id = 14, Directory = "S_NMDET"},
                //new AllDirectory() { Id = 13, Directory = "SPR_PKP"},
                //new AllDirectory() { Id = 15, Directory = "S_OBOZN"},
                //new AllDirectory() { Id = 5, Directory = "SPR_KDAN"},
                //new AllDirectory() { Id = 6, Directory = "SPR_AGR"},
                //new AllDirectory() { Id = 22, Directory = "S_OBOZ_M"},
                //new AllDirectory() { Id = 17, Directory = "GRRAZIZD"},
                //new AllDirectory() { Id = 19, Directory = "S_RASCEX"},
                //new AllDirectory() { Id = 27, Directory = "S_PRASC"},
                //new AllDirectory() { Id = 21, Directory = "S_RASCV"},
                //new AllDirectory() { Id = 26, Directory = "S_SPECIF"},
                //new AllDirectory() { Id = 28, Directory = "S_PRIM"},               

                ////////////////Trud/////////////////////////////////

                //new AllDirectory() { Id = 31, Directory = "TR_ISKL"},
                //new AllDirectory() { Id = 30, Directory = "TR_OBOZN"},

                //////////////// Skm ////////////////////////////

                //new AllDirectory() { Id = 33, Directory = "DOK_OBOS"},
                //new AllDirectory() { Id = 34, Directory = "GOST_MAT"},
                //new AllDirectory() { Id = 35, Directory = "MARK_M"},
                //new AllDirectory() { Id = 36, Directory = "NM_MAT"},
                //new AllDirectory() { Id = 37, Directory = "PR_POST"},
                //new AllDirectory() { Id = 38, Directory = "SORT_MAT"},
                //new AllDirectory() { Id = 39, Directory = "SPR_BSCH"},
                //new AllDirectory() { Id = 40, Directory = "SPR_EIZM"},
                //new AllDirectory() { Id = 42, Directory = "SPR_GRM"},
                //new AllDirectory() { Id = 43, Directory = "SPR_KGR"},
                //new AllDirectory() { Id = 48, Directory = "SPR_PRKM"},
                //new AllDirectory() { Id = 44, Directory = "SPR_OGT"},
                //new AllDirectory() { Id = 45, Directory = "SPR_OTS"},
                //new AllDirectory() { Id = 46, Directory = "spr_SORT"},
                //new AllDirectory() { Id = 47, Directory = "USL_SKM"},
                //new AllDirectory() { Id = 32, Directory = "SPR_SKM"},
                ////new AllDirectory() { Id = 51, Directory = "SPR_CENM"},

                //////////////// Tto ////////////////////////////

                //new AllDirectory() { Id = 25, Directory = "SPR_CEX"},
                //new AllDirectory() { Id = 50, Directory = "SPR_PRTO"},
                //new AllDirectory() { Id = 49, Directory = "SPR_TTO"},

                ///////////////Metrology/////////////////////////

                //new AllDirectory() { Id = 52, Directory = "GOS_REE" },
                //new AllDirectory() { Id = 53, Directory = "KONSERV" },
                //new AllDirectory() { Id = 54, Directory = "NAZN_PRI" },
                //new AllDirectory() { Id = 55, Directory = "NM_PRIB" },
                //new AllDirectory() { Id = 56, Directory = "NM_VIDIZ" },
                //new AllDirectory() { Id = 57, Directory = "PER_POV" },
                //new AllDirectory() { Id = 58, Directory = "PRED_IZG" },
                //new AllDirectory() { Id = 59, Directory = "ROD_POV" },
                //new AllDirectory() { Id = 60, Directory = "SPR_CENA" },
                //new AllDirectory() { Id = 61, Directory = "SPR_KLAS" },
                //new AllDirectory() { Id = 62, Directory = "SPR_PRED" },
                //new AllDirectory() { Id = 63, Directory = "SPR_STAN" },
                //new AllDirectory() { Id = 64, Directory = "USL_EKSP" },
                //new AllDirectory() { Id = 65, Directory = "TIP_PRIB" },
                //new AllDirectory() { Id = 66, Directory = "VID_IZME" },
                //new AllDirectory() { Id = 67, Directory = "POD_PRIB" },
                //new AllDirectory() { Id = 68, Directory = "SPR_METR" },
            };

            if (ModelState.IsValid)
            {
                var sw = new Stopwatch();
                sw.Start();
                //string fileName = Path.GetFileName(fileExcel.FileName);
                foreach (var item in dir)
                {
                    string fileName = item.Directory + ".xlsx";
                    //fileExcel.SaveAs(Server.MapPath("~/App_Data/" + fileName));
                    //string xsltPath = Path.Combine(System.Web.HttpContext.Current.Server.MapPath(@"~/App_Data"), fileName);
                    string xsltPath = Path.Combine(System.Web.HttpContext.Current.Server.MapPath(@"~/App_Data/Excel"), fileName);

                    //string xsltExcel = Path.Combine(System.Web.HttpContext.Current.Server.MapPath(@"~/App_Data/Excel"), "Telephone.xlsx");

                    //FileInfo fileInfo = new FileInfo(xsltExcel);

                    //var i = fileInfo.FullName;

                    //fileInfo.Open(FileMode.Open, FileAccess.Read);


                    //fileInfo.CopyTo(Path.Combine(System.Web.HttpContext.Current.Server.MapPath(@"~/App_Data/Excel/Telephone")));

                    //string xsltExcel = Path.Combine(@"\\forward\Телефонный справочник\Телефонный справочник.xls");

                    //var t = System.Diagnostics.Process.Start(Path.Combine(@"\\forward\Телефонный справочник\Телефонный справочник.xls"));



                    var workbook = new XLWorkbook(xsltPath);
                    var worksheet = workbook.Worksheet(1);
                    var rows = worksheet.RangeUsed().RowsUsed().Skip(1);
                    directory = fileName.Substring(0, fileName.IndexOf('.'));
                    switch (directory)
                    {
                        case "SPR_METR":
                            Spr_metrol spr_metrol = new Spr_metrol();
                            foreach (var row in rows)
                            {
                                spr_metrol.link_podgrupp = Valid_Cell(row.Cell(2).Value.ToString()) == null ? 1 : int.Parse(Valid_Cell(row.Cell(2).Value.ToString()));
                                spr_metrol.link_naznach = Valid_Cell(row.Cell(3).Value.ToString()) == null ? 1 : int.Parse(Valid_Cell(row.Cell(3).Value.ToString()));
                                spr_metrol.link_cex = Valid_Cell(row.Cell(4).Value.ToString()) == null ? 1 : int.Parse(Valid_Cell(row.Cell(4).Value.ToString()));
                                spr_metrol.link_period_poverk = Valid_Cell(row.Cell(5).Value.ToString()) == null ? 1 : int.Parse(Valid_Cell(row.Cell(5).Value.ToString()));
                                spr_metrol.MestoPoverkId = Valid_Cell(row.Cell(6).Value.ToString()) == null ? 1 : int.Parse(Valid_Cell(row.Cell(6).Value.ToString()));
                                spr_metrol.link_mesto_k= Valid_Cell(row.Cell(7).Value.ToString()) == null ? 1 : int.Parse(Valid_Cell(row.Cell(7).Value.ToString()));
                                spr_metrol.link_mesto_rem = Valid_Cell(row.Cell(8).Value.ToString()) == null ? 1 : int.Parse(Valid_Cell(row.Cell(8).Value.ToString()));
                                spr_metrol.link_rod_poverk = Valid_Cell(row.Cell(9).Value.ToString()) == null ? 1 : int.Parse(Valid_Cell(row.Cell(9).Value.ToString()));
                                spr_metrol.link_tip_pribora = Valid_Cell(row.Cell(10).Value.ToString()) == null ? 1 : int.Parse(Valid_Cell(row.Cell(10).Value.ToString()));
                                spr_metrol.link_konserv = Valid_Cell(row.Cell(11).Value.ToString()) == null ? 1 : int.Parse(Valid_Cell(row.Cell(11).Value.ToString()));
                                spr_metrol.n_pasporta = Valid_Cell(row.Cell(12).Value.ToString()) == null ? 0 : int.Parse(Valid_Cell(row.Cell(12).Value.ToString()));
                                spr_metrol.n_zavod = row.Cell(13).Value.ToString();                               
                                

                                if (DateTime.TryParse(row.Cell(14).Value.ToString(), out DateTime data_pover))
                                {
                                    spr_metrol.data_pover = DateTime.Parse(row.Cell(14).Value.ToString());
                                }
                                //if (DateTime.Parse(row.Cell(15).Value.ToString()) != null)
                                //{
                                //    spr_metrol.data_pred_pov = DateTime.Parse(row.Cell(15).Value.ToString());
                                //}
                                spr_metrol.remont = row.Cell(16).Value.ToString();
                                spr_metrol.remont = row.Cell(17).Value.ToString();
                                spr_metrol.link_usl = Valid_Cell(row.Cell(18).Value.ToString()) == null ? 1 : int.Parse(Valid_Cell(row.Cell(18).Value.ToString()));
                                spr_metrol.link_stan = Valid_Cell(row.Cell(19).Value.ToString()) == null ? 1 : int.Parse(Valid_Cell(row.Cell(19).Value.ToString()));
                                spr_metrol.link_ree = Valid_Cell(row.Cell(20).Value.ToString()) == null ? 1 : int.Parse(Valid_Cell(row.Cell(20).Value.ToString()));
                                spr_metrol.god_vip = row.Cell(21).Value.ToString();
                                spr_metrol.link_predpr = Valid_Cell(row.Cell(22).Value.ToString()) == null ? 1 : int.Parse(Valid_Cell(row.Cell(22).Value.ToString()));
                                if (DateTime.Parse(row.Cell(23).Value.ToString()) != null)
                                {
                                    spr_metrol.data_izm = DateTime.Parse(row.Cell(23).Value.ToString());
                                }
                                spr_metrol.DocumentStatusId = Valid_Cell(row.Cell(24).Value.ToString()) == null ? 1 : int.Parse(Valid_Cell(row.Cell(24).Value.ToString()));
                                spr_metrol.link_slugba = Valid_Cell(row.Cell(26).Value.ToString()) == null ? 1 : int.Parse(Valid_Cell(row.Cell(26).Value.ToString()));
                                spr_metrol.link_pvi = (int)PviLevel.Insert;
                                spr_metrol.CustomerId = _workContext.CurrentCustomer.Id;
                                spr_metrol.operation_date = DateTime.Now;
                                spr_metrol.period_open_date = DateTime.Now;
                                _SprMetrolRepository.Insert(spr_metrol);
                                sw.Stop();
                                TempData["timer"] = sw.Elapsed;
                            }
                            break;

                        case "POD_PRIB":
                            Podgr_prib podgr_prib = new Podgr_prib();
                            foreach (var row in rows)
                            {
                                podgr_prib.n_podgrupp = Valid_Cell(row.Cell(2).Value.ToString()) == null ? 0 : int.Parse(Valid_Cell(row.Cell(2).Value.ToString()));
                                podgr_prib.link_nmprib = Valid_Cell(row.Cell(3).Value.ToString()) == null ? 1 : int.Parse(Valid_Cell(row.Cell(3).Value.ToString()));
                                podgr_prib.link_vidiz = Valid_Cell(row.Cell(4).Value.ToString()) == null ? 1 : int.Parse(Valid_Cell(row.Cell(4).Value.ToString()));
                                podgr_prib.DocumentStatusId = Valid_Cell(row.Cell(5).Value.ToString()) == null ? 1 : int.Parse(Valid_Cell(row.Cell(5).Value.ToString()));
                                podgr_prib.link_pvi = (int)PviLevel.Insert;
                                podgr_prib.CustomerId = _workContext.CurrentCustomer.Id;
                                podgr_prib.operation_date = DateTime.Now;
                                podgr_prib.period_open_date = DateTime.Now;
                                _PodgrPribRepository.Insert(podgr_prib);
                                sw.Stop();
                                TempData["timer"] = sw.Elapsed;
                            }
                            break;

                        case "VID_IZME":
                            Vid_izmer vid_izmer = new Vid_izmer();
                            foreach (var row in rows)
                            {
                                vid_izmer.n_vidiz = Valid_Cell(row.Cell(2).Value.ToString()) == null ? 0 : int.Parse(Valid_Cell(row.Cell(2).Value.ToString()));
                                vid_izmer.link_nmvidiz = Valid_Cell(row.Cell(3).Value.ToString()) == null ? 1 : int.Parse(Valid_Cell(row.Cell(3).Value.ToString()));
                                vid_izmer.DocumentStatusId = Valid_Cell(row.Cell(4).Value.ToString()) == null ? 1 : int.Parse(Valid_Cell(row.Cell(4).Value.ToString()));
                                vid_izmer.link_pvi = (int)PviLevel.Insert;
                                vid_izmer.CustomerId = _workContext.CurrentCustomer.Id;
                                vid_izmer.operation_date = DateTime.Now;
                                vid_izmer.period_open_date = DateTime.Now;
                                _VidizmerRepository.Insert(vid_izmer);
                                sw.Stop();
                                TempData["timer"] = sw.Elapsed;
                            }
                            break;

                        case "TIP_PRIB":
                            Tip_pribora tip_pribora = new Tip_pribora();
                            foreach (var row in rows)
                            {
                                tip_pribora.tip_naim = row.Cell(2).Value.ToString();
                                tip_pribora.link_predel = Valid_Cell(row.Cell(3).Value.ToString()) == null ? 1 : int.Parse(Valid_Cell(row.Cell(3).Value.ToString()));
                                tip_pribora.link_cena_del = Valid_Cell(row.Cell(4).Value.ToString()) == null ? 1 : int.Parse(Valid_Cell(row.Cell(4).Value.ToString()));
                                tip_pribora.link_klass_tochn = Valid_Cell(row.Cell(5).Value.ToString()) == null ? 1 : int.Parse(Valid_Cell(row.Cell(5).Value.ToString()));
                                tip_pribora.DocumentStatusId = Valid_Cell(row.Cell(6).Value.ToString()) == null ? 1 : int.Parse(Valid_Cell(row.Cell(6).Value.ToString()));
                                tip_pribora.link_pvi = (int)PviLevel.Insert;
                                tip_pribora.CustomerId = _workContext.CurrentCustomer.Id;
                                tip_pribora.operation_date = DateTime.Now;
                                tip_pribora.period_open_date = DateTime.Now;
                                _TipPriboraRepository.Insert(tip_pribora);
                                sw.Stop();
                                TempData["timer"] = sw.Elapsed;
                            }
                            break;

                        case "USL_EKSP":
                            Usl_expluat usl_expluat = new Usl_expluat();
                            foreach (var row in rows)
                            {
                                usl_expluat.kod_usl = row.Cell(2).Value.ToString();
                                usl_expluat.naim_usl = row.Cell(3).Value.ToString();
                                usl_expluat.DocumentStatusId = Valid_Cell(row.Cell(4).Value.ToString()) == null ? 1 : int.Parse(Valid_Cell(row.Cell(4).Value.ToString()));
                                usl_expluat.link_pvi = (int)PviLevel.Insert;
                                usl_expluat.CustomerId = _workContext.CurrentCustomer.Id;
                                usl_expluat.operation_date = DateTime.Now;
                                usl_expluat.period_open_date = DateTime.Now;
                                _UslExpluatRepository.Insert(usl_expluat);
                                sw.Stop();
                                TempData["timer"] = sw.Elapsed;
                            }
                            break;

                        case "SPR_STAN":
                            Spr_stan spr_stan = new Spr_stan();
                            foreach (var row in rows)
                            {
                                spr_stan.stan = row.Cell(2).Value.ToString();
                                spr_stan.n_stan = row.Cell(3).Value.ToString();
                                spr_stan.DocumentStatusId = Valid_Cell(row.Cell(4).Value.ToString()) == null ? 1 : int.Parse(Valid_Cell(row.Cell(4).Value.ToString()));
                                spr_stan.link_pvi = (int)PviLevel.Insert;
                                spr_stan.CustomerId = _workContext.CurrentCustomer.Id;
                                spr_stan.operation_date = DateTime.Now;
                                spr_stan.period_open_date = DateTime.Now;
                                _SprStanRepository.Insert(spr_stan);
                                sw.Stop();
                                TempData["timer"] = sw.Elapsed;
                            }
                            break;

                        case "SPR_PRED":
                            Spr_predel spr_predel = new Spr_predel();
                            foreach (var row in rows)
                            {
                                spr_predel.predel = row.Cell(2).Value.ToString();
                                spr_predel.DocumentStatusId = Valid_Cell(row.Cell(3).Value.ToString()) == null ? 1 : int.Parse(Valid_Cell(row.Cell(3).Value.ToString()));
                                spr_predel.link_pvi = (int)PviLevel.Insert;
                                spr_predel.CustomerId = _workContext.CurrentCustomer.Id;
                                spr_predel.operation_date = DateTime.Now;
                                spr_predel.period_open_date = DateTime.Now;
                                _SprPredelRepository.Insert(spr_predel);
                                sw.Stop();
                                TempData["timer"] = sw.Elapsed;
                            }
                            break;

                        case "SPR_KLAS":
                            Spr_klass_tochn spr_klass_tochn = new Spr_klass_tochn();
                            foreach (var row in rows)
                            {
                                spr_klass_tochn.klass_tochn = row.Cell(2).Value.ToString();
                                spr_klass_tochn.DocumentStatusId = Valid_Cell(row.Cell(3).Value.ToString()) == null ? 1 : int.Parse(Valid_Cell(row.Cell(3).Value.ToString()));
                                spr_klass_tochn.link_pvi = (int)PviLevel.Insert;
                                spr_klass_tochn.CustomerId = _workContext.CurrentCustomer.Id;
                                spr_klass_tochn.operation_date = DateTime.Now;
                                spr_klass_tochn.period_open_date = DateTime.Now;
                                _SprKlassTochnRepository.Insert(spr_klass_tochn);
                                sw.Stop();
                                TempData["timer"] = sw.Elapsed;
                            }
                            break;

                        case "SPR_CENA":
                            Spr_cena_del spr_cena_del = new Spr_cena_del();
                            foreach (var row in rows)
                            {
                                spr_cena_del.cena_del = row.Cell(2).Value.ToString();
                                spr_cena_del.DocumentStatusId = Valid_Cell(row.Cell(3).Value.ToString()) == null ? 1 : int.Parse(Valid_Cell(row.Cell(3).Value.ToString()));
                                spr_cena_del.link_pvi = (int)PviLevel.Insert;
                                spr_cena_del.CustomerId = _workContext.CurrentCustomer.Id;
                                spr_cena_del.operation_date = DateTime.Now;
                                spr_cena_del.period_open_date = DateTime.Now;
                                _SprCenaDelRepository.Insert(spr_cena_del);
                                sw.Stop();
                                TempData["timer"] = sw.Elapsed;
                            }
                            break;

                        case "ROD_POV":
                            Rod_poverk rod_poverk = new Rod_poverk();
                            foreach (var row in rows)
                            {
                                rod_poverk.n_rod_poverk = row.Cell(2).Value.ToString();
                                rod_poverk.naim_rod = row.Cell(3).Value.ToString();
                                rod_poverk.DocumentStatusId = Valid_Cell(row.Cell(4).Value.ToString()) == null ? 1 : int.Parse(Valid_Cell(row.Cell(4).Value.ToString()));
                                rod_poverk.link_pvi = (int)PviLevel.Insert;
                                rod_poverk.CustomerId = _workContext.CurrentCustomer.Id;
                                rod_poverk.operation_date = DateTime.Now;
                                rod_poverk.period_open_date = DateTime.Now;
                                _RodPoverkRepository.Insert(rod_poverk);
                                sw.Stop();
                                TempData["timer"] = sw.Elapsed;
                            }
                            break;

                        case "PRED_IZG":
                            Predpr_izg predpr_izg = new Predpr_izg();
                            foreach (var row in rows)
                            {
                                predpr_izg.predpr = row.Cell(2).Value.ToString();
                                predpr_izg.DocumentStatusId = Valid_Cell(row.Cell(3).Value.ToString()) == null ? 1 : int.Parse(Valid_Cell(row.Cell(3).Value.ToString()));
                                predpr_izg.link_pvi = (int)PviLevel.Insert;
                                predpr_izg.CustomerId = _workContext.CurrentCustomer.Id;
                                predpr_izg.operation_date = DateTime.Now;
                                predpr_izg.period_open_date = DateTime.Now;
                                _PredprIzgRepository.Insert(predpr_izg);
                                sw.Stop();
                                TempData["timer"] = sw.Elapsed;
                            }
                            break;

                        case "PER_POV":
                            Period_pover period_pover = new Period_pover();
                            foreach (var row in rows)
                            {
                                period_pover.period = row.Cell(2).Value.ToString();
                                period_pover.DocumentStatusId = Valid_Cell(row.Cell(3).Value.ToString()) == null ? 1 : int.Parse(Valid_Cell(row.Cell(3).Value.ToString()));
                                period_pover.link_pvi = (int)PviLevel.Insert;
                                period_pover.CustomerId = _workContext.CurrentCustomer.Id;
                                period_pover.operation_date = DateTime.Now;
                                period_pover.period_open_date = DateTime.Now;
                                _PeriodPoverRepository.Insert(period_pover);
                                sw.Stop();
                                TempData["timer"] = sw.Elapsed;
                            }
                            break;

                        case "NM_VIDIZ":
                            Nm_vidiz nm_vidiz = new Nm_vidiz();
                            foreach (var row in rows)
                            {
                                nm_vidiz.nm_vidiz1 = row.Cell(2).Value.ToString();
                                nm_vidiz.DocumentStatusId = Valid_Cell(row.Cell(3).Value.ToString()) == null ? 1 : int.Parse(Valid_Cell(row.Cell(3).Value.ToString()));
                                nm_vidiz.link_pvi = (int)PviLevel.Insert;
                                nm_vidiz.CustomerId = _workContext.CurrentCustomer.Id;
                                nm_vidiz.operation_date = DateTime.Now;
                                nm_vidiz.period_open_date = DateTime.Now;
                                _NmVidizRepository.Insert(nm_vidiz);
                                sw.Stop();
                                TempData["timer"] = sw.Elapsed;
                            }
                            break;

                        case "NM_PRIB":
                            Nm_prib nm_prib = new Nm_prib();
                            foreach (var row in rows)
                            {
                                nm_prib.nm_prib1 = row.Cell(2).Value.ToString();
                                nm_prib.DocumentStatusId = Valid_Cell(row.Cell(3).Value.ToString()) == null ? 1 : int.Parse(Valid_Cell(row.Cell(3).Value.ToString()));
                                nm_prib.link_pvi = (int)PviLevel.Insert;
                                nm_prib.CustomerId = _workContext.CurrentCustomer.Id;
                                nm_prib.operation_date = DateTime.Now;
                                nm_prib.period_open_date = DateTime.Now;
                                _NmPribRepository.Insert(nm_prib);
                                sw.Stop();
                                TempData["timer"] = sw.Elapsed;
                            }
                            break;

                        case "NAZN_PRI":
                            Nazn_prib nazn_prib = new Nazn_prib();
                            foreach (var row in rows)
                            {
                                nazn_prib.naznach = row.Cell(2).Value.ToString();
                                nazn_prib.n_naznach = row.Cell(3).Value.ToString();
                                nazn_prib.DocumentStatusId = Valid_Cell(row.Cell(4).Value.ToString()) == null ? 1 : int.Parse(Valid_Cell(row.Cell(4).Value.ToString()));
                                nazn_prib.link_pvi = (int)PviLevel.Insert;
                                nazn_prib.CustomerId = _workContext.CurrentCustomer.Id;
                                nazn_prib.operation_date = DateTime.Now;
                                nazn_prib.period_open_date = DateTime.Now;
                                _NaznPribRepository.Insert(nazn_prib);
                                sw.Stop();
                                TempData["timer"] = sw.Elapsed;
                            }
                            break;

                        case "KONSERV":
                            Konserv konserv = new Konserv();
                            foreach (var row in rows)
                            {
                                konserv.kod_konserv = row.Cell(2).Value.ToString();
                                konserv.nm_konserv = row.Cell(3).Value.ToString();
                                konserv.DocumentStatusId = Valid_Cell(row.Cell(4).Value.ToString()) == null ? 1 : int.Parse(Valid_Cell(row.Cell(4).Value.ToString()));
                                konserv.link_pvi = (int)PviLevel.Insert;
                                konserv.CustomerId = _workContext.CurrentCustomer.Id;
                                konserv.operation_date = DateTime.Now;
                                konserv.period_open_date = DateTime.Now;
                                _KonservRepository.Insert(konserv);
                                sw.Stop();
                                TempData["timer"] = sw.Elapsed;
                            }
                            break;

                        case "GOS_REE":
                            Gos_ree gos_ree = new Gos_ree();
                            foreach (var row in rows)
                            {
                                gos_ree.n_gos_ree = row.Cell(2).Value.ToString();
                                gos_ree.DocumentStatusId = Valid_Cell(row.Cell(3).Value.ToString()) == null ? 1 : int.Parse(Valid_Cell(row.Cell(3).Value.ToString()));
                                gos_ree.link_pvi = (int)PviLevel.Insert;
                                gos_ree.CustomerId = _workContext.CurrentCustomer.Id;
                                gos_ree.operation_date = DateTime.Now;
                                gos_ree.period_open_date = DateTime.Now;
                                _gosReeRepository.Insert(gos_ree);
                                sw.Stop();
                                TempData["timer"] = sw.Elapsed;
                            }
                            break;

                        case "GRRAZIZD":
                            GR_RAZDIZD gr_razd_izd = new GR_RAZDIZD();
                            foreach (var row in rows)
                            {
                                gr_razd_izd.IzdId = Valid_Cell(row.Cell(2).Value.ToString()) == null ? 1 : int.Parse(Valid_Cell(row.Cell(2).Value.ToString()));
                                gr_razd_izd.RazdIzdId = Valid_Cell(row.Cell(3).Value.ToString()) == null ? 1 : int.Parse(Valid_Cell(row.Cell(3).Value.ToString()));
                                gr_razd_izd.Shifr = row.Cell(4).Value.ToString();
                                gr_razd_izd.NmGrup = row.Cell(5).Value.ToString();
                                gr_razd_izd.GolSborkId = Valid_Cell(row.Cell(6).Value.ToString()) == null ? 1 : int.Parse(Valid_Cell(row.Cell(6).Value.ToString()));
                                gr_razd_izd.PviId = Valid_Cell(row.Cell(7).Value.ToString()) == null ? (int)PviLevel.Insert : int.Parse(Valid_Cell(row.Cell(7).Value.ToString()));
                                gr_razd_izd.DocumentStatusId = Valid_Cell(row.Cell(8).Value.ToString()) == null ? 1 : int.Parse(Valid_Cell(row.Cell(8).Value.ToString()));
                                gr_razd_izd.OperationDate = DateTime.Now;
                                gr_razd_izd.PeriodOpenDate = DateTime.Now;
                                _GrRazdIzdRepository.Insert(gr_razd_izd);
                                sw.Stop();
                                TempData["timer"] = sw.Elapsed;
                            }
                            break;

                        case "TR_OBOZN":
                            DirectiveWork directiveWork = new DirectiveWork();
                            foreach (var row in rows)
                            {
                                directiveWork.PkpId = Valid_Cell(row.Cell(2).Value.ToString()) == null ? 1 : int.Parse(Valid_Cell(row.Cell(2).Value.ToString()));
                                directiveWork.OboznId = Valid_Cell(row.Cell(3).Value.ToString()) == null ? 1 : int.Parse(Valid_Cell(row.Cell(3).Value.ToString()));
                                directiveWork.CexIzgId = Valid_Cell(row.Cell(4).Value.ToString()) == null ? 1 : int.Parse(Valid_Cell(row.Cell(4).Value.ToString()));
                                directiveWork.CexPotrId = Valid_Cell(row.Cell(5).Value.ToString()) == null ? 1 : int.Parse(Valid_Cell(row.Cell(5).Value.ToString()));
                                if (String.IsNullOrEmpty(row.Cell(6).Value.ToString()) || String.IsNullOrWhiteSpace(row.Cell(6).Value.ToString()))
                                {
                                    directiveWork.Directive_work_sdeln_izg = null;
                                }
                                else
                                {
                                    try
                                    {
                                        directiveWork.Directive_work_sdeln_izg = decimal.Parse(row.Cell(6).Value.ToString());
                                        //tto.nrvp = decimal.Parse(row.Cell(8).Value.ToString(), NumberStyles.Any, CultureInfo.InvariantCulture);
                                    }
                                    catch (Exception)
                                    {
                                        directiveWork.Directive_work_sdeln_izg = decimal.Parse(row.Cell(6).Value.ToString(), NumberStyles.Any, CultureInfo.InvariantCulture);
                                    }
                                }
                                if (String.IsNullOrEmpty(row.Cell(7).Value.ToString()) || String.IsNullOrWhiteSpace(row.Cell(7).Value.ToString()))
                                {
                                    directiveWork.Directive_work_povr_izg = null;
                                }
                                else
                                {
                                    try
                                    {
                                        directiveWork.Directive_work_povr_izg = decimal.Parse(row.Cell(7).Value.ToString());
                                        //tto.nrvp = decimal.Parse(row.Cell(8).Value.ToString(), NumberStyles.Any, CultureInfo.InvariantCulture);
                                    }
                                    catch (Exception)
                                    {
                                        directiveWork.Directive_work_povr_izg = decimal.Parse(row.Cell(7).Value.ToString(), NumberStyles.Any, CultureInfo.InvariantCulture);
                                    }
                                }
                                if (String.IsNullOrEmpty(row.Cell(8).Value.ToString()) || String.IsNullOrWhiteSpace(row.Cell(8).Value.ToString()))
                                {
                                    directiveWork.Directive_work_sdeln_usl = null;
                                }
                                else
                                {
                                    try
                                    {
                                        directiveWork.Directive_work_sdeln_usl = decimal.Parse(row.Cell(8).Value.ToString());
                                        //tto.nrvp = decimal.Parse(row.Cell(8).Value.ToString(), NumberStyles.Any, CultureInfo.InvariantCulture);
                                    }
                                    catch (Exception)
                                    {
                                        directiveWork.Directive_work_sdeln_usl = decimal.Parse(row.Cell(8).Value.ToString(), NumberStyles.Any, CultureInfo.InvariantCulture);
                                    }
                                }
                                if (String.IsNullOrEmpty(row.Cell(9).Value.ToString()) || String.IsNullOrWhiteSpace(row.Cell(9).Value.ToString()))
                                {
                                    directiveWork.Directive_work_povr_usl = null;
                                }
                                else
                                {
                                    try
                                    {
                                        directiveWork.Directive_work_povr_usl = decimal.Parse(row.Cell(9).Value.ToString());
                                        //tto.nrvp = decimal.Parse(row.Cell(8).Value.ToString(), NumberStyles.Any, CultureInfo.InvariantCulture);
                                    }
                                    catch (Exception)
                                    {
                                        directiveWork.Directive_work_povr_usl = decimal.Parse(row.Cell(9).Value.ToString(), NumberStyles.Any, CultureInfo.InvariantCulture);
                                    }
                                }
                                directiveWork.ExceptionForWorkId = Valid_Cell(row.Cell(10).Value.ToString()) == null ? 1 : int.Parse(Valid_Cell(row.Cell(10).Value.ToString()));
                                directiveWork.Prim = row.Cell(11).Value.ToString();
                                directiveWork.NomDok = row.Cell(12).Value.ToString();
                                directiveWork.SprPviId = Valid_Cell(row.Cell(13).Value.ToString()) == null ? (int)PviLevel.Insert : int.Parse(Valid_Cell(row.Cell(13).Value.ToString()));
                                directiveWork.DocumentStatusId = Valid_Cell(row.Cell(14).Value.ToString()) == null ? 1 : int.Parse(Valid_Cell(row.Cell(14).Value.ToString()));
                                directiveWork.CustomerId = _workContext.CurrentCustomer.Id;
                                directiveWork.Operation = Operation;
                                directiveWork.OperationDate = DateTime.Now;
                                directiveWork.PeriodOpenDate = DateTime.Now;
                                _DirectiveWorkRepository.Insert(directiveWork);
                                sw.Stop();
                                TempData["timer"] = sw.Elapsed;
                            }
                            break;

                        case "RAZD_DSE":
                            Spr_Razd_DSE razd_dse = new Spr_Razd_DSE();
                            foreach (var row in rows)
                            {
                                razd_dse.NmRazd_K = row.Cell(2).Value.ToString();
                                razd_dse.NmRazd_P = row.Cell(3).Value.ToString();
                                razd_dse.PviId = Valid_Cell(row.Cell(4).Value.ToString()) == null ? (int)PviLevel.Insert : int.Parse(Valid_Cell(row.Cell(4).Value.ToString()));
                                razd_dse.DocumentStatusId = Valid_Cell(row.Cell(5).Value.ToString()) == null ? 1 : int.Parse(Valid_Cell(row.Cell(5).Value.ToString()));
                                razd_dse.OperationDate = DateTime.Now;
                                razd_dse.PeriodOpenDate = DateTime.Now;
                                _razDseRepository.Insert(razd_dse);
                                sw.Stop();
                                TempData["timer"] = sw.Elapsed;
                            }
                            break;

                        case "TR_ISKL":
                            ExceptionForWork exceptionForWork = new ExceptionForWork();
                            foreach (var row in rows)
                            {
                                exceptionForWork.ShortName = row.Cell(2).Value.ToString();
                                exceptionForWork.FullName = row.Cell(3).Value.ToString();
                                exceptionForWork.Prim = row.Cell(4).Value.ToString();
                                exceptionForWork.SprPviId = Valid_Cell(row.Cell(5).Value.ToString()) == null ? (int)PviLevel.Insert : int.Parse(Valid_Cell(row.Cell(5).Value.ToString()));
                                exceptionForWork.DocumentStatusId = Valid_Cell(row.Cell(6).Value.ToString()) == null ? 1 : int.Parse(Valid_Cell(row.Cell(6).Value.ToString()));
                                exceptionForWork.OperationDate = DateTime.Now;
                                exceptionForWork.PeriodOpenDate = DateTime.Now;
                                _ExceptionForWorkRepository.Insert(exceptionForWork);
                                sw.Stop();
                                TempData["timer"] = sw.Elapsed;
                            }
                            break;

                        case "USL_SKM":
                            UslSkm usl_Skm = new UslSkm();
                            foreach (var row in rows)
                            {
                                usl_Skm.KolSimvol = Valid_Cell(row.Cell(2).Value.ToString()) == null ? 0 : int.Parse(Valid_Cell(row.Cell(2).Value.ToString()));
                                usl_Skm.PerOgt = row.Cell(3).Value.ToString();
                                usl_Skm.UslSort = row.Cell(4).Value.ToString();
                                usl_Skm.Spr_pviId = Valid_Cell(row.Cell(5).Value.ToString()) == null ? (int)PviLevel.Insert : int.Parse(Valid_Cell(row.Cell(5).Value.ToString()));
                                usl_Skm.StatusDocumentId = Valid_Cell(row.Cell(6).Value.ToString()) == null ? 1 : int.Parse(Valid_Cell(row.Cell(6).Value.ToString()));
                                usl_Skm.OperationDate = DateTime.Now;
                                usl_Skm.PeriodOpenDate = DateTime.Now;
                                _uslSkmRepository.Insert(usl_Skm);
                                sw.Stop();
                                TempData["timer"] = sw.Elapsed;
                            }
                            break;

                        case "RAZ_DET":
                            Raz_det razd_det = new Raz_det();
                            foreach (var row in rows)
                            {
                                razd_det.Razd = row.Cell(2).Value.ToString();
                                razd_det.NaimRazd = row.Cell(3).Value.ToString();
                                razd_det.PviId = Valid_Cell(row.Cell(4).Value.ToString()) == null ? (int)PviLevel.Insert : int.Parse(Valid_Cell(row.Cell(4).Value.ToString()));
                                razd_det.DocumentStatusId = Valid_Cell(row.Cell(5).Value.ToString()) == null ? 1 : int.Parse(Valid_Cell(row.Cell(5).Value.ToString()));
                                razd_det.OperationDate = DateTime.Now;
                                razd_det.PeriodOpenDate = DateTime.Now;
                                _razDetRepository.Insert(razd_det);
                                sw.Stop();
                                TempData["timer"] = sw.Elapsed;
                            }
                            break;

                        case "RAZD_IZD":
                            Spr_Razd_Izd razd_izd = new Spr_Razd_Izd();
                            foreach (var row in rows)
                            {
                                razd_izd.NmRazdIzd = row.Cell(2).Value.ToString();
                                razd_izd.PviId = Valid_Cell(row.Cell(3).Value.ToString()) == null ? (int)PviLevel.Insert : int.Parse(Valid_Cell(row.Cell(3).Value.ToString()));
                                razd_izd.DocumentStatusId = Valid_Cell(row.Cell(4).Value.ToString()) == null ? 1 : int.Parse(Valid_Cell(row.Cell(4).Value.ToString()));
                                razd_izd.OperationDate = DateTime.Now;
                                razd_izd.PeriodOpenDate = DateTime.Now;
                                _razIzdRepository.Insert(razd_izd);
                                sw.Stop();
                                TempData["timer"] = sw.Elapsed;
                            }
                            break;

                        case "SPR_KDAN":
                            Spr_kdan kdan = new Spr_kdan();
                            foreach (var row in rows)
                            {
                                kdan.Kdan = row.Cell(2).Value.ToString();
                                kdan.NmKdan = row.Cell(3).Value.ToString();
                                kdan.PviId = Valid_Cell(row.Cell(4).Value.ToString()) == null ? (int)PviLevel.Insert : int.Parse(Valid_Cell(row.Cell(4).Value.ToString()));
                                kdan.DocumentStatusId = Valid_Cell(row.Cell(5).Value.ToString()) == null ? 1 : int.Parse(Valid_Cell(row.Cell(5).Value.ToString()));
                                kdan.OperationDate = DateTime.Now;
                                kdan.PeriodOpenDate = DateTime.Now;
                                _kdanRepository.Insert(kdan);
                                sw.Stop();
                                TempData["timer"] = sw.Elapsed;
                            }
                            break;

                        case "S_CEXPR":
                            Spr_cex_prizn cex_pr = new Spr_cex_prizn();
                            foreach (var row in rows)
                            {
                                cex_pr.NmPrcex_k = row.Cell(2).Value.ToString();
                                cex_pr.NmPrcex_p = row.Cell(3).Value.ToString();
                                cex_pr.NmGrup = row.Cell(4).Value.ToString();
                                cex_pr.PviId = Valid_Cell(row.Cell(5).Value.ToString()) == null ? (int)PviLevel.Insert : int.Parse(Valid_Cell(row.Cell(5).Value.ToString()));
                                cex_pr.DocumentStatusId = Valid_Cell(row.Cell(6).Value.ToString()) == null ? 1 : int.Parse(Valid_Cell(row.Cell(6).Value.ToString()));
                                cex_pr.OperationDate = DateTime.Now;
                                cex_pr.PeriodOpenDate = DateTime.Now;
                                _cexPriznRepository.Insert(cex_pr);
                                sw.Stop();
                                TempData["timer"] = sw.Elapsed;
                            }
                            break;

                        case "S_KOMPL":
                            Spr_kompl s_kompl = new Spr_kompl();
                            foreach (var row in rows)
                            {
                                s_kompl.Komplekt = row.Cell(2).Value.ToString();
                                if (int.Parse(row.Cell(3).Value.ToString()) == 0 || (String.IsNullOrEmpty(row.Cell(3).Value.ToString()) || String.IsNullOrWhiteSpace(row.Cell(3).Value.ToString())))
                                {
                                    s_kompl.NaKompl = null;
                                }
                                else
                                    s_kompl.NaKompl = int.Parse(row.Cell(3).Value.ToString());

                                s_kompl.PviId = Valid_Cell(row.Cell(4).Value.ToString()) == null ? (int)PviLevel.Insert : int.Parse(Valid_Cell(row.Cell(4).Value.ToString()));
                                s_kompl.DocumentStatusId = Valid_Cell(row.Cell(5).Value.ToString()) == null ? 1 : int.Parse(Valid_Cell(row.Cell(5).Value.ToString()));
                                s_kompl.OperationDate = DateTime.Now;
                                s_kompl.PeriodOpenDate = DateTime.Now;
                                _komplRepository.Insert(s_kompl);
                                sw.Stop();
                                TempData["timer"] = sw.Elapsed;
                            }
                            break;

                        case "SPR_CEX":
                            Spr_cex s_cex = new Spr_cex();
                            foreach (var row in rows)
                            {
                                s_cex.Cex = row.Cell(2).Value.ToString();
                                s_cex.NmCexKrat = row.Cell(3).Value.ToString();
                                s_cex.NaimCex = row.Cell(4).Value.ToString();
                                s_cex.CexRealId = Valid_Cell(row.Cell(5).Value.ToString()) == null ? 1 : int.Parse(Valid_Cell(row.Cell(5).Value.ToString()));
                                s_cex.PviId = Valid_Cell(row.Cell(6).Value.ToString()) == null ? (int)PviLevel.Insert : int.Parse(Valid_Cell(row.Cell(6).Value.ToString()));
                                s_cex.DocumentStatusId = Valid_Cell(row.Cell(7).Value.ToString()) == null ? 1 : int.Parse(Valid_Cell(row.Cell(7).Value.ToString()));
                                s_cex.OperationDate = DateTime.Now;
                                s_cex.PeriodOpenDate = DateTime.Now;
                                _cexRepository.Insert(s_cex);
                                sw.Stop();
                                TempData["timer"] = sw.Elapsed;
                            }
                            break;

                        case "S_PRASC":

                            Spr_poln_rascex s_poln_rasce = new Spr_poln_rascex();
                            foreach (var row in rows)
                            {
                                s_poln_rasce.RascizdId = Valid_Cell(row.Cell(2).Value.ToString()) == null ? 1 : int.Parse(Valid_Cell(row.Cell(2).Value.ToString()));
                                //s_poln_rasce.Cp1_Id = Valid_Cell(row.Cell(3).Value.ToString()) == null ? 1 : int.Parse(Valid_Cell(row.Cell(3).Value.ToString()));
                                //s_poln_rasce.Cp2_Id = Valid_Cell(row.Cell(4).Value.ToString()) == null ? 1 : int.Parse(Valid_Cell(row.Cell(4).Value.ToString()));
                                //s_poln_rasce.Cp3_Id = Valid_Cell(row.Cell(5).Value.ToString()) == null ? 1 : int.Parse(Valid_Cell(row.Cell(5).Value.ToString()));
                                s_poln_rasce.Cp1_Id = int.Parse(row.Cell(3).Value.ToString());
                                if (s_poln_rasce.Cp1_Id == 0 || s_poln_rasce.Cp1_Id == null)
                                {
                                    s_poln_rasce.Cp1_Id = null;
                                }
                                else
                                {
                                    s_poln_rasce.Cp1_Id = int.Parse(row.Cell(3).Value.ToString());
                                }
                                s_poln_rasce.Cp2_Id = int.Parse(row.Cell(4).Value.ToString());
                                if (s_poln_rasce.Cp2_Id == 0 || s_poln_rasce.Cp2_Id == null)
                                {
                                    s_poln_rasce.Cp2_Id = null;
                                }
                                else
                                {
                                    s_poln_rasce.Cp2_Id = int.Parse(row.Cell(4).Value.ToString());
                                }
                                s_poln_rasce.Cp3_Id = int.Parse(row.Cell(5).Value.ToString());
                                if (s_poln_rasce.Cp3_Id == 0 || s_poln_rasce.Cp3_Id == null)
                                {
                                    s_poln_rasce.Cp3_Id = null;
                                }
                                else
                                {
                                    s_poln_rasce.Cp3_Id = int.Parse(row.Cell(5).Value.ToString());
                                }
                                s_poln_rasce.Rascex = row.Cell(6).Value.ToString();
                                s_poln_rasce.PviId = Valid_Cell(row.Cell(7).Value.ToString()) == null ? (int)PviLevel.Insert : int.Parse(Valid_Cell(row.Cell(7).Value.ToString()));
                                s_poln_rasce.DocumentStatusId = Valid_Cell(row.Cell(8).Value.ToString()) == null ? 1 : int.Parse(Valid_Cell(row.Cell(8).Value.ToString()));
                                s_poln_rasce.OperationDate = DateTime.Now;
                                s_poln_rasce.PeriodOpenDate = DateTime.Now;
                                _polnRascexRepository.Insert(s_poln_rasce);
                                sw.Stop();
                                TempData["timer"] = sw.Elapsed;
                            }
                            break;

                        case "S_GRPRIM":
                            spr_grup_prim s_grprim = new spr_grup_prim();
                            foreach (var row in rows)
                            {
                                s_grprim.NmGrPrim = row.Cell(2).Value.ToString();
                                s_grprim.NmGrPrim_k = row.Cell(3).Value.ToString();
                                s_grprim.PviId = Valid_Cell(row.Cell(4).Value.ToString()) == null ? (int)PviLevel.Insert : int.Parse(Valid_Cell(row.Cell(4).Value.ToString()));
                                s_grprim.DocumentStatusId = Valid_Cell(row.Cell(5).Value.ToString()) == null ? 1 : int.Parse(Valid_Cell(row.Cell(5).Value.ToString()));
                                s_grprim.OperationDate = DateTime.Now;
                                s_grprim.PeriodOpenDate = DateTime.Now;
                                _grupPrimRepository.Insert(s_grprim);
                                sw.Stop();
                                TempData["timer"] = sw.Elapsed;
                            }
                            break;

                        case "SPR_AGR":
                            Spr_agr agr = new Spr_agr();
                            foreach (var row in rows)
                            {
                                agr.GrKonstr = row.Cell(2).Value.ToString();
                                agr.Agrk_k = row.Cell(3).Value.ToString();
                                agr.Agrk_p = row.Cell(4).Value.ToString();
                                agr.AgrGrId = Valid_Cell(row.Cell(5).Value.ToString()) == null ? 1 : int.Parse(Valid_Cell(row.Cell(5).Value.ToString()));
                                agr.PviId = Valid_Cell(row.Cell(6).Value.ToString()) == null ? (int)PviLevel.Insert : int.Parse(Valid_Cell(row.Cell(6).Value.ToString()));
                                agr.DocumentStatusId = Valid_Cell(row.Cell(7).Value.ToString()) == null ? 1 : int.Parse(Valid_Cell(row.Cell(7).Value.ToString()));
                                agr.OperationDate = DateTime.Now;
                                agr.PeriodOpenDate = DateTime.Now;
                                _agrRepository.Insert(agr);
                                sw.Stop();
                                TempData["timer"] = sw.Elapsed;
                            }
                            break;

                        case "SPR_PKP":
                            Spr_pkp pkp = new Spr_pkp();
                            foreach (var row in rows)
                            {
                                pkp.Pkp = row.Cell(2).Value.ToString();
                                pkp.NmPkp = row.Cell(3).Value.ToString();
                                pkp.RazdId = Valid_Cell(row.Cell(4).Value.ToString()) == null ? 1 : int.Parse(Valid_Cell(row.Cell(4).Value.ToString()));
                                pkp.RazdDse = Valid_Cell(row.Cell(5).Value.ToString()) == null ? 1 : int.Parse(Valid_Cell(row.Cell(5).Value.ToString()));
                                pkp.PkpDos = row.Cell(6).Value.ToString();
                                pkp.ImageFileName = row.Cell(7).Value.ToString();
                                pkp.PviId = Valid_Cell(row.Cell(8).Value.ToString()) == null ? (int)PviLevel.Insert : int.Parse(Valid_Cell(row.Cell(8).Value.ToString()));
                                pkp.DocumentStatusId = Valid_Cell(row.Cell(9).Value.ToString()) == null ? 1 : int.Parse(Valid_Cell(row.Cell(9).Value.ToString()));
                                pkp.OperationDate = DateTime.Now;
                                pkp.PeriodOpenDate = DateTime.Now;
                                _pkpRepository.Insert(pkp);
                                sw.Stop();
                                TempData["timer"] = sw.Elapsed;
                            }
                            break;

                        case "SPERIZD":
                            Spr_Perizd perizd = new Spr_Perizd();
                            foreach (var row in rows)
                            {
                                perizd.Izdelie = row.Cell(2).Value.ToString();
                                perizd.KodIzd = row.Cell(3).Value.ToString();
                                perizd.NmIzd = row.Cell(4).Value.ToString();
                                perizd.Ser_ss = row.Cell(5).Value.ToString();
                                perizd.Ser_spo = row.Cell(6).Value.ToString();
                                perizd.Soot_ss = row.Cell(7).Value.ToString();
                                perizd.Soot_spo = row.Cell(8).Value.ToString();
                                perizd.Kgk1_1 = row.Cell(9).Value.ToString();
                                perizd.Kgk1_n = row.Cell(10).Value.ToString();
                                perizd.Kgk1_m = row.Cell(11).Value.ToString();
                                perizd.TemaId = Valid_Cell(row.Cell(12).Value.ToString()) == null ? 1 : int.Parse(Valid_Cell(row.Cell(12).Value.ToString()));
                                perizd.Model_tehnol_podgot_proizv = Valid_Cell(row.Cell(13).Value.ToString()) == null ? 0 : int.Parse(Valid_Cell(row.Cell(13).Value.ToString()));
                                perizd.Prim = row.Cell(14).Value.ToString();
                                perizd.TematikId = Valid_Cell(row.Cell(15).Value.ToString()) == null ? 1 : int.Parse(Valid_Cell(row.Cell(15).Value.ToString()));
                                perizd.PrKomplektov = row.Cell(16).Value.ToString();
                                perizd.ZakazId = Valid_Cell(row.Cell(17).Value.ToString()) == null ? 1 : int.Parse(Valid_Cell(row.Cell(17).Value.ToString()));
                                perizd.PviId = Valid_Cell(row.Cell(18).Value.ToString()) == null ? (int)PviLevel.Insert : int.Parse(Valid_Cell(row.Cell(18).Value.ToString()));
                                perizd.DocumentStatusId = Valid_Cell(row.Cell(19).Value.ToString()) == null ? 1 : int.Parse(Valid_Cell(row.Cell(19).Value.ToString()));
                                perizd.OperationDate = DateTime.Now;
                                perizd.period_open_date = DateTime.Now;
                                _perizdRepository.Insert(perizd);
                                sw.Stop();
                                TempData["timer"] = sw.Elapsed;
                            }
                            break;
                        //case "Team_Center_NX":
                        //    //int? z_position = 0;
                        //    //string z_kts = null;
                        //    ReplacingPairsOfLetterController replacing = new ReplacingPairsOfLetterController();
                        //    Team_Center_NX _team_center_NX = new Team_Center_NX();
                        //    foreach (var row in rows)
                        //    {
                        //        if (String.IsNullOrEmpty(row.Cell(2).Value.ToString()) || String.IsNullOrWhiteSpace(row.Cell(2).Value.ToString()))
                        //        {
                        //            continue;
                        //        }
                        //        if (!String.IsNullOrEmpty(row.Cell(1).Value.ToString()) || !String.IsNullOrWhiteSpace(row.Cell(1).Value.ToString()))
                        //        {
                        //            _team_center_NX.Object = row.Cell(1).Value.ToString().ToUpper();
                        //            var outObject = replacing.TranslitFileName(_team_center_NX.Object);
                        //            _team_center_NX.Object = outObject;
                        //        }
                        //        else
                        //        {
                        //            _team_center_NX.Object = null;
                        //        }
                        //        _team_center_NX.Position = ((_team_center_NX.Object.TrimEnd().Length) - (_team_center_NX.Object.TrimStart().Length)) / 4 + 1;
                        //        _team_center_NX.Designation = row.Cell(2).Value.ToString();
                        //        _team_center_NX.Name = row.Cell(3).Value.ToString();
                        //        _team_center_NX.Revision = row.Cell(4).Value.ToString();
                        //        _team_center_NX.Description = row.Cell(5).Value.ToString();
                        //        _team_center_NX.Updated = row.Cell(6).Value.ToString();
                        //        if (String.IsNullOrEmpty(row.Cell(7).Value.ToString()) || String.IsNullOrWhiteSpace(row.Cell(7).Value.ToString()))
                        //        {
                        //            _team_center_NX.Quantity = null;
                        //        }
                        //        else
                        //            _team_center_NX.Quantity = int.Parse(row.Cell(7).Value.ToString());
                        //        _team_center_NX.ReferenceSet = row.Cell(8).Value.ToString();
                        //        if (String.IsNullOrEmpty(row.Cell(9).Value.ToString()) || String.IsNullOrWhiteSpace(row.Cell(9).Value.ToString()))
                        //        {
                        //            _team_center_NX.Weight = null;
                        //        }
                        //        else
                        //        {
                        //            try
                        //            {
                        //                _team_center_NX.Weight = decimal.Parse(row.Cell(9).Value.ToString());
                        //            }
                        //            catch (Exception)
                        //            {
                        //                _team_center_NX.Weight = decimal.Parse(row.Cell(9).Value.ToString(), NumberStyles.Any, CultureInfo.InvariantCulture);
                        //            }
                        //        }
                        //        _team_center_NX.OverallDimensions = row.Cell(10).Value.ToString();
                        //        if (String.IsNullOrEmpty(row.Cell(11).Value.ToString()) || String.IsNullOrWhiteSpace(row.Cell(11).Value.ToString()))
                        //        {
                        //            _team_center_NX.Balance = null;
                        //        }
                        //        else
                        //        {
                        //            try
                        //            {
                        //                _team_center_NX.Balance = decimal.Parse(row.Cell(11).Value.ToString());
                        //            }
                        //            catch (Exception)
                        //            {
                        //                _team_center_NX.Balance = decimal.Parse(row.Cell(11).Value.ToString(), NumberStyles.Any, CultureInfo.InvariantCulture);
                        //            }
                        //        }
                        //        _team_center_NX.WeightCondition = row.Cell(12).Value.ToString();
                        //        _team_center_NX.Material = row.Cell(13).Value.ToString();
                        //        if (string.IsNullOrEmpty(row.Cell(14).Value.ToString()) || string.IsNullOrWhiteSpace(row.Cell(14).Value.ToString()))
                        //        {
                        //            _team_center_NX.Lenght = null;
                        //        }
                        //        else
                        //        {
                        //            try
                        //            {
                        //                _team_center_NX.Lenght = decimal.Parse(row.Cell(14).Value.ToString());
                        //            }
                        //            catch (Exception)
                        //            {
                        //                _team_center_NX.Lenght = decimal.Parse(row.Cell(14).Value.ToString(), NumberStyles.Any, CultureInfo.InvariantCulture);
                        //            }
                        //        }
                        //        if (String.IsNullOrEmpty(row.Cell(15).Value.ToString()) || String.IsNullOrWhiteSpace(row.Cell(15).Value.ToString()))
                        //        {
                        //            _team_center_NX.Width = null;
                        //        }
                        //        else
                        //        {
                        //            try
                        //            {
                        //                _team_center_NX.Width = decimal.Parse(row.Cell(15).Value.ToString());
                        //            }
                        //            catch (Exception)
                        //            {
                        //                _team_center_NX.Width = decimal.Parse(row.Cell(15).Value.ToString(), NumberStyles.Any, CultureInfo.InvariantCulture);
                        //            }
                        //        }
                        //        if (string.IsNullOrEmpty(row.Cell(16).Value.ToString()) || String.IsNullOrWhiteSpace(row.Cell(16).Value.ToString()))
                        //        {
                        //            _team_center_NX.Height = null;
                        //        }
                        //        else
                        //        {
                        //            try
                        //            {
                        //                _team_center_NX.Height = decimal.Parse(row.Cell(16).Value.ToString());
                        //            }
                        //            catch (Exception)
                        //            {
                        //                _team_center_NX.Height = decimal.Parse(row.Cell(16).Value.ToString(), NumberStyles.Any, CultureInfo.InvariantCulture);
                        //            }
                        //        }
                        //        _team_center_NX.Layer = row.Cell(17).Value.ToString();
                        //        _team_center_NX.Projects = row.Cell(18).Value.ToString();
                        //        _team_center_NX.Condition = row.Cell(19).Value.ToString();
                        //        if (String.IsNullOrEmpty(row.Cell(20).Value.ToString()) || String.IsNullOrWhiteSpace(row.Cell(20).Value.ToString()))
                        //        {
                        //            _team_center_NX.Weight_ = null;
                        //        }
                        //        else
                        //        {
                        //            try
                        //            {
                        //                _team_center_NX.Weight_ = decimal.Parse(row.Cell(20).Value.ToString());
                        //            }
                        //            catch (Exception)
                        //            {
                        //                _team_center_NX.Weight_ = decimal.Parse(row.Cell(20).Value.ToString(), NumberStyles.Any, CultureInfo.InvariantCulture);
                        //            }
                        //        }
                        //        _team_center_NX.Classified = row.Cell(21).Value.ToString();
                        //        _team_center_NX.DB_PART_TYPE = row.Cell(22).Value.ToString();
                        //        _team_center_NX.Developed = row.Cell(23).Value.ToString();
                        //        _team_center_NX.NX_Material = row.Cell(24).Value.ToString();
                        //        _team_center_NX.Department = row.Cell(25).Value.ToString();
                        //        _team_center_NX.Mass_Prop_Mass = row.Cell(26).Value.ToString();
                        //        _team_center_NX.Applicability = row.Cell(27).Value.ToString();
                        //        _team_center_NX.operation_date = DateTime.Now;
                        //        _team_center_NX.period_open_date = DateTime.Now;
                        //        _team_center_NX.operation = "INSERT";
                        //        model_Team_Center_NX.Add(_team_center_NX);
                        //        integration_context_team_center.SaveChanges();
                        //        sw.Stop();
                        //        TempData["timer"] = sw.Elapsed;
                        //    }
                        //    string connetionString = null;
                        //    SqlConnection connection;
                        //    //SqlParameter cex_bilo;
                        //    connetionString = "data source = i7-860; initial catalog = INTEGRATION; user id = k6; password = jnltk35";
                        //    connection = new SqlConnection(connetionString);
                        //    try
                        //    {
                        //        connection.Open();
                        //        SqlCommand cmd = new SqlCommand("Load_NX", connection);
                        //        cmd.CommandType = CommandType.StoredProcedure;

                        //        //cex_bilo = new SqlParameter("@cex_bilo", bilo);
                        //        //cex_bilo.Direction = ParameterDirection.Input;
                        //        //cmd.Parameters.Add(cex_bilo);

                        //        cmd.ExecuteNonQuery();
                        //        connection.Close();
                        //    }
                        //    catch (Exception)
                        //    {
                        //        TempData["msg"] = "<script>alert('Нет подключения к серверу Баз Данных!!!!!');</script>";
                        //    }
                        //    break;

                        case "SPR_PRTO":
                            Spr_prpokr spr_Prpokr = new Spr_prpokr();
                            foreach (var row in rows)
                            {
                                spr_Prpokr.Prpokr = row.Cell(2).Value.ToString();
                                spr_Prpokr.NmPrpokr = row.Cell(3).Value.ToString();
                                spr_Prpokr.Spr_pviId = Valid_Cell(row.Cell(4).Value.ToString()) == null ? (int)PviLevel.Insert : int.Parse(Valid_Cell(row.Cell(4).Value.ToString()));
                                spr_Prpokr.DocumentStatusId = Valid_Cell(row.Cell(5).Value.ToString()) == null ? 1 : int.Parse(Valid_Cell(row.Cell(5).Value.ToString()));
                                spr_Prpokr.OperationDate = DateTime.Now;
                                spr_Prpokr.PeriodOpenDate = DateTime.Now;
                                _prpokrRepository.Insert(spr_Prpokr);
                                sw.Stop();
                                TempData["timer"] = sw.Elapsed;
                            }
                            break;

                        case "SPR_TTO":
                            Spr_tto tto = new Spr_tto();
                            foreach (var row in rows)
                            {
                                tto.KodTTOId = Valid_Cell(row.Cell(2).Value.ToString()) == null ? 1 : int.Parse(Valid_Cell(row.Cell(2).Value.ToString()));
                                tto.KodKompId = Valid_Cell(row.Cell(3).Value.ToString()) == null ? 1 : int.Parse(Valid_Cell(row.Cell(3).Value.ToString()));
                                tto.CizgId = Valid_Cell(row.Cell(4).Value.ToString()) == null ? 1 : int.Parse(Valid_Cell(row.Cell(4).Value.ToString()));
                                tto.PrpokrId = Valid_Cell(row.Cell(5).Value.ToString()) == null ? 1 : int.Parse(Valid_Cell(row.Cell(5).Value.ToString()));
                                tto.PrkmId = Valid_Cell(row.Cell(6).Value.ToString()) == null ? 1 : int.Parse(Valid_Cell(row.Cell(6).Value.ToString()));
                                if (String.IsNullOrEmpty(row.Cell(7).Value.ToString()) || String.IsNullOrWhiteSpace(row.Cell(7).Value.ToString()))
                                {
                                    tto.Nrm = null;
                                }
                                else
                                {
                                    try
                                    {
                                        tto.Nrm = decimal.Parse(row.Cell(7).Value.ToString());
                                        //tto.nrm = decimal.Parse(row.Cell(6).Value.ToString(), NumberStyles.AllowDecimalPoint, CultureInfo.CurrentCulture);
                                    }
                                    catch (Exception)
                                    {
                                        tto.Nrm = decimal.Parse(row.Cell(7).Value.ToString(), NumberStyles.Any, CultureInfo.InvariantCulture);
                                    }
                                }
                                tto.Vpost = Valid_Cell(row.Cell(8).Value.ToString()) == null ? 0 : int.Parse(Valid_Cell(row.Cell(8).Value.ToString()));
                                if (String.IsNullOrEmpty(row.Cell(9).Value.ToString()) || String.IsNullOrWhiteSpace(row.Cell(9).Value.ToString()))
                                {
                                    tto.Nrvp = null;
                                }
                                else
                                {
                                    try
                                    {
                                        tto.Nrvp = decimal.Parse(row.Cell(9).Value.ToString());
                                        //tto.nrvp = decimal.Parse(row.Cell(8).Value.ToString(), NumberStyles.Any, CultureInfo.InvariantCulture);
                                    }
                                    catch (Exception)
                                    {
                                        tto.Nrvp = decimal.Parse(row.Cell(9).Value.ToString(), NumberStyles.Any, CultureInfo.InvariantCulture);
                                    }
                                }
                                tto.Krat = Valid_Cell(row.Cell(10).Value.ToString()) == null ? 0 : int.Parse(Valid_Cell(row.Cell(10).Value.ToString()));
                                tto.VpostSh = Valid_Cell(row.Cell(11).Value.ToString()) == null ? 0 : int.Parse(Valid_Cell(row.Cell(11).Value.ToString()));
                                tto.SortKodTTO = Valid_Cell(row.Cell(12).Value.ToString()) == null ? 0 : int.Parse(Valid_Cell(row.Cell(12).Value.ToString()));
                                tto.SortKodKomp = Valid_Cell(row.Cell(13).Value.ToString()) == null ? 0 : int.Parse(Valid_Cell(row.Cell(13).Value.ToString()));
                                tto.Spr_pviId = Valid_Cell(row.Cell(14).Value.ToString()) == null ? (int)PviLevel.Insert : int.Parse(Valid_Cell(row.Cell(14).Value.ToString()));
                                tto.DocumentStatusId = Valid_Cell(row.Cell(15).Value.ToString()) == null ? 1 : int.Parse(Valid_Cell(row.Cell(15).Value.ToString()));
                                tto.OperationDate = DateTime.Now;
                                tto.PeriodOpenDate = DateTime.Now;
                                _ttoRepository.Insert(tto);
                                sw.Stop();
                                TempData["timer"] = sw.Elapsed;
                            }
                            break;

                        case "S_OBOZN":
                            Spr_obozn obozn = new Spr_obozn();
                            foreach (var row in rows)
                            {
                                obozn.Obozn = row.Cell(2).Value.ToString();
                                obozn.Var = row.Cell(3).Value.ToString();
                                obozn.Obozn_p = row.Cell(4).Value.ToString();
                                obozn.Obozn_dos = row.Cell(5).Value.ToString();
                                obozn.PkpId = Valid_Cell(row.Cell(6).Value.ToString()) == null ? 1 : int.Parse(Valid_Cell(row.Cell(6).Value.ToString()));
                                obozn.NaimId = Valid_Cell(row.Cell(7).Value.ToString()) == null ? 1 : int.Parse(Valid_Cell(row.Cell(7).Value.ToString()));
                                obozn.Stsort_kt = row.Cell(8).Value.ToString();
                                obozn.Stsort_tip = row.Cell(9).Value.ToString();
                                obozn.Stsort_tr_1 = row.Cell(10).Value.ToString();
                                obozn.Stsort_tr_2 = row.Cell(11).Value.ToString();
                                obozn.Stsort_tr_3 = row.Cell(12).Value.ToString();
                                obozn.Stsort_tr_4 = row.Cell(13).Value.ToString();
                                obozn.Stsort_tr_5 = row.Cell(14).Value.ToString();
                                obozn.Stsort_tr_6 = row.Cell(15).Value.ToString();
                                obozn.Stsort_tr_7 = row.Cell(16).Value.ToString();
                                obozn.PviId = Valid_Cell(row.Cell(17).Value.ToString()) == null ? (int)PviLevel.Insert : int.Parse(Valid_Cell(row.Cell(17).Value.ToString()));
                                obozn.DocumentStatusId = Valid_Cell(row.Cell(18).Value.ToString()) == null ? 1 : int.Parse(Valid_Cell(row.Cell(18).Value.ToString()));
                                obozn.OperationDate = DateTime.Now;
                                obozn.PeriodOpenDate = DateTime.Now;
                                _oboznRepository.Insert(obozn);
                                sw.Stop();
                                TempData["timer"] = sw.Elapsed;
                            }
                            break;

                        case "SPR_SKM":
                            SprSkm skm = new SprSkm();
                            foreach (var row in rows)
                            {
                                skm.Km = row.Cell(2).Value.ToString();
                                skm.Dbt = row.Cell(3).Value.ToString();
                                skm.Dsh = row.Cell(4).Value.ToString();
                                try
                                {
                                    skm.Ves = decimal.Parse(row.Cell(5).Value.ToString());
                                }
                                catch (Exception)
                                {
                                    skm.Ves = decimal.Parse(row.Cell(5).Value.ToString(), NumberStyles.Any, CultureInfo.InvariantCulture);
                                }
                                skm.NomenklNomer = row.Cell(6).Value.ToString();
                                skm.OpisanCherteg = row.Cell(7).Value.ToString();
                                skm.DopolnNomProfil = row.Cell(8).Value.ToString();
                                skm.PriznTto = row.Cell(9).Value.ToString();
                                skm.NmSkmId = Valid_Cell(row.Cell(10).Value.ToString()) == null ? 1 : int.Parse(Valid_Cell(row.Cell(10).Value.ToString()));
                                skm.MarkaId = Valid_Cell(row.Cell(11).Value.ToString()) == null ? 1 : int.Parse(Valid_Cell(row.Cell(11).Value.ToString()));
                                skm.GostId = Valid_Cell(row.Cell(12).Value.ToString()) == null ? 1 : int.Parse(Valid_Cell(row.Cell(12).Value.ToString()));
                                skm.KgrId = Valid_Cell(row.Cell(13).Value.ToString()) == null ? 1 : int.Parse(Valid_Cell(row.Cell(13).Value.ToString()));
                                skm.OgtId = Valid_Cell(row.Cell(14).Value.ToString()) == null ? 1 : int.Parse(Valid_Cell(row.Cell(14).Value.ToString()));
                                skm.GRMaterId = Valid_Cell(row.Cell(15).Value.ToString()) == null ? 1 : int.Parse(Valid_Cell(row.Cell(15).Value.ToString()));
                                skm.OtsId = Valid_Cell(row.Cell(16).Value.ToString()) == null ? 1 : int.Parse(Valid_Cell(row.Cell(16).Value.ToString()));
                                skm.PrkmId = Valid_Cell(row.Cell(17).Value.ToString()) == null ? 1 : int.Parse(Valid_Cell(row.Cell(17).Value.ToString()));
                                skm.EizmId = Valid_Cell(row.Cell(18).Value.ToString()) == null ? 1 : int.Parse(Valid_Cell(row.Cell(18).Value.ToString()));
                                skm.BalschId = Valid_Cell(row.Cell(19).Value.ToString()) == null ? 1 : int.Parse(Valid_Cell(row.Cell(19).Value.ToString()));
                                skm.SortOGT = Valid_Cell(row.Cell(20).Value.ToString()) == null ? 1 : int.Parse(Valid_Cell(row.Cell(20).Value.ToString()));
                                skm.Spr_pviId = Valid_Cell(row.Cell(21).Value.ToString()) == null ? (int)PviLevel.Insert : int.Parse(Valid_Cell(row.Cell(21).Value.ToString()));
                                skm.DocumentStatusId = Valid_Cell(row.Cell(22).Value.ToString()) == null ? 1 : int.Parse(Valid_Cell(row.Cell(22).Value.ToString()));
                                skm.OperationDate = DateTime.Now;
                                skm.PeriodOpenDate = DateTime.Now;
                                _skmRepository.Insert(skm);
                                sw.Stop();
                                TempData["timer"] = sw.Elapsed;
                            }
                            break;
                        case "DOK_OBOS":
                            DokumObosnov docum_obosn = new DokumObosnov();
                            foreach (var row in rows)
                            {
                                docum_obosn.Obosnov = row.Cell(2).Value.ToString();
                                docum_obosn.Spr_pviId = Valid_Cell(row.Cell(3).Value.ToString()) == null ? (int)PviLevel.Insert : int.Parse(Valid_Cell(row.Cell(3).Value.ToString()));
                                docum_obosn.StatusDocumentId = Valid_Cell(row.Cell(4).Value.ToString()) == null ? 1 : int.Parse(Valid_Cell(row.Cell(4).Value.ToString()));
                                docum_obosn.OperationDate = DateTime.Now;
                                docum_obosn.PeriodOpenDate = DateTime.Now;
                                _dokumObosnovRepository.Insert(docum_obosn);
                                sw.Stop();
                                TempData["timer"] = sw.Elapsed;
                            }
                            break;
                        case "GOST_MAT":
                            GostMater gostMater = new GostMater();
                            foreach (var row in rows)
                            {
                                gostMater.Gost = row.Cell(2).Value.ToString();
                                gostMater.Spr_pviId = Valid_Cell(row.Cell(3).Value.ToString()) == null ? (int)PviLevel.Insert : int.Parse(Valid_Cell(row.Cell(3).Value.ToString()));
                                gostMater.StatusDocumentId = Valid_Cell(row.Cell(4).Value.ToString()) == null ? 1 : int.Parse(Valid_Cell(row.Cell(4).Value.ToString()));
                                gostMater.OperationDate = DateTime.Now;
                                gostMater.PeriodOpenDate = DateTime.Now;
                                _gostMaterRepository.Insert(gostMater);
                                sw.Stop();
                                TempData["timer"] = sw.Elapsed;
                            }
                            break;
                        case "MARK_M":
                            MarkMater mark_Mater = new MarkMater();
                            foreach (var row in rows)
                            {
                                mark_Mater.MarkaMater = row.Cell(2).Value.ToString();
                                mark_Mater.Spr_pviId = Valid_Cell(row.Cell(3).Value.ToString()) == null ? (int)PviLevel.Insert : int.Parse(Valid_Cell(row.Cell(3).Value.ToString()));
                                mark_Mater.StatusDocumentId = Valid_Cell(row.Cell(4).Value.ToString()) == null ? 1 : int.Parse(Valid_Cell(row.Cell(4).Value.ToString()));
                                mark_Mater.OperationDate = DateTime.Now;
                                mark_Mater.PeriodOpenDate = DateTime.Now;
                                _markMaterRepository.Insert(mark_Mater);
                                sw.Stop();
                                TempData["timer"] = sw.Elapsed;
                            }
                            break;
                        case "NM_MAT":
                            DirectoryOfMaterialName nm_Mater = new DirectoryOfMaterialName();
                            foreach (var row in rows)
                            {
                                nm_Mater.NameMaterial = row.Cell(2).Value.ToString();
                                nm_Mater.Spr_pviId = Valid_Cell(row.Cell(3).Value.ToString()) == null ? (int)PviLevel.Insert : int.Parse(Valid_Cell(row.Cell(3).Value.ToString()));
                                nm_Mater.StatusDocumentId = Valid_Cell(row.Cell(4).Value.ToString()) == null ? 1 : int.Parse(Valid_Cell(row.Cell(4).Value.ToString()));
                                nm_Mater.OperationDate = DateTime.Now;
                                nm_Mater.PeriodOpenDate = DateTime.Now;
                                _materialNameRepository.Insert(nm_Mater);
                                sw.Stop();
                                TempData["timer"] = sw.Elapsed;
                            }
                            break;
                        case "PR_POST":
                            PredprPostav postav = new PredprPostav();
                            foreach (var row in rows)
                            {
                                postav.Predpr = row.Cell(2).Value.ToString();
                                postav.Address = row.Cell(3).Value.ToString();
                                postav.Spr_pviId = Valid_Cell(row.Cell(4).Value.ToString()) == null ? (int)PviLevel.Insert : int.Parse(Valid_Cell(row.Cell(4).Value.ToString()));
                                postav.StatusDocumentId = Valid_Cell(row.Cell(5).Value.ToString()) == null ? 1 : int.Parse(Valid_Cell(row.Cell(5).Value.ToString()));
                                postav.OperationDate = DateTime.Now;
                                postav.PeriodOpenDate = DateTime.Now;
                                _predprPostavRepository.Insert(postav);
                                sw.Stop();
                                TempData["timer"] = sw.Elapsed;
                            }
                            break;
                        case "SORT_MAT":
                            SortMater sort_mat = new SortMater();
                            foreach (var row in rows)
                            {
                                sort_mat.UslRu = row.Cell(2).Value.ToString();
                                sort_mat.SortUsl = row.Cell(3).Value.ToString();
                                sort_mat.Spr_pviId = Valid_Cell(row.Cell(4).Value.ToString()) == null ? (int)PviLevel.Insert : int.Parse(Valid_Cell(row.Cell(4).Value.ToString()));
                                sort_mat.StatusDocumentId = Valid_Cell(row.Cell(5).Value.ToString()) == null ? 1 : int.Parse(Valid_Cell(row.Cell(5).Value.ToString()));
                                sort_mat.OperationDate = DateTime.Now;
                                sort_mat.PeriodOpenDate = DateTime.Now;
                                _sortMaterRepository.Insert(sort_mat);
                                sw.Stop();
                                TempData["timer"] = sw.Elapsed;
                            }
                            break;
                        case "SPR_BSCH":
                            SprBalSch bal_sch = new SprBalSch();
                            foreach (var row in rows)
                            {
                                bal_sch.BalSchet = Valid_Cell(row.Cell(2).Value.ToString()) == null ? 0 : int.Parse(Valid_Cell(row.Cell(2).Value.ToString()));
                                bal_sch.Opis = row.Cell(3).Value.ToString();
                                bal_sch.Spr_pviId = Valid_Cell(row.Cell(4).Value.ToString()) == null ? (int)PviLevel.Insert : int.Parse(Valid_Cell(row.Cell(4).Value.ToString()));
                                bal_sch.StatusDocumentId = Valid_Cell(row.Cell(5).Value.ToString()) == null ? 1 : int.Parse(Valid_Cell(row.Cell(5).Value.ToString()));
                                bal_sch.OperationDate = DateTime.Now;
                                bal_sch.PeriodOpenDate = DateTime.Now;
                                _balSchrRepository.Insert(bal_sch);
                                sw.Stop();
                                TempData["timer"] = sw.Elapsed;
                            }
                            break;
                        case "SPR_CENM":
                            SprCenMater cen_mater = new SprCenMater();
                            foreach (var row in rows)
                            {
                                cen_mater.SkmId = Valid_Cell(row.Cell(2).Value.ToString()) == null ? 1 : int.Parse(Valid_Cell(row.Cell(2).Value.ToString()));
                                if (String.IsNullOrEmpty(row.Cell(3).Value.ToString()) || String.IsNullOrWhiteSpace(row.Cell(3).Value.ToString()))
                                {
                                    cen_mater.Cmat = null;
                                }
                                else
                                {
                                    try
                                    {
                                        cen_mater.Cmat = decimal.Parse(row.Cell(3).Value.ToString());
                                    }
                                    catch (Exception)
                                    {
                                        cen_mater.Cmat = decimal.Parse(row.Cell(3).Value.ToString(), NumberStyles.Any, CultureInfo.InvariantCulture);
                                    }
                                }
                                cen_mater.PredprId = Valid_Cell(row.Cell(4).Value.ToString()) == null ? 1 : int.Parse(Valid_Cell(row.Cell(4).Value.ToString()));
                                cen_mater.ObosnovId = Valid_Cell(row.Cell(5).Value.ToString()) == null ? 1 : int.Parse(Valid_Cell(row.Cell(5).Value.ToString()));
                                cen_mater.GodPrimCen = row.Cell(6).Value.ToString();
                                cen_mater.CurrencyId = Valid_Cell(row.Cell(7).Value.ToString()) == null ? 1 : int.Parse(Valid_Cell(row.Cell(7).Value.ToString()));
                                cen_mater.Spr_pviId = Valid_Cell(row.Cell(8).Value.ToString()) == null ? (int)PviLevel.Insert : int.Parse(Valid_Cell(row.Cell(8).Value.ToString()));
                                cen_mater.StatusDocumentId = Valid_Cell(row.Cell(9).Value.ToString()) == null ? 1 : int.Parse(Valid_Cell(row.Cell(9).Value.ToString()));
                                cen_mater.OperationDate = DateTime.Now;
                                cen_mater.PeriodOpenDate = DateTime.Now;
                                _cenMaterRepository.Insert(cen_mater);
                                sw.Stop();
                                TempData["timer"] = sw.Elapsed;
                            }
                            break;
                        case "SPR_EIZM":
                            SprEizm eizm = new SprEizm();
                            foreach (var row in rows)
                            {
                                eizm.KratNaimEizm = row.Cell(2).Value.ToString();
                                eizm.PolnNaimEizm = row.Cell(3).Value.ToString();
                                eizm.Spr_pviId = Valid_Cell(row.Cell(4).Value.ToString()) == null ? (int)PviLevel.Insert : int.Parse(Valid_Cell(row.Cell(4).Value.ToString()));
                                eizm.StatusDocumentId = Valid_Cell(row.Cell(5).Value.ToString()) == null ? 1 : int.Parse(Valid_Cell(row.Cell(5).Value.ToString()));
                                eizm.OperationDate = DateTime.Now;
                                eizm.PeriodOpenDate = DateTime.Now;
                                _eizmRepository.Insert(eizm);
                                sw.Stop();
                                TempData["timer"] = sw.Elapsed;
                            }
                            break;
                        case "SPR_GRM":
                            SprGrMater gr_mater = new SprGrMater();
                            foreach (var row in rows)
                            {
                                gr_mater.NomerGrMater = Valid_Cell(row.Cell(2).Value.ToString()) == null ? 0 : int.Parse(Valid_Cell(row.Cell(2).Value.ToString()));
                                gr_mater.NmGrMater = row.Cell(3).Value.ToString();
                                gr_mater.Spr_pviId = Valid_Cell(row.Cell(4).Value.ToString()) == null ? (int)PviLevel.Insert : int.Parse(Valid_Cell(row.Cell(4).Value.ToString()));
                                gr_mater.StatusDocumentId = Valid_Cell(row.Cell(5).Value.ToString()) == null ? 1 : int.Parse(Valid_Cell(row.Cell(5).Value.ToString()));
                                gr_mater.OperationDate = DateTime.Now;
                                gr_mater.PeriodOpenDate = DateTime.Now;
                                _grMaterRepository.Insert(gr_mater);
                                sw.Stop();
                                TempData["timer"] = sw.Elapsed;
                            }
                            break;
                        case "SPR_KGR":
                            SprKgr spr_kgr = new SprKgr();
                            foreach (var row in rows)
                            {
                                spr_kgr.Kgr = Valid_Cell(row.Cell(2).Value.ToString()) == null ? 0 : int.Parse(Valid_Cell(row.Cell(2).Value.ToString()));
                                spr_kgr.Fio = row.Cell(3).Value.ToString();
                                spr_kgr.Spr_pviId = Valid_Cell(row.Cell(4).Value.ToString()) == null ? (int)PviLevel.Insert : int.Parse(Valid_Cell(row.Cell(4).Value.ToString()));
                                spr_kgr.StatusDocumentId = Valid_Cell(row.Cell(5).Value.ToString()) == null ? 1 : int.Parse(Valid_Cell(row.Cell(5).Value.ToString()));
                                spr_kgr.OperationDate = DateTime.Now;
                                spr_kgr.PeriodOpenDate = DateTime.Now;
                                _kgrMaterRepository.Insert(spr_kgr);
                                sw.Stop();
                                TempData["timer"] = sw.Elapsed;
                            }
                            break;
                        case "SPR_OGT":
                            SprOgt spr_ogt = new SprOgt();
                            foreach (var row in rows)
                            {
                                spr_ogt.OGT = Valid_Cell(row.Cell(2).Value.ToString()) == null ? 0 : int.Parse(Valid_Cell(row.Cell(2).Value.ToString()));
                                spr_ogt.NaimOgt = row.Cell(3).Value.ToString();
                                spr_ogt.GrMaterId = Valid_Cell(row.Cell(4).Value.ToString()) == null ? 1 : int.Parse(Valid_Cell(row.Cell(4).Value.ToString()));
                                spr_ogt.PrkmId = Valid_Cell(row.Cell(5).Value.ToString()) == null ? 1 : int.Parse(Valid_Cell(row.Cell(5).Value.ToString()));
                                spr_ogt.SortMaterId = Valid_Cell(row.Cell(6).Value.ToString()) == null ? 1 : int.Parse(Valid_Cell(row.Cell(6).Value.ToString()));
                                spr_ogt.KsimKm = Valid_Cell(row.Cell(7).Value.ToString()) == null ? 0 : int.Parse(Valid_Cell(row.Cell(7).Value.ToString()));
                                spr_ogt.Spr_pviId = Valid_Cell(row.Cell(8).Value.ToString()) == null ? (int)PviLevel.Insert : int.Parse(Valid_Cell(row.Cell(8).Value.ToString()));
                                spr_ogt.StatusDocumentId = Valid_Cell(row.Cell(9).Value.ToString()) == null ? 1 : int.Parse(Valid_Cell(row.Cell(9).Value.ToString()));
                                spr_ogt.OperationDate = DateTime.Now;
                                spr_ogt.PeriodOpenDate = DateTime.Now;
                                _ogtMaterRepository.Insert(spr_ogt);
                                sw.Stop();
                                TempData["timer"] = sw.Elapsed;
                            }
                            break;
                        case "SPR_OTS":
                            SprOts spr_ots = new SprOts();
                            foreach (var row in rows)
                            {
                                spr_ots.KodSklad = Valid_Cell(row.Cell(2).Value.ToString()) == null ? 0 : int.Parse(Valid_Cell(row.Cell(2).Value.ToString()));
                                spr_ots.Per = row.Cell(3).Value.ToString();
                                spr_ots.Ots = Valid_Cell(row.Cell(4).Value.ToString()) == null ? 0 : int.Parse(Valid_Cell(row.Cell(4).Value.ToString()));
                                spr_ots.Nomer_Sklad = row.Cell(5).Value.ToString();
                                spr_ots.PviId = Valid_Cell(row.Cell(6).Value.ToString()) == null ? (int)PviLevel.Insert : int.Parse(Valid_Cell(row.Cell(6).Value.ToString()));
                                spr_ots.StatusDocumentId = Valid_Cell(row.Cell(7).Value.ToString()) == null ? 1 : int.Parse(Valid_Cell(row.Cell(7).Value.ToString()));
                                spr_ots.OperationDate = DateTime.Now;
                                spr_ots.PeriodOpenDate = DateTime.Now;
                                _otsMaterRepository.Insert(spr_ots);
                                sw.Stop();
                                TempData["timer"] = sw.Elapsed;
                            }
                            break;
                        case "Spr_pvi":
                            Spr_pvi spr_pvi = new Spr_pvi();
                            foreach (var row in rows)
                            {
                                spr_pvi.Pvi = row.Cell(2).Value.ToString();
                                spr_pvi.NaimPvi = row.Cell(3).Value.ToString();
                                spr_pvi.PviLevelId = Valid_Cell(row.Cell(4).Value.ToString()) == null ? 1 : int.Parse(Valid_Cell(row.Cell(4).Value.ToString()));
                                spr_pvi.StatusDocumentId = Valid_Cell(row.Cell(5).Value.ToString()) == null ? 1 : int.Parse(Valid_Cell(row.Cell(5).Value.ToString()));
                                spr_pvi.OperationDate = DateTime.Now;
                                spr_pvi.PeriodOpenDate = DateTime.Now;
                                _pviRepository.Insert(spr_pvi);
                                sw.Stop();
                                TempData["timer"] = sw.Elapsed;
                            }
                            break;
                        case "SPR_PRKM":
                            SprPrKm s_prkm = new SprPrKm();
                            foreach (var row in rows)
                            {
                                s_prkm.PrKm = row.Cell(2).Value.ToString();
                                s_prkm.NmPrkm = row.Cell(3).Value.ToString();
                                s_prkm.Spr_pviId = Valid_Cell(row.Cell(4).Value.ToString()) == null ? (int)PviLevel.Insert : int.Parse(Valid_Cell(row.Cell(4).Value.ToString()));
                                s_prkm.DocumentStatusId = Valid_Cell(row.Cell(5).Value.ToString()) == null ? 1 : int.Parse(Valid_Cell(row.Cell(5).Value.ToString()));
                                s_prkm.OperationDate = DateTime.Now;
                                s_prkm.PeriodOpenDate = DateTime.Now;
                                _prKmMaterRepository.Insert(s_prkm);
                                sw.Stop();
                                TempData["timer"] = sw.Elapsed;
                            }
                            break;
                        case "spr_SORT":
                            SprSortam s_sortam = new SprSortam();
                            foreach (var row in rows)
                            {
                                s_sortam.Sortament = row.Cell(2).Value.ToString();
                                s_sortam.Spr_pviId = Valid_Cell(row.Cell(3).Value.ToString()) == null ? (int)PviLevel.Insert : int.Parse(Valid_Cell(row.Cell(3).Value.ToString()));
                                s_sortam.StatusDocumentId = Valid_Cell(row.Cell(4).Value.ToString()) == null ? 1 : int.Parse(Valid_Cell(row.Cell(4).Value.ToString()));
                                s_sortam.OperationDate = DateTime.Now;
                                s_sortam.PeriodOpenDate = DateTime.Now;
                                _sortamRepository.Insert(s_sortam);
                                sw.Stop();
                                TempData["timer"] = sw.Elapsed;
                            }
                            break;
                        case "S_PRIM":
                            Spr_prim_dse prim = new Spr_prim_dse();
                            foreach (var row in rows)
                            {
                                prim.PolnRascexId = Valid_Cell(row.Cell(2).Value.ToString()) == null ? 1 : int.Parse(Valid_Cell(row.Cell(2).Value.ToString()));
                                prim.IzdId = Valid_Cell(row.Cell(3).Value.ToString()) == null ? 1 : int.Parse(Valid_Cell(row.Cell(3).Value.ToString()));
                                prim.GrRazdIzdId = Valid_Cell(row.Cell(4).Value.ToString()) == null ? 1 : int.Parse(Valid_Cell(row.Cell(4).Value.ToString()));
                                prim.GrPrimId = Valid_Cell(row.Cell(5).Value.ToString()) == null ? 1 : int.Parse(Valid_Cell(row.Cell(5).Value.ToString()));
                                prim.SpecifId = Valid_Cell(row.Cell(6).Value.ToString()) == null ? 1 : int.Parse(Valid_Cell(row.Cell(6).Value.ToString()));
                                prim.Ss = row.Cell(7).Value.ToString();
                                prim.Spo = row.Cell(8).Value.ToString();
                                prim.n_list = row.Cell(9).Value.ToString();
                                prim.n_poz = row.Cell(10).Value.ToString();
                                prim.Kizd = Valid_Cell(row.Cell(11).Value.ToString()) == null ? 0 : int.Parse(Valid_Cell(row.Cell(11).Value.ToString()));
                                prim.Kp1 = Valid_Cell(row.Cell(12).Value.ToString()) == null ? 0 : int.Parse(Valid_Cell(row.Cell(12).Value.ToString()));
                                prim.Kp2 = Valid_Cell(row.Cell(13).Value.ToString()) == null ? 0 : int.Parse(Valid_Cell(row.Cell(13).Value.ToString()));
                                prim.Kp3 = Valid_Cell(row.Cell(14).Value.ToString()) == null ? 0 : int.Parse(Valid_Cell(row.Cell(14).Value.ToString()));
                                prim.Tk1 = row.Cell(15).Value.ToString();
                                prim.Tk2 = row.Cell(16).Value.ToString();
                                prim.Tk3 = row.Cell(17).Value.ToString();
                                if (String.IsNullOrEmpty(row.Cell(18).Value.ToString()) || String.IsNullOrWhiteSpace(row.Cell(18).Value.ToString()))
                                {
                                    prim.Masizd = null;
                                }
                                else
                                {
                                    try
                                    {
                                        prim.Masizd = decimal.Parse(row.Cell(18).Value.ToString());
                                        //tto.nrm = decimal.Parse(row.Cell(6).Value.ToString(), NumberStyles.AllowDecimalPoint, CultureInfo.CurrentCulture);
                                    }
                                    catch (Exception)
                                    {
                                        prim.Masizd = decimal.Parse(row.Cell(18).Value.ToString(), NumberStyles.Any, CultureInfo.InvariantCulture);
                                    }
                                }




                                prim.KtsId = Valid_Cell(row.Cell(19).Value.ToString()) == null ? 1 : int.Parse(Valid_Cell(row.Cell(19).Value.ToString()));
                                prim.OboznId = Valid_Cell(row.Cell(20).Value.ToString()) == null ? 1 : int.Parse(Valid_Cell(row.Cell(20).Value.ToString()));
                                prim.LinkOboznMater = Valid_Cell(row.Cell(21).Value.ToString()) == null ? 1 : int.Parse(Valid_Cell(row.Cell(21).Value.ToString()));
                                prim.PrimPrinadlegn = row.Cell(22).Value.ToString();
                                prim.PrimTehnol = row.Cell(23).Value.ToString();
                                prim.PrimKonstruktor = row.Cell(24).Value.ToString();
                                prim.PrimIzmenChast = row.Cell(25).Value.ToString();
                                prim.PviId = Valid_Cell(row.Cell(26).Value.ToString()) == null ? (int)PviLevel.Insert : int.Parse(Valid_Cell(row.Cell(26).Value.ToString()));
                                prim.DocumentStatusId = Valid_Cell(row.Cell(27).Value.ToString()) == null ? 1 : int.Parse(Valid_Cell(row.Cell(27).Value.ToString()));
                                prim.OperationDate = DateTime.Now;
                                prim.PeriodOpenDate = DateTime.Now;
                                _primDseRepository.Insert(prim);
                                sw.Stop();
                                TempData["timer"] = sw.Elapsed;
                            }
                            break;
                        case "S_SPECIF":
                            Spr_specif spr_specif = new Spr_specif();
                            foreach (var row in rows)
                            {
                                spr_specif.SpecId = Valid_Cell(row.Cell(2).Value.ToString()) == null ? 1 : int.Parse(Valid_Cell(row.Cell(2).Value.ToString()));
                                spr_specif.KtsId = Valid_Cell(row.Cell(3).Value.ToString()) == null ? 1 : int.Parse(Valid_Cell(row.Cell(3).Value.ToString()));
                                spr_specif.OboznId = Valid_Cell(row.Cell(4).Value.ToString()) == null ? 1 : int.Parse(Valid_Cell(row.Cell(4).Value.ToString()));
                                spr_specif.PkpTTVId = Valid_Cell(row.Cell(5).Value.ToString()) == null ? 1 : int.Parse(Valid_Cell(row.Cell(5).Value.ToString()));
                                spr_specif.KdanId = Valid_Cell(row.Cell(6).Value.ToString()) == null ? 1 : int.Parse(Valid_Cell(row.Cell(6).Value.ToString()));
                                spr_specif.RazdDetId = Valid_Cell(row.Cell(7).Value.ToString()) == null ? 1 : int.Parse(Valid_Cell(row.Cell(7).Value.ToString()));
                                spr_specif.Ksb = Valid_Cell(row.Cell(8).Value.ToString()) == null ? 0 : int.Parse(Valid_Cell(row.Cell(8).Value.ToString()));
                                spr_specif.KomplektId = Valid_Cell(row.Cell(9).Value.ToString()) == null ? 1 : int.Parse(Valid_Cell(row.Cell(9).Value.ToString()));
                                spr_specif.SvyzPrimId = Valid_Cell(row.Cell(10).Value.ToString()) == null ? 1 : int.Parse(Valid_Cell(row.Cell(10).Value.ToString()));
                                spr_specif.PviId = Valid_Cell(row.Cell(11).Value.ToString()) == null ? (int)PviLevel.Insert : int.Parse(Valid_Cell(row.Cell(11).Value.ToString()));
                                spr_specif.DocumentStatusId = Valid_Cell(row.Cell(12).Value.ToString()) == null ? 1 : int.Parse(Valid_Cell(row.Cell(12).Value.ToString()));
                                spr_specif.OperationDate = DateTime.Now;
                                spr_specif.PeriodOpenDate = DateTime.Now;
                                _specifRepository.Insert(spr_specif);
                                sw.Stop();
                                TempData["timer"] = sw.Elapsed;
                            }
                            break;
                        case "S_OBOZ_M":
                            Spr_obozn_mater obozn_mater = new Spr_obozn_mater();
                            foreach (var row in rows)
                            {
                                obozn_mater.OboznId = Valid_Cell(row.Cell(2).Value.ToString()) == null ? 1 : int.Parse(Valid_Cell(row.Cell(2).Value.ToString()));
                                if (String.IsNullOrEmpty(row.Cell(3).Value.ToString()) || String.IsNullOrWhiteSpace(row.Cell(3).Value.ToString()))
                                {
                                    obozn_mater.Mas1sh = null;
                                }
                                else
                                {
                                    try
                                    {
                                        obozn_mater.Mas1sh = decimal.Parse(row.Cell(3).Value.ToString());
                                    }
                                    catch (Exception)
                                    {
                                        obozn_mater.Mas1sh = decimal.Parse(row.Cell(3).Value.ToString(), NumberStyles.Any, CultureInfo.InvariantCulture);
                                    }
                                }
                                obozn_mater.MaterId = Valid_Cell(row.Cell(4).Value.ToString()) == null ? 1 : int.Parse(Valid_Cell(row.Cell(4).Value.ToString()));
                                obozn_mater.PviId = Valid_Cell(row.Cell(5).Value.ToString()) == null ? (int)PviLevel.Insert : int.Parse(Valid_Cell(row.Cell(5).Value.ToString()));
                                obozn_mater.DocumentStatusId = Valid_Cell(row.Cell(6).Value.ToString()) == null ? 1 : int.Parse(Valid_Cell(row.Cell(6).Value.ToString()));
                                obozn_mater.OperationDate = DateTime.Now;
                                obozn_mater.PeriodOpenDate = DateTime.Now;
                                _oboznMaterRepository.Insert(obozn_mater);
                                sw.Stop();
                                TempData["timer"] = sw.Elapsed;
                            }
                            break;
                        //case "TEHN_IZG_PROIZV":
                        //    Models.Msi.Spr_tehnol_izgot_proizv teh_izg_proizv = new Models.Msi.Spr_tehnol_izgot_proizv();
                        //    foreach (var row in rows)
                        //    {
                        //        teh_izg_proizv.nm_GR_tematik_p = row.Cell(1).Value.ToString();
                        //        teh_izg_proizv.nm_GR_tematik_k = row.Cell(2).Value.ToString();
                        //        teh_izg_proizv.prim = row.Cell(3).Value.ToString();
                        //        teh_izg_proizv.link_status = int.Parse(row.Cell(4).Value.ToString());
                        //        teh_izg_proizv.link_user = int.Parse(row.Cell(5).Value.ToString());
                        //        teh_izg_proizv.operation = row.Cell(6).Value.ToString();
                        //        teh_izg_proizv.operation_date = DateTime.Now;
                        //        teh_izg_proizv.period_open_date = DateTime.Now;
                        //        tehn_izgotov_proizv.Add(teh_izg_proizv);
                        //        con_str.SaveChanges();
                        //        sw.Stop();
                        //        TempData["timer"] = sw.Elapsed;
                        //    }
                        //    break;
                        case "TEMATIK":
                            Spr_tematik tematik = new Spr_tematik();
                            foreach (var row in rows)
                            {
                                tematik.NmTematik_p = row.Cell(2).Value.ToString();
                                tematik.NmTematik_k = row.Cell(3).Value.ToString();
                                tematik.Prim = row.Cell(4).Value.ToString();
                                tematik.PviId = Valid_Cell(row.Cell(5).Value.ToString()) == null ? (int)PviLevel.Insert : int.Parse(Valid_Cell(row.Cell(5).Value.ToString()));
                                tematik.DocumentStatusId = Valid_Cell(row.Cell(6).Value.ToString()) == null ? 1 : int.Parse(Valid_Cell(row.Cell(6).Value.ToString()));
                                tematik.OperationDate = DateTime.Now;
                                tematik.PeriodOpenDate = DateTime.Now;
                                _tematikRepository.Insert(tematik);
                            }
                            sw.Stop();
                            TempData["timer"] = sw.Elapsed;
                            break;
                        case "SPR_TEM":
                            Spr_tem tem = new Spr_tem();
                            foreach (var row in rows)
                            {
                                tem.Nm_tem_p = row.Cell(2).Value.ToString();
                                tem.Nm_tem_k = row.Cell(3).Value.ToString();
                                tem.Prim = row.Cell(4).Value.ToString();
                                tem.PviId = Valid_Cell(row.Cell(5).Value.ToString()) == null ? (int)PviLevel.Insert : int.Parse(Valid_Cell(row.Cell(5).Value.ToString()));
                                tem.DocumentStatusId = Valid_Cell(row.Cell(6).Value.ToString()) == null ? 1 : int.Parse(Valid_Cell(row.Cell(6).Value.ToString()));
                                tem.OperationDate = DateTime.Now;
                                tem.PeriodOpenDate = DateTime.Now;
                                _temRepository.Insert(tem);
                            }
                            sw.Stop();
                            TempData["timer"] = sw.Elapsed;
                            break;
                        case "SPR_MASH":
                            Spr_mash mash = new Spr_mash();
                            foreach (var row in rows)
                            {
                                mash.NomMash = row.Cell(2).Value.ToString();
                                mash.NaimMash = row.Cell(3).Value.ToString();
                                mash.IzdId = Valid_Cell(row.Cell(4).Value.ToString()) == null ? 1 : int.Parse(Valid_Cell(row.Cell(4).Value.ToString()));
                                mash.Ser_s = row.Cell(5).Value.ToString();
                                mash.Ser_po = row.Cell(6).Value.ToString();
                                mash.Soot_s = row.Cell(7).Value.ToString();
                                mash.Soot_po = row.Cell(8).Value.ToString();
                                mash.ZakazId = Valid_Cell(row.Cell(9).Value.ToString()) == null ? 1 : int.Parse(Valid_Cell(row.Cell(9).Value.ToString()));
                                mash.KolKompl = Valid_Cell(row.Cell(10).Value.ToString()) == null ? 0 : int.Parse(Valid_Cell(row.Cell(10).Value.ToString()));
                                mash.Shag = Valid_Cell(row.Cell(11).Value.ToString()) == null ? 0 : int.Parse(Valid_Cell(row.Cell(11).Value.ToString()));
                                mash.GrKol = Valid_Cell(row.Cell(12).Value.ToString()) == null ? 0 : int.Parse(Valid_Cell(row.Cell(12).Value.ToString()));
                                mash.PrZrasc = Valid_Cell(row.Cell(13).Value.ToString()) == null ? 0 : int.Parse(Valid_Cell(row.Cell(13).Value.ToString()));
                                mash.PrKompl = Valid_Cell(row.Cell(14).Value.ToString()) == null ? 0 : int.Parse(Valid_Cell(row.Cell(14).Value.ToString()));
                                mash.Ud_ss = row.Cell(15).Value.ToString();
                                mash.Ud_spo = row.Cell(16).Value.ToString();
                                mash.Soot_ud_ss = row.Cell(17).Value.ToString();
                                mash.Soot_ud_spo = row.Cell(18).Value.ToString();
                                mash.Kompl1 = row.Cell(19).Value.ToString();
                                mash.Kompl2 = row.Cell(20).Value.ToString();
                                mash.Sort = row.Cell(21).Value.ToString();
                                mash.Rspo = row.Cell(22).Value.ToString();
                                mash.Kmash = Valid_Cell(row.Cell(23).Value.ToString()) == null ? 0 : int.Parse(Valid_Cell(row.Cell(23).Value.ToString()));
                                mash.Kod_o = row.Cell(24).Value.ToString();
                                mash.PviId = Valid_Cell(row.Cell(25).Value.ToString()) == null ? (int)PviLevel.Insert : int.Parse(Valid_Cell(row.Cell(25).Value.ToString()));
                                mash.DocumentStatusId = Valid_Cell(row.Cell(26).Value.ToString()) == null ? 1 : int.Parse(Valid_Cell(row.Cell(26).Value.ToString()));
                                mash.OperationDate = DateTime.Now;
                                mash.PeriodOpenDate = DateTime.Now;
                                _mashRepository.Insert(mash);
                                sw.Stop();
                                TempData["timer"] = sw.Elapsed;
                            }
                            break;
                        case "S_MASHSG":
                            Spr_mash_sg mash_sg = new Spr_mash_sg();
                            foreach (var row in rows)
                            {
                                mash_sg.NomMash = row.Cell(2).Value.ToString();
                                mash_sg.NaimMash = row.Cell(3).Value.ToString();
                                mash_sg.IzdId = Valid_Cell(row.Cell(4).Value.ToString()) == null ? 1 : int.Parse(Valid_Cell(row.Cell(4).Value.ToString()));
                                mash_sg.Ser_s = row.Cell(5).Value.ToString();
                                mash_sg.Ser_po = row.Cell(6).Value.ToString();
                                mash_sg.Soot_s = row.Cell(7).Value.ToString();
                                mash_sg.Soot_po = row.Cell(8).Value.ToString();
                                mash_sg.IzdId = Valid_Cell(row.Cell(9).Value.ToString()) == null ? 1 : int.Parse(Valid_Cell(row.Cell(9).Value.ToString()));
                                mash_sg.KolKompl = Valid_Cell(row.Cell(10).Value.ToString()) == null ? 0 : int.Parse(Valid_Cell(row.Cell(10).Value.ToString()));
                                mash_sg.Shag = Valid_Cell(row.Cell(11).Value.ToString()) == null ? 0 : int.Parse(Valid_Cell(row.Cell(11).Value.ToString()));
                                mash_sg.GrKol = Valid_Cell(row.Cell(12).Value.ToString()) == null ? 0 : int.Parse(Valid_Cell(row.Cell(12).Value.ToString()));
                                mash_sg.PrZrasc = Valid_Cell(row.Cell(13).Value.ToString()) == null ? 0 : int.Parse(Valid_Cell(row.Cell(13).Value.ToString()));
                                mash_sg.PrKompl = Valid_Cell(row.Cell(14).Value.ToString()) == null ? 0 : int.Parse(Valid_Cell(row.Cell(14).Value.ToString()));
                                mash_sg.Ud_ss = row.Cell(15).Value.ToString();
                                mash_sg.Ud_spo = row.Cell(16).Value.ToString();
                                mash_sg.Soot_ud_ss = row.Cell(17).Value.ToString();
                                mash_sg.Soot_ud_spo = row.Cell(18).Value.ToString();
                                mash_sg.Kompl1 = row.Cell(19).Value.ToString();
                                mash_sg.Kompl2 = row.Cell(20).Value.ToString();
                                mash_sg.Sort = row.Cell(21).Value.ToString();
                                mash_sg.Rspo = row.Cell(22).Value.ToString();
                                mash_sg.Kmash = Valid_Cell(row.Cell(23).Value.ToString()) == null ? 0 : int.Parse(Valid_Cell(row.Cell(23).Value.ToString()));
                                mash_sg.Kod_o = row.Cell(24).Value.ToString();
                                mash_sg.PviId = Valid_Cell(row.Cell(25).Value.ToString()) == null ? (int)PviLevel.Insert : int.Parse(Valid_Cell(row.Cell(25).Value.ToString()));
                                mash_sg.DocumentStatusId = Valid_Cell(row.Cell(26).Value.ToString()) == null ? 1 : int.Parse(Valid_Cell(row.Cell(26).Value.ToString()));
                                mash_sg.OperationDate = DateTime.Now;
                                mash_sg.PeriodOpenDate = DateTime.Now;
                                _mashsgRepository.Insert(mash_sg);

                                sw.Stop();
                                TempData["timer"] = sw.Elapsed;
                            }
                            break;
                        case "S_MATER":
                            Spr_mater s_mater = new Spr_mater();
                            foreach (var row in rows)
                            {
                                s_mater.NmMater = row.Cell(2).Value.ToString();
                                s_mater.PviId = Valid_Cell(row.Cell(3).Value.ToString()) == null ? (int)PviLevel.Insert : int.Parse(Valid_Cell(row.Cell(3).Value.ToString()));
                                s_mater.DocumentStatusId = Valid_Cell(row.Cell(4).Value.ToString()) == null ? 1 : int.Parse(Valid_Cell(row.Cell(4).Value.ToString()));
                                s_mater.OperationDate = DateTime.Now;
                                s_mater.PeriodOpenDate = DateTime.Now;
                                _materRepository.Insert(s_mater);
                                sw.Stop();
                                TempData["timer"] = sw.Elapsed;
                            }
                            break;
                        case "S_NMDET":
                            Spr_nmdet nmdet = new Spr_nmdet();
                            foreach (var row in rows)
                            {
                                nmdet.Naim_det = row.Cell(2).Value.ToString();
                                nmdet.PviId = Valid_Cell(row.Cell(3).Value.ToString()) == null ? (int)PviLevel.Insert : int.Parse(Valid_Cell(row.Cell(3).Value.ToString()));
                                nmdet.DocumentStatusId = Valid_Cell(row.Cell(4).Value.ToString()) == null ? 1 : int.Parse(Valid_Cell(row.Cell(4).Value.ToString()));
                                nmdet.OperationDate = DateTime.Now;
                                nmdet.PeriodOpenDate = DateTime.Now;
                                _nmDetRepository.Insert(nmdet);
                                sw.Stop();
                                TempData["timer"] = sw.Elapsed;
                            }
                            break;
                        case "S_ZAKAZ":
                            Spr_Zakaz zakaz = new Spr_Zakaz();
                            foreach (var row in rows)
                            {
                                zakaz.Zakaz = row.Cell(2).Value.ToString();
                                zakaz.NmZakaz = row.Cell(3).Value.ToString();

                                if (String.IsNullOrEmpty(row.Cell(4).Value.ToString()) || String.IsNullOrWhiteSpace(row.Cell(4).Value.ToString()))
                                {
                                    zakaz.ZakazOpenDate = null;
                                }
                                else
                                    zakaz.ZakazOpenDate = DateTime.Parse(row.Cell(4).Value.ToString());

                                if (String.IsNullOrEmpty(row.Cell(5).Value.ToString()) || String.IsNullOrWhiteSpace(row.Cell(5).Value.ToString()))
                                {
                                    zakaz.ZakazCloseDate = null;
                                }
                                else
                                    zakaz.ZakazCloseDate = DateTime.Parse(row.Cell(5).Value.ToString());
                                zakaz.Osnovanie = row.Cell(6).Value.ToString();
                                zakaz.PviId = Valid_Cell(row.Cell(7).Value.ToString()) == null ? (int)PviLevel.Insert : int.Parse(Valid_Cell(row.Cell(7).Value.ToString()));
                                zakaz.DocumentStatusId = Valid_Cell(row.Cell(8).Value.ToString()) == null ? 1 : int.Parse(Valid_Cell(row.Cell(8).Value.ToString()));
                                zakaz.OperationDate = DateTime.Now;
                                zakaz.PeriodOpenDate = DateTime.Now;
                                _zakazRepository.Insert(zakaz);
                                sw.Stop();
                                TempData["timer"] = sw.Elapsed;
                            }
                            break;
                        case "S_RASCV":
                            Spr_rasc_vert vertical = new Spr_rasc_vert();
                            foreach (var row in rows)
                            {
                                if (String.IsNullOrEmpty(row.Cell(2).Value.ToString()) || String.IsNullOrWhiteSpace(row.Cell(2).Value.ToString()))
                                {
                                    vertical.Npp = null;
                                }
                                else
                                {
                                    try
                                    {
                                        vertical.Npp = decimal.Parse(row.Cell(2).Value.ToString());
                                        //vertical.npp = decimal.Parse(row.Cell(1).Value.ToString(), NumberStyles.Any, CultureInfo.InvariantCulture);
                                    }
                                    catch (Exception)
                                    {
                                        vertical.Npp = decimal.Parse(row.Cell(2).Value.ToString(), NumberStyles.Any, CultureInfo.InvariantCulture);
                                    }
                                }
                                vertical.RascexPoln = Valid_Cell(row.Cell(3).Value.ToString()) == null ? 1 : int.Parse(Valid_Cell(row.Cell(3).Value.ToString()));
                                vertical.CexId = Valid_Cell(row.Cell(4).Value.ToString()) == null ? 1 : int.Parse(Valid_Cell(row.Cell(4).Value.ToString()));
                                vertical.CexPriznId = Valid_Cell(row.Cell(5).Value.ToString()) == null ? 1 : int.Parse(Valid_Cell(row.Cell(5).Value.ToString()));
                                vertical.PrCexOsn = row.Cell(6).Value.ToString() == "0" ? false : true;
                                vertical.CexPotrId = Valid_Cell(row.Cell(7).Value.ToString()) == null ? 1 : int.Parse(Valid_Cell(row.Cell(7).Value.ToString()));
                                vertical.PviId = Valid_Cell(row.Cell(8).Value.ToString()) == null ? (int)PviLevel.Insert : int.Parse(Valid_Cell(row.Cell(8).Value.ToString()));
                                vertical.DocumentStatusId = Valid_Cell(row.Cell(9).Value.ToString()) == null ? 1 : int.Parse(Valid_Cell(row.Cell(9).Value.ToString()));
                                vertical.OperationDate = DateTime.Now;
                                vertical.PeriodOpenDate = DateTime.Now;
                                _rascVertRascexRepository.Insert(vertical);
                                sw.Stop();
                                TempData["timer"] = sw.Elapsed;
                            }
                            break;
                        case "Spr_cex_prizn":
                            Models.Msi.Spr_cex_prizn prizn_cex = new Models.Msi.Spr_cex_prizn();
                            foreach (var row in rows)
                            {
                                prizn_cex.nm_prcex_k = row.Cell(1).Value.ToString();
                                prizn_cex.nm_prcex_p = row.Cell(2).Value.ToString();
                                prizn_cex.nm_grup = row.Cell(3).Value.ToString();
                                prizn_cex.link_status = int.Parse(row.Cell(4).Value.ToString());

                                prizn_cex.operation = row.Cell(6).Value.ToString();
                                prizn_cex.operation_date = DateTime.Now;
                                prizn_cex.period_open_date = DateTime.Now;

                                sw.Stop();
                                TempData["timer"] = sw.Elapsed;
                            }
                            break;
                        case "TEHN_IZG":
                            Spr_Tehnizg tehn_izg = new Spr_Tehnizg();
                            foreach (var row in rows)
                            {
                                tehn_izg.Tehn_izg_p = row.Cell(2).Value.ToString();
                                tehn_izg.Tehn_izg_k = row.Cell(3).Value.ToString();
                                tehn_izg.Prim = row.Cell(4).Value.ToString();
                                tehn_izg.PviId = Valid_Cell(row.Cell(5).Value.ToString()) == null ? (int)PviLevel.Insert : int.Parse(Valid_Cell(row.Cell(5).Value.ToString()));
                                tehn_izg.DocumentStatusId = Valid_Cell(row.Cell(6).Value.ToString()) == null ? 1 : int.Parse(Valid_Cell(row.Cell(6).Value.ToString()));
                                tehn_izg.PeriodOpenDate = DateTime.Now;
                                _tehnizgRepository.Insert(tehn_izg);
                                sw.Stop();
                                TempData["timer"] = sw.Elapsed;
                            }
                            break;
                        case "S_RASC_P":
                            Models.Msi.Spr_rascex_poln poln_rascex = new Models.Msi.Spr_rascex_poln();
                            foreach (var row in rows)
                            {
                                poln_rascex.link_rascex = int.Parse(row.Cell(1).Value.ToString());
                                if (poln_rascex.link_rascex == 0 || String.IsNullOrEmpty(row.Cell(1).Value.ToString()) || String.IsNullOrWhiteSpace(row.Cell(1).Value.ToString()))
                                {
                                    poln_rascex.link_rascex = null;
                                }
                                else
                                    poln_rascex.link_rascex = int.Parse(row.Cell(1).Value.ToString());

                                poln_rascex.CI11 = int.Parse(row.Cell(2).Value.ToString());
                                if (poln_rascex.CI11 == 0 || String.IsNullOrEmpty(row.Cell(2).Value.ToString()) || String.IsNullOrWhiteSpace(row.Cell(2).Value.ToString()))
                                {
                                    poln_rascex.CI11 = null;
                                }
                                else
                                    poln_rascex.CI11 = int.Parse(row.Cell(2).Value.ToString());

                                poln_rascex.CI12 = int.Parse(row.Cell(3).Value.ToString());
                                if (poln_rascex.CI12 == 0 || String.IsNullOrEmpty(row.Cell(3).Value.ToString()) || String.IsNullOrWhiteSpace(row.Cell(3).Value.ToString()))
                                {
                                    poln_rascex.CI12 = null;
                                }
                                else
                                    poln_rascex.CI12 = int.Parse(row.Cell(3).Value.ToString());

                                poln_rascex.CI13 = int.Parse(row.Cell(4).Value.ToString());
                                if (poln_rascex.CI13 == 0 || String.IsNullOrEmpty(row.Cell(4).Value.ToString()) || String.IsNullOrWhiteSpace(row.Cell(4).Value.ToString()))
                                {
                                    poln_rascex.CI13 = null;
                                }
                                else
                                    poln_rascex.CI13 = int.Parse(row.Cell(4).Value.ToString());

                                poln_rascex.CP1 = int.Parse(row.Cell(5).Value.ToString());
                                if (poln_rascex.CP1 == 0 || String.IsNullOrEmpty(row.Cell(5).Value.ToString()) || String.IsNullOrWhiteSpace(row.Cell(5).Value.ToString()))
                                {
                                    poln_rascex.CP1 = null;
                                }
                                else
                                    poln_rascex.CP1 = int.Parse(row.Cell(5).Value.ToString());

                                poln_rascex.CP2 = int.Parse(row.Cell(6).Value.ToString());
                                if (poln_rascex.CP2 == 0 || String.IsNullOrEmpty(row.Cell(6).Value.ToString()) || String.IsNullOrWhiteSpace(row.Cell(6).Value.ToString()))
                                {
                                    poln_rascex.CP2 = null;
                                }
                                else
                                    poln_rascex.CP2 = int.Parse(row.Cell(6).Value.ToString());

                                poln_rascex.CP3 = int.Parse(row.Cell(7).Value.ToString());
                                if (poln_rascex.CP3 == 0 || String.IsNullOrEmpty(row.Cell(7).Value.ToString()) || String.IsNullOrWhiteSpace(row.Cell(7).Value.ToString()))
                                {
                                    poln_rascex.CP3 = null;
                                }
                                else
                                    poln_rascex.CP3 = int.Parse(row.Cell(7).Value.ToString());

                                poln_rascex.CI2 = int.Parse(row.Cell(8).Value.ToString());
                                if (poln_rascex.CI2 == 0 || String.IsNullOrEmpty(row.Cell(8).Value.ToString()) || String.IsNullOrWhiteSpace(row.Cell(8).Value.ToString()))
                                {
                                    poln_rascex.CI2 = null;
                                }
                                else
                                    poln_rascex.CI2 = int.Parse(row.Cell(8).Value.ToString());

                                poln_rascex.CI3 = int.Parse(row.Cell(9).Value.ToString());
                                if (poln_rascex.CI3 == 0 || String.IsNullOrEmpty(row.Cell(9).Value.ToString()) || String.IsNullOrWhiteSpace(row.Cell(9).Value.ToString()))
                                {
                                    poln_rascex.CI3 = null;
                                }
                                else
                                    poln_rascex.CI3 = int.Parse(row.Cell(9).Value.ToString());

                                poln_rascex.CI4 = int.Parse(row.Cell(10).Value.ToString());
                                if (poln_rascex.CI4 == 0 || String.IsNullOrEmpty(row.Cell(10).Value.ToString()) || String.IsNullOrWhiteSpace(row.Cell(10).Value.ToString()))
                                {
                                    poln_rascex.CI4 = null;
                                }
                                else
                                    poln_rascex.CI4 = int.Parse(row.Cell(10).Value.ToString());

                                poln_rascex.CI5 = int.Parse(row.Cell(11).Value.ToString());
                                if (poln_rascex.CI5 == 0 || String.IsNullOrEmpty(row.Cell(11).Value.ToString()) || String.IsNullOrWhiteSpace(row.Cell(11).Value.ToString()))
                                {
                                    poln_rascex.CI5 = null;
                                }
                                else
                                    poln_rascex.CI5 = int.Parse(row.Cell(11).Value.ToString());

                                poln_rascex.CI6 = int.Parse(row.Cell(12).Value.ToString());
                                if (poln_rascex.CI6 == 0 || String.IsNullOrEmpty(row.Cell(12).Value.ToString()) || String.IsNullOrWhiteSpace(row.Cell(12).Value.ToString()))
                                {
                                    poln_rascex.CI6 = null;
                                }
                                else
                                    poln_rascex.CI6 = int.Parse(row.Cell(12).Value.ToString());

                                poln_rascex.CI7 = int.Parse(row.Cell(13).Value.ToString());
                                if (poln_rascex.CI7 == 0 || String.IsNullOrEmpty(row.Cell(13).Value.ToString()) || String.IsNullOrWhiteSpace(row.Cell(13).Value.ToString()))
                                {
                                    poln_rascex.CI7 = null;
                                }
                                else
                                    poln_rascex.CI7 = int.Parse(row.Cell(13).Value.ToString());

                                poln_rascex.CI8 = int.Parse(row.Cell(14).Value.ToString());
                                if (poln_rascex.CI8 == 0 || String.IsNullOrEmpty(row.Cell(14).Value.ToString()) || String.IsNullOrWhiteSpace(row.Cell(14).Value.ToString()))
                                {
                                    poln_rascex.CI8 = null;
                                }
                                else
                                    poln_rascex.CI8 = int.Parse(row.Cell(14).Value.ToString());

                                poln_rascex.CI9 = int.Parse(row.Cell(15).Value.ToString());
                                if (poln_rascex.CI9 == 0 || String.IsNullOrEmpty(row.Cell(15).Value.ToString()) || String.IsNullOrWhiteSpace(row.Cell(15).Value.ToString()))
                                {
                                    poln_rascex.CI9 = null;
                                }
                                else
                                    poln_rascex.CI9 = int.Parse(row.Cell(15).Value.ToString());

                                poln_rascex.CI10 = int.Parse(row.Cell(16).Value.ToString());
                                if (poln_rascex.CI10 == 0 || poln_rascex.CI10 == null || String.IsNullOrEmpty(row.Cell(16).Value.ToString()) || String.IsNullOrWhiteSpace(row.Cell(16).Value.ToString()))
                                {
                                    poln_rascex.CI10 = null;
                                }
                                else
                                    poln_rascex.CI10 = int.Parse(row.Cell(16).Value.ToString());

                                poln_rascex.CTO = int.Parse(row.Cell(17).Value.ToString());
                                if (poln_rascex.CTO == 0 || String.IsNullOrEmpty(row.Cell(17).Value.ToString()) || String.IsNullOrWhiteSpace(row.Cell(17).Value.ToString()))
                                {
                                    poln_rascex.CTO = null;
                                }
                                else
                                    poln_rascex.CTO = int.Parse(row.Cell(17).Value.ToString());

                                poln_rascex.CPK1 = int.Parse(row.Cell(18).Value.ToString());
                                if (poln_rascex.CPK1 == 0 || String.IsNullOrEmpty(row.Cell(18).Value.ToString()) || String.IsNullOrWhiteSpace(row.Cell(18).Value.ToString()))
                                {
                                    poln_rascex.CPK1 = null;
                                }
                                else
                                    poln_rascex.CPK1 = int.Parse(row.Cell(18).Value.ToString());

                                poln_rascex.CPK2 = int.Parse(row.Cell(19).Value.ToString());
                                if (poln_rascex.CPK2 == 0 || String.IsNullOrEmpty(row.Cell(19).Value.ToString()) || String.IsNullOrWhiteSpace(row.Cell(19).Value.ToString()))
                                {
                                    poln_rascex.CPK2 = null;
                                }
                                else
                                    poln_rascex.CPK2 = int.Parse(row.Cell(19).Value.ToString());

                                poln_rascex.CPK3 = int.Parse(row.Cell(20).Value.ToString());
                                if (poln_rascex.CPK3 == 0 || String.IsNullOrEmpty(row.Cell(20).Value.ToString()) || String.IsNullOrWhiteSpace(row.Cell(20).Value.ToString()))
                                {
                                    poln_rascex.CPK3 = null;
                                }
                                else
                                    poln_rascex.CPK3 = int.Parse(row.Cell(20).Value.ToString());

                                poln_rascex.CPK4 = int.Parse(row.Cell(21).Value.ToString());
                                if (poln_rascex.CPK4 == 0 || String.IsNullOrEmpty(row.Cell(21).Value.ToString()) || String.IsNullOrWhiteSpace(row.Cell(21).Value.ToString()))
                                {
                                    poln_rascex.CPK4 = null;
                                }
                                else
                                    poln_rascex.CPK4 = int.Parse(row.Cell(21).Value.ToString());

                                poln_rascex.CUS1 = int.Parse(row.Cell(22).Value.ToString());
                                if (poln_rascex.CUS1 == 0 || String.IsNullOrEmpty(row.Cell(22).Value.ToString()) || String.IsNullOrWhiteSpace(row.Cell(22).Value.ToString()))
                                {
                                    poln_rascex.CUS1 = null;
                                }
                                else
                                    poln_rascex.CUS1 = int.Parse(row.Cell(22).Value.ToString());

                                poln_rascex.CUS2 = int.Parse(row.Cell(23).Value.ToString());
                                if (poln_rascex.CUS2 == 0 || String.IsNullOrEmpty(row.Cell(23).Value.ToString()) || String.IsNullOrWhiteSpace(row.Cell(23).Value.ToString()))
                                {
                                    poln_rascex.CUS2 = null;
                                }
                                else
                                    poln_rascex.CUS2 = int.Parse(row.Cell(23).Value.ToString());

                                poln_rascex.CUS3 = int.Parse(row.Cell(24).Value.ToString());
                                if (poln_rascex.CUS3 == 0 || String.IsNullOrEmpty(row.Cell(24).Value.ToString()) || String.IsNullOrWhiteSpace(row.Cell(24).Value.ToString()))
                                {
                                    poln_rascex.CUS3 = null;
                                }
                                else
                                    poln_rascex.CUS3 = int.Parse(row.Cell(24).Value.ToString());

                                poln_rascex.CUS4 = int.Parse(row.Cell(25).Value.ToString());
                                if (poln_rascex.CUS4 == 0 || String.IsNullOrEmpty(row.Cell(25).Value.ToString()) || String.IsNullOrWhiteSpace(row.Cell(25).Value.ToString()))
                                {
                                    poln_rascex.CUS4 = null;
                                }
                                else
                                    poln_rascex.CUS4 = int.Parse(row.Cell(25).Value.ToString());

                                poln_rascex.CUS5 = int.Parse(row.Cell(26).Value.ToString());
                                if (poln_rascex.CUS5 == 0 || String.IsNullOrEmpty(row.Cell(26).Value.ToString()) || String.IsNullOrWhiteSpace(row.Cell(26).Value.ToString()))
                                {
                                    poln_rascex.CUS5 = null;
                                }
                                else
                                    poln_rascex.CUS5 = int.Parse(row.Cell(26).Value.ToString());

                                poln_rascex.CUS6 = int.Parse(row.Cell(27).Value.ToString());
                                if (poln_rascex.CUS6 == 0 || String.IsNullOrEmpty(row.Cell(27).Value.ToString()) || String.IsNullOrWhiteSpace(row.Cell(27).Value.ToString()))
                                {
                                    poln_rascex.CUS6 = null;
                                }
                                else
                                    poln_rascex.CUS6 = int.Parse(row.Cell(27).Value.ToString());

                                poln_rascex.CUS7 = int.Parse(row.Cell(28).Value.ToString());
                                if (poln_rascex.CUS7 == 0 || String.IsNullOrEmpty(row.Cell(28).Value.ToString()) || String.IsNullOrWhiteSpace(row.Cell(28).Value.ToString()))
                                {
                                    poln_rascex.CUS7 = null;
                                }
                                else
                                    poln_rascex.CUS7 = int.Parse(row.Cell(28).Value.ToString());

                                poln_rascex.CUS8 = int.Parse(row.Cell(29).Value.ToString());
                                if (poln_rascex.CUS8 == 0 || String.IsNullOrEmpty(row.Cell(29).Value.ToString()) || String.IsNullOrWhiteSpace(row.Cell(29).Value.ToString()))
                                {
                                    poln_rascex.CUS8 = null;
                                }
                                else
                                    poln_rascex.CUS8 = int.Parse(row.Cell(29).Value.ToString());

                                poln_rascex.CUS9 = int.Parse(row.Cell(30).Value.ToString());
                                if (poln_rascex.CUS9 == 0 || String.IsNullOrEmpty(row.Cell(30).Value.ToString()) || String.IsNullOrWhiteSpace(row.Cell(30).Value.ToString()))
                                {
                                    poln_rascex.CUS9 = null;
                                }
                                else
                                    poln_rascex.CUS9 = int.Parse(row.Cell(30).Value.ToString());

                                poln_rascex.CUS10 = int.Parse(row.Cell(31).Value.ToString());
                                if (poln_rascex.CUS10 == 0 || String.IsNullOrEmpty(row.Cell(31).Value.ToString()) || String.IsNullOrWhiteSpace(row.Cell(31).Value.ToString()))
                                {
                                    poln_rascex.CUS10 = null;
                                }
                                else
                                    poln_rascex.CUS10 = int.Parse(row.Cell(31).Value.ToString());
                                poln_rascex.rascex_small = row.Cell(32).Value.ToString();
                                poln_rascex.link_status = Valid_Cell(row.Cell(33).Value.ToString()) == null ? 1 : int.Parse(Valid_Cell(row.Cell(33).Value.ToString()));

                                poln_rascex.operation = row.Cell(35).Value.ToString();
                                poln_rascex.operation_date = DateTime.Now;
                                poln_rascex.period_open_date = DateTime.Now;

                                sw.Stop();
                                TempData["timer"] = sw.Elapsed;
                            }
                            break;
                        case "S_RASCEX":
                            Spr_rascex rascex = new Spr_rascex();
                            foreach (var row in rows)
                            {
                                rascex.CI11 = int.Parse(row.Cell(2).Value.ToString());
                                if (rascex.CI11 == 0 || rascex.CI11 == null)
                                {
                                    rascex.CI11 = null;
                                }
                                else
                                {
                                    rascex.CI11 = int.Parse(row.Cell(2).Value.ToString());
                                }

                                rascex.CI12 = int.Parse(row.Cell(3).Value.ToString());
                                if (rascex.CI12 == 0 || rascex.CI12 == null)
                                {
                                    rascex.CI12 = null;
                                }
                                else
                                {
                                    rascex.CI12 = int.Parse(row.Cell(3).Value.ToString());
                                }

                                rascex.CI13 = int.Parse(row.Cell(4).Value.ToString());
                                if (rascex.CI13 == 0 || rascex.CI13 == null)
                                {
                                    rascex.CI13 = null;
                                }
                                else
                                {
                                    rascex.CI13 = int.Parse(row.Cell(4).Value.ToString());
                                }

                                rascex.CI2 = int.Parse(row.Cell(5).Value.ToString());
                                if ((rascex.CI2 == 0 || rascex.CI2 == null))
                                {
                                    rascex.CI2 = null;
                                }
                                else
                                {
                                    rascex.CI2 = int.Parse(row.Cell(5).Value.ToString());
                                }

                                rascex.CI3 = int.Parse(row.Cell(6).Value.ToString());
                                if (rascex.CI3 == 0 || rascex.CI3 == null)
                                {
                                    rascex.CI3 = null;
                                }
                                else
                                {
                                    rascex.CI3 = int.Parse(row.Cell(6).Value.ToString());
                                }

                                rascex.CI4 = int.Parse(row.Cell(7).Value.ToString());
                                if (rascex.CI4 == 0 || rascex.CI4 == null)
                                {
                                    rascex.CI4 = null;
                                }
                                else
                                {
                                    rascex.CI4 = int.Parse(row.Cell(7).Value.ToString());
                                }
                                rascex.CI5 = int.Parse(row.Cell(8).Value.ToString());
                                if (rascex.CI5 == 0 || rascex.CI5 == null)
                                {
                                    rascex.CI5 = null;
                                }
                                else
                                {
                                    rascex.CI5 = int.Parse(row.Cell(8).Value.ToString());
                                }
                                rascex.CI6 = int.Parse(row.Cell(9).Value.ToString());
                                if (rascex.CI6 == 0 || rascex.CI6 == null)
                                {
                                    rascex.CI6 = null;
                                }
                                else
                                {
                                    rascex.CI6 = int.Parse(row.Cell(9).Value.ToString());
                                }
                                rascex.CI7 = int.Parse(row.Cell(10).Value.ToString());
                                if (rascex.CI7 == 0 || rascex.CI7 == null)
                                {
                                    rascex.CI7 = null;
                                }
                                else
                                {
                                    rascex.CI7 = int.Parse(row.Cell(10).Value.ToString());
                                }
                                rascex.CI8 = int.Parse(row.Cell(11).Value.ToString());
                                if (rascex.CI8 == 0 || rascex.CI8 == null)
                                {
                                    rascex.CI8 = null;
                                }
                                else
                                {
                                    rascex.CI8 = int.Parse(row.Cell(11).Value.ToString());
                                }
                                rascex.CI9 = int.Parse(row.Cell(12).Value.ToString());
                                if (rascex.CI9 == 0 || rascex.CI9 == null)
                                {
                                    rascex.CI9 = null;
                                }
                                else
                                {
                                    rascex.CI9 = int.Parse(row.Cell(12).Value.ToString());
                                }
                                rascex.CI10 = int.Parse(row.Cell(13).Value.ToString());
                                if (rascex.CI10 == 0 || rascex.CI10 == null)
                                {
                                    rascex.CI10 = null;
                                }
                                else
                                {
                                    rascex.CI10 = int.Parse(row.Cell(13).Value.ToString());
                                }
                                rascex.CTO = int.Parse(row.Cell(14).Value.ToString());
                                if (rascex.CTO == 0 || rascex.CTO == null)
                                {
                                    rascex.CTO = null;
                                }
                                else
                                {
                                    rascex.CTO = int.Parse(row.Cell(14).Value.ToString());
                                }
                                rascex.CPK1 = int.Parse(row.Cell(15).Value.ToString());
                                if (rascex.CPK1 == 0 || rascex.CPK1 == null)
                                {
                                    rascex.CPK1 = null;
                                }
                                else
                                {
                                    rascex.CPK1 = int.Parse(row.Cell(15).Value.ToString());
                                }
                                rascex.CPK2 = int.Parse(row.Cell(16).Value.ToString());
                                if (rascex.CPK2 == 0 || rascex.CPK2 == null)
                                {
                                    rascex.CPK2 = null;
                                }
                                else
                                {
                                    rascex.CPK2 = int.Parse(row.Cell(16).Value.ToString());
                                }
                                rascex.CPK3 = int.Parse(row.Cell(17).Value.ToString());
                                if (rascex.CPK3 == 0 || rascex.CPK3 == null)
                                {
                                    rascex.CPK3 = null;
                                }
                                else
                                {
                                    rascex.CPK3 = int.Parse(row.Cell(17).Value.ToString());
                                }
                                rascex.CPK4 = int.Parse(row.Cell(18).Value.ToString());
                                if (rascex.CPK4 == 0 || rascex.CPK4 == null)
                                {
                                    rascex.CPK4 = null;
                                }
                                else
                                {
                                    rascex.CPK4 = int.Parse(row.Cell(18).Value.ToString());
                                }
                                rascex.CUS1 = int.Parse(row.Cell(19).Value.ToString());
                                if (rascex.CUS1 == 0 || rascex.CUS1 == null)
                                {
                                    rascex.CUS1 = null;
                                }
                                else
                                {
                                    rascex.CUS1 = int.Parse(row.Cell(19).Value.ToString());
                                }
                                rascex.CUS2 = int.Parse(row.Cell(20).Value.ToString());
                                if (rascex.CUS2 == 0 || rascex.CUS2 == null)
                                {
                                    rascex.CUS2 = null;
                                }
                                else
                                {
                                    rascex.CUS2 = int.Parse(row.Cell(20).Value.ToString());
                                }
                                rascex.CUS3 = int.Parse(row.Cell(21).Value.ToString());
                                if (rascex.CUS3 == 0 || rascex.CUS3 == null)
                                {
                                    rascex.CUS3 = null;
                                }
                                else
                                {
                                    rascex.CUS3 = int.Parse(row.Cell(21).Value.ToString());
                                }
                                rascex.CUS4 = int.Parse(row.Cell(22).Value.ToString());
                                if (rascex.CUS4 == 0 || rascex.CUS4 == null)
                                {
                                    rascex.CUS4 = null;
                                }
                                else
                                {
                                    rascex.CUS4 = int.Parse(row.Cell(22).Value.ToString());
                                }
                                rascex.CUS5 = int.Parse(row.Cell(23).Value.ToString());
                                if (rascex.CUS5 == 0 || rascex.CUS5 == null)
                                {
                                    rascex.CUS5 = null;
                                }
                                else
                                {
                                    rascex.CUS5 = int.Parse(row.Cell(23).Value.ToString());
                                }
                                rascex.CUS6 = int.Parse(row.Cell(24).Value.ToString());
                                if (rascex.CUS6 == 0 || rascex.CUS6 == null)
                                {
                                    rascex.CUS6 = null;
                                }
                                else
                                {
                                    rascex.CUS6 = int.Parse(row.Cell(24).Value.ToString());
                                }
                                rascex.CUS7 = int.Parse(row.Cell(25).Value.ToString());
                                if (rascex.CUS7 == 0 || rascex.CUS7 == null)
                                {
                                    rascex.CUS7 = null;
                                }
                                else
                                {
                                    rascex.CUS7 = int.Parse(row.Cell(25).Value.ToString());
                                }
                                rascex.CUS8 = int.Parse(row.Cell(26).Value.ToString());
                                if (rascex.CUS8 == 0 || rascex.CUS8 == null)
                                {
                                    rascex.CUS8 = null;
                                }
                                else
                                {
                                    rascex.CUS8 = int.Parse(row.Cell(26).Value.ToString());
                                }
                                rascex.CUS9 = int.Parse(row.Cell(27).Value.ToString());
                                if (rascex.CUS9 == 0 || rascex.CUS9 == null)
                                {
                                    rascex.CUS9 = null;
                                }
                                else
                                {
                                    rascex.CUS9 = int.Parse(row.Cell(27).Value.ToString());
                                }
                                rascex.CUS10 = int.Parse(row.Cell(28).Value.ToString());
                                if (rascex.CUS10 == 0 || rascex.CUS10 == null)
                                {
                                    rascex.CUS10 = null;
                                }
                                else
                                {
                                    rascex.CUS10 = int.Parse(row.Cell(28).Value.ToString());
                                }
                                rascex.RascexSmall = row.Cell(29).Value.ToString();
                                rascex.PviId = Valid_Cell(row.Cell(30).Value.ToString()) == null ? (int)PviLevel.Insert : int.Parse(Valid_Cell(row.Cell(30).Value.ToString()));
                                rascex.DocumentStatusId = Valid_Cell(row.Cell(31).Value.ToString()) == null ? 1 : int.Parse(Valid_Cell(row.Cell(31).Value.ToString()));
                                rascex.OperationDate = DateTime.Now;
                                rascex.PeriodOpenDate = DateTime.Now;
                                _rascexRepository.Insert(rascex);
                                sw.Stop();
                                TempData["timer"] = sw.Elapsed;
                            }
                            break;
                    }
                }
            }
            return Json("Загрузка завершена");
        }
    }
}