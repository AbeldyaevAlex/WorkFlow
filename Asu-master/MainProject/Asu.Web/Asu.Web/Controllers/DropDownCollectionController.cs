using Asu.Core.Domain.StatusDirectory;
using Asu.Mapping.DocumentStatusService;
using Asu.Web.Areas.DirectoryOfMaterialCodifiers.Controllers;
using Asu.Web.Model;
using Asu.Web.Models;
using Asu.Web.Models.ContextDb;
using Asu.Web.Models.UsersTask;
using Asu.Web.ViewModel;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Asu.Web.Controllers
{
    public class DropDownCollectionController 
    {
        private readonly IDocumentStatus _DocumentStatusService;
        public DropDownCollectionController(IDocumentStatus DocumentStatusService)
        {
            _DocumentStatusService = DocumentStatusService;
        }

        public virtual IEnumerable GetStatus()
        {
            return _DocumentStatusService.GetAllStatus();
        }






        public static IEnumerable GetPkp()
        {
            using (var db = new AsuAviaDbContext())
            {
                var x = db.Spr_PKP.ToList();
                return (x);
            }
        }
        //public static IEnumerable<Spr_poln_rascex> GetRascexPoln()
        //{
        //    using (var db = new AsuAviaDbContext())
        //    {
        //        return db.Spr_poln_rascex.ToList();
        //    }

        //}
        public static IEnumerable GetGrPrim()
        {
            using (var db = new AsuAviaDbContext())
            {
                var x = db.spr_grup_prim.ToList();
                return (x);
            }
        }
        public static IEnumerable GetKts()
        {
            using (var db = new AsuAviaDbContext())
            {
                var x = db.Spr_obozn.ToList();
                return (x);
            }
        }

        public static IEnumerable GetPerIzd()
        {
            using (var db = new AsuAviaDbContext())
            {
                var x = db.Spr_Perizd.ToList();
                return (x);
            }
        }
        public static IEnumerable GetPvi()
        {
            using (var db = new AsuAviaDbContext())
            {
                var x = db.Spr_pvi.ToList();
                return (x);
            }
        }
        public static IEnumerable GetNaim()
        {
            using (var db = new AsuAviaDbContext())
            {
                var x = db.Spr_nmdet.ToList();
                return (x);
            }
        }
        public static IEnumerable GetMater()
        {
            using (var db = new AsuAviaDbContext())
            {
                var x = db.Spr_mater.ToList();
                return (x);
            }
        }
        //public static IEnumerable GetStSort()
        //{
        //    using (var db = new ASU_AVIAEntities12())
        //    {
        //        var x = db.St_sort_ob.ToList();
        //        return (x);
        //    }
        //}

        public static IEnumerable GetCex()
        {
            using (var db = new AsuAviaDbContext())
            {
                var x = db.Spr_cex.ToList();
                return (x);
            }
        }
        public static IEnumerable GetSpecif()
        {
            using (var db = new AsuAviaDbContext())
            {
                var x = db.Spr_specif.ToList();
                return (x);
            }
        }
        public static IEnumerable GetIzd()
        {
            using (var db = new AsuAviaDbContext())
            {
                var x = db.Spr_Perizd.ToList();
                return (x);
            }
        }
        public static IEnumerable GetRazdIzd()
        {
            using (var db = new AsuAviaDbContext())
            {
                var x = db.Spr_Razd_Izd.ToList();
                return (x);
            }
        }
        public static IEnumerable GetGrRazdIzd()
        {
            using (var db = new AsuAviaDbContext())
            {
                var x = db.GR_RAZDIZD.ToList();
                return (x);
            }
        }
        public static IEnumerable GetRascex()
        {
            using (var db = new AsuAviaDbContext())
            {
                var x = db.Spr_rascex.ToList();
                return (x);
            }
        }
        public static IEnumerable GetObozn()
        {
            using (var db = new AsuAviaDbContext())
            {
                //var list_obozn = AutoMapper.Mapper.Map<IEnumerable<OboznViewModel>>(db.Spr_obozn);
                var list_obozn = (from ob in db.Spr_obozn
                                  select new
                                  {
                                      Id = ob.Id,
                                      obozn = ob.obozn,
                                      var = ob.var,
                                      obozn_p = ob.obozn_p
                                  });
                return list_obozn.ToList();
            }
        }

        public static IEnumerable GetOboznMater()
        {
            using (var db = new AsuAviaDbContext())
            {
                var list_obozn_mater = (from ob in db.Spr_obozn_mater
                                        select new
                                        {
                                            Id = ob.Id,
                                            //obozn = ob.Spr_obozn.obozn,
                                            //mater = ob.Spr_mater.nm_mater,
                                            mas1sh = ob.mas1sh
                                        });
                return list_obozn_mater.ToList();
            }
        }
        public static IEnumerable GetKdan()
        {
            using (var db = new AsuAviaDbContext())
            {
                var x = db.Spr_kdan.ToList();
                return (x);
            }
        }
        public static IEnumerable GetKompl()
        {
            using (var db = new AsuAviaDbContext())
            {
                var x = db.Spr_kompl.ToList();
                return (x);
            }
        }
        //public Enum GetT_TV()
        //{
        //    return (null);
        //}
        public static IEnumerable GetTask()
        {
            using (var db = new AsuAviaDbContext())
            {
                var x = db.Spr_nm_task.ToList();
                return (x);
            }
        }
        public static IEnumerable GetRazDet()
        {
            using (var db = new AsuAviaDbContext())
            {
                var x = db.Raz_det.OrderBy(r => r.sort).ToList();
                return (x);
            }
        }
        public static IEnumerable GetNmMater()
        {
            using (var db = new AsuAviaDbContext())
            {
                var x = db.Nm_mater.Where(i => i.Id != 1).ToList();
                return (x);
            }
        }
        public static IEnumerable GetMarka()
        {
            using (var db = new AsuAviaDbContext())
            {
                var x = db.Mark_mater.ToList();
                return (x);
            }
        }
        public static IEnumerable GetOgt()
        {
            using (var db = new AsuAviaDbContext())
            {
                var x = db.SPR_OGT.ToList();
                return (x);
            }
        }
        public static IEnumerable GetGost()
        {
            using (var db = new AsuAviaDbContext())
            {
                var x = db.GOST_mater.ToList();
                return (x);
            }
        }
        public static IEnumerable GetEizm()
        {
            using (var db = new AsuAviaDbContext())
            {
                var x = db.Spr_eizm.ToList();
                return (x);
            }
        }
        public static IEnumerable GetKgr()
        {
            using (var db = new AsuAviaDbContext())
            {
                var x = db.Spr_kgr.ToList();
                return (x);
            }
        }
        public static IEnumerable GetOTS()
        {
            using (var db = new AsuAviaDbContext())
            {
                var x = db.SPR_OTS.ToList();
                return (x);
            }
        }
        public static IEnumerable GetSchet()
        {
            using (var db = new AsuAviaDbContext())
            {
                var x = db.Spr_balsch.ToList();
                return (x);
            }
        }
        public static IEnumerable GetPrKm()
        {
            using (var db = new AsuAviaDbContext())
            {
                var x = db.SPR_PRKM.ToList();
                return (x);
            }
        }
        public static IEnumerable GetGr_Mater()
        {
            using (var db = new AsuAviaDbContext())
            {
                var x = db.Spr_GR_Mater.ToList();
                return (x);
            }
        }
        public static IEnumerable GetGrMater()
        {
            using (var db = new AsuAviaDbContext())
            {
                var x = db.Spr_GR_Mater.ToList();
                return (x);
            }
        }
        public static IEnumerable GetKM()
        {
            using (var db = new AsuAviaDbContext())
            {
                var x = db.Spr_skm.ToList();
                return (x);
            }
        }
        public static IEnumerable Get_Pr_Pokr()
        {
            using (var db = new AsuAviaDbContext())
            {
                var x = db.Spr_prpokr.ToList();
                return (x);
            }
        }
        public static IEnumerable Get_km_()
        {
            using (var db = new AsuAviaDbContext())
            {
                var skm_2 = (from skm in db.Spr_skm
                             select new
                             {
                                 skm.Id,
                                 skm.km
                             });
                return skm_2.ToArray();
            }
        }


        public static IEnumerable Get_km_new()
        {
            using (var db = new AsuAviaDbContext())
            {
                var skm_ = (from skm in db.Spr_skm
                            select new
                            {
                                Id = skm.Id,
                                km = skm.km
                            });
                return skm_.ToList();
            }
        }

        public static IEnumerable Get_km_new_kkomp()
        {
            using (var db = new AsuAviaDbContext())
            {
                var skm_ = (from skm in db.Spr_skm
                            select new
                            {
                                Id = skm.Id,
                                km = skm.km
                            });
                return skm_.ToList();
            }
        }



        public static IEnumerable Get_kkomp()
        {
            using (var db1 = new AsuAviaDbContext())
            {
                var kkomp_ = (from kkomp in db1.Spr_tto
                              select new
                              {
                                  kkomp.Id,
                              });
                return kkomp_.ToList();
            }
        }

        public static IEnumerable Get_Docum_Obosnov()
        {
            using (var db = new AsuAviaDbContext())
            {
                var x = db.Dokum_Obosnov.ToList();
                return (x);
            }
        }
        public static IEnumerable Get_Predpr_Postav()
        {
            using (var db = new AsuAviaDbContext())
            {
                var x = db.Predpr_Postav.ToList();
                return (x);
            }
        }
        public static IEnumerable Get_Sort_Ogt()
        {
            using (var db = new AsuAviaDbContext())
            {
                var x = db.Sort_Mater.ToList();
                return (x);
            }
        }
    }

}

