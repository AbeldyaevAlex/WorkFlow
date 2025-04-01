using Asu.Web.Models;
using Asu.Web.ViewModel;
using AutoMapper;
using Asu.Web.Models.ContextDb;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Security.RightsManagement;
using Asu.Core.Domain.DirectoryOfMaterialCodifiers;
using Asu.Core.Domain.StatusDirectory;
using Asu.Core.Domain.Msi;

namespace Asu.Web.AutoMapperConfig
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<SprSkm, SkmViewModel>();
            CreateMap<Spr_pkp, PkpViewModel>();
            CreateMap<MarkMater, MarkMaterViewModel>();
            CreateMap<DirectoryOfMaterialName, DirectoryOfMaterialNameViewModel>();
            CreateMap<Models.Msi.Spr_obozn, OboznViewModel>();
            CreateMap<Models.TypicalTechnologicalOperations.Spr_tto, TtoViewModel>();
            CreateMap<Uvedomlenie_mater, NotificationViewModel>();
            CreateMap<Zayvka_mater, AppForAddingMaterial>();
            //CreateMap<Spr_tem, TemViewModel>();
           //CreateMap<Spr_Perizd, PerIzd>();
            CreateMap<GOST_mater, GostMaterViewModel>();
            CreateMap<Trud_ob, TrudObViewModel>();
            CreateMap<Tasks, TasksViewModel>();
            //CreateMap<Status_dok, StatusViewModel>();
            //CreateMap<St_sort_ob, StandartSortingViewModel>();
            //CreateMap<Spr_Zakaz, OrderViewModel>();
            CreateMap<Spr_uch_cex, DirectoryOfWorkshopSitesViewModel>();
            //CreateMap<Spr_tematik, DirectoryOfTopicsViewModel>();
            CreateMap<Spr_tehnol_izgot_proizv, DirectoryOfManufacturingTechnologies>();
           // CreateMap<Spr_Tehnizg, ManufacturingTechnology>();
           // CreateMap<Spr_specif, SpecificationReferenceViewModel>();
            CreateMap<SprSortam, AssotmentReferenceViewModel>();
            CreateMap<Spr_razd_izm, ChangesSectionViewModel>();
           // CreateMap<Spr_Razd_Izd, ProductSectionViewModel>();
           // CreateMap<Spr_Razd_DSE, DseSectionViewModel>();
           // CreateMap<Spr_rascex, DirectoryOfShopRoutesViewModel>();
           // CreateMap<Spr_rasc_vert, VericalDirectoryOfShopRoutesViewModel>();
            CreateMap<Spr_pvi, DirectoryOfPviViewModel>();
            CreateMap<Spr_prpokr, DirectoryOfSignsOfCoverageViewModel>();
            CreateMap<SprPrKm, DirectoryOfSignsOfMaterialCodesViewModel>();
           //CreateMap<Spr_prim_dse, DirectoryOfReferenceDseViewModel>();
            CreateMap<Spr_per_dok_ob, DirectoryOfTheListOfDocumentsViewModel>();
            CreateMap<SprOts, DirectoryOfOtsViewModel>();
            CreateMap<SprOgt, DirectoryOfOgtViewModel>();
            CreateMap<Spr_obraz, DirectoryOfSampleViewModel>();
            CreateMap<Spr_normali, DirectoryOfNormalViewModel>();
           // CreateMap<Spr_nmdet, DirectoryOfPartNamesViewModel>();
            CreateMap<Spr_nm_task, DirectoryOfTaskNamesViewModel>();
            CreateMap<Spr_METODIC, DirectoryOfMetodicViewModel>();
           // CreateMap<Spr_mater, DirectoryOfMaterViewModel>();
           // CreateMap<Spr_kompl, DirectoryOfKomplectViewModel>();
            CreateMap<SprKgr, DirectoryOfKgrViewModel>();
            CreateMap<Spr_GR_tematik, DirectoryOfTopicGroupViewModel>();
            CreateMap<SprGrMater, DirectoryOfGroupMaterViewModel>();
            CreateMap<SprEizm, DirectoryOfUnitsOfMeasurementViewModel>();
            CreateMap<Spr_dse, DirectoryOfDseViewModel>();
          //  CreateMap<Spr_cex_prizn, DirectoryOfShopHallSignViewModel>();
          //  CreateMap<Spr_cex, DirectoryOfShopViewModel>();
            CreateMap<Spr_cen_mater, DirectoryOfMaterialPriceViewModel>();
            CreateMap<SprBalSch, DirectoryOfBalanceAccountsViewModel>();
          //  CreateMap<Spr_agr, DirectoryOfAgregatesViewModel>();
            CreateMap<Sort_Mater, DirectoryOfSortingMaterialsViewModel>();
            CreateMap<Role, DirectoryOfRoleUsersViewModel>();
           // CreateMap<Raz_det, DirectoryOfPartsSectionViewModel>();
            CreateMap<Predpr_Postav, DirectoryOfSupplierEnterprisesViewModel>();
           // CreateMap<GR_RAZDIZD, DirectoryOfProductSectionGroupViewModel>();
            CreateMap<GostMater, DirectoryOfGostMaterialViewModel>();
            CreateMap<Dokum_Obosnov, DirectoryOfDocumentsJustificationViewModel>();
           // CreateMap<Der_izd, Der_izdViewModel>();
            CreateMap<Der_btp, Der_btpViewModel>();

            CreateMap<MemoAddingMaterialCode, MemoAddingMaterialCodeViewModel>();

            //CreateMap<MemoAddingMaterialCode, MemoAddingMaterialCodeViewModel>()
            //    .ForMember(dest => dest.DocumentStatus, st => st.MapFrom(x => x.Statuses.Select(r => r.Status))).MaxDepth(3);
        }
    }
}