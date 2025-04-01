using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Web;
using Msi = Asu.Web.Models.Msi;
using Tasks = Asu.Web.Models.UsersTask;

namespace Asu.Web.Models.ContextDb
{
    public class AsuAviaDbContext : DbContext
    {
        public AsuAviaDbContext() : base("AsuAviaContext")
        {

        }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TypicalTechnologicalOperations.Spr_tto>().Property(e => e.nrm).HasPrecision(38, 7);
            modelBuilder.Entity<TypicalTechnologicalOperations.Spr_tto>().Property(e => e.nrvp).HasPrecision(38, 7);
            modelBuilder.Entity<DirectoryOfMaterialCodifiers.Spr_skm>().Property(e => e.ves).HasPrecision(38, 7);
            modelBuilder.Entity<DirectoryOfMaterialCodifiers.Spr_cen_mater>().Property(e => e.cmat).HasPrecision(38, 7);
            modelBuilder.Entity<Msi.Spr_prim_dse>().Property(e => e.masizd).HasPrecision(38, 7);
            modelBuilder.Entity<Msi.Spr_obozn_mater>().Property(e => e.mas1sh).HasPrecision(38, 7);
            modelBuilder.Entity<Msi.Spr_rasc_vert>().Property(e => e.npp).HasPrecision(38, 7);
        }
        public DbSet<Msi.Spr_cex> Spr_cex { get; set; }
        public DbSet<Msi.Spr_tematik> Spr_tematik { get; set; }
        public DbSet<Msi.Spr_Tehnizg> Spr_Tehnizg { get; set; }
        public DbSet<Msi.Spr_tem> Spr_tem { get; set; }
        public DbSet<Msi.Spr_Zakaz> Spr_Zakaz { get; set; }
        public DbSet<Msi.Spr_kdan> Spr_kdan { get; set; }
        public DbSet<Msi.Spr_agr> Spr_agr { get; set; }
        public DbSet<Msi.Spr_Perizd> Spr_Perizd { get; set; }
        public DbSet<Msi.Spr_mash> Spr_mash { get; set; }
        public DbSet<Msi.Spr_cex_prizn> Spr_cex_prizn { get; set; }
        public DbSet<Msi.Spr_Razd_Izd> Spr_Razd_Izd { get; set; }
        public DbSet<Msi.Spr_Razd_DSE> Spr_Razd_DSE { get; set; }
        public DbSet<Msi.Raz_det> Raz_det { get; set; }
        public DbSet<Msi.Spr_PKP> Spr_PKP { get; set; }
        public DbSet<Msi.Spr_nmdet> Spr_nmdet { get; set; }
        public DbSet<Msi.Spr_obozn> Spr_obozn { get; set; }
        public DbSet<Msi.Spr_mater> Spr_mater { get; set; }
        public DbSet<Msi.GR_RAZDIZD> GR_RAZDIZD { get; set; }
        public DbSet<Msi.Spr_kompl> Spr_kompl { get; set; }
        public DbSet<Msi.Spr_rascex> Spr_rascex { get; set; }
        public DbSet<Msi.Spr_rascex_poln> Spr_rascex_poln { get; set; }
        public DbSet<Msi.Spr_rasc_vert> Spr_rasc_vert { get; set; }
        public DbSet<Msi.Spr_obozn_mater> Spr_obozn_mater { get; set; }
        public DbSet<Msi.Spr_mash_sg> Spr_mash_sg { get; set; }
        public DbSet<Msi.Spr_specif> Spr_specif { get; set; }
        public DbSet<Msi.spr_grup_prim> spr_grup_prim { get; set; }
        public DbSet<Msi.Spr_prim_dse> Spr_prim_dse { get; set; }
        public DbSet<Msi.Spr_poln_rascex> Spr_poln_rascex { get; set; }
        public DbSet<Msi.DocumentStatus> DocumentStatus { get; set; }
        public DbSet<Msi.Der_izd> Der_izd { get; set; }
        public DbSet<Msi.Spr_pvi> Spr_pvi { get; set; }
        public DbSet<UsersTask.Tasks> Tasks { get; set; }
        public DbSet<UsersTask.Spr_nm_task> Spr_nm_task { get; set; }      
        public DbSet<TypicalTechnologicalOperations.Spr_tto> Spr_tto { get; set; }
        public DbSet<TypicalTechnologicalOperations.Spr_prpokr> Spr_prpokr { get; set; }
        public DbSet<DirectoryOfMaterialCodifiers.Dokum_Obosnov> Dokum_Obosnov { get; set; }
        public DbSet<DirectoryOfMaterialCodifiers.GOST_mater> GOST_mater { get; set; }
        public DbSet<DirectoryOfMaterialCodifiers.Spr_eizm> Spr_eizm { get; set; }
        public DbSet<DirectoryOfMaterialCodifiers.Mark_mater> Mark_mater { get; set; }
        public DbSet<DirectoryOfMaterialCodifiers.Nm_mater> Nm_mater { get; set; }
        public DbSet<DirectoryOfMaterialCodifiers.Predpr_Postav> Predpr_Postav { get; set; }
        public DbSet<DirectoryOfMaterialCodifiers.Spr_balsch> Spr_balsch { get; set; }
        public DbSet<DirectoryOfMaterialCodifiers.Spr_cen_mater> Spr_cen_mater { get; set; }
        public DbSet<DirectoryOfMaterialCodifiers.SPR_OGT> SPR_OGT { get; set; }
        public DbSet<DirectoryOfMaterialCodifiers.Spr_GR_Mater> Spr_GR_Mater { get; set; }
        public DbSet<DirectoryOfMaterialCodifiers.Spr_kgr> Spr_kgr { get; set; }
        public DbSet<DirectoryOfMaterialCodifiers.SPR_OTS> SPR_OTS { get; set; }
        public DbSet<DirectoryOfMaterialCodifiers.SPR_PRKM> SPR_PRKM { get; set; }
        public DbSet<DirectoryOfMaterialCodifiers.Spr_skm> Spr_skm { get; set; }
        public DbSet<DirectoryOfMaterialCodifiers.SPR_sortam> SPR_sortam { get; set; }
        public DbSet<DirectoryOfMaterialCodifiers.Sort_Mater> Sort_Mater { get; set; }
        public DbSet<Metrology.Gos_ree> Gos_ree { get; set; }
        public DbSet<Metrology.Grupp> Grupp { get; set; }
        public DbSet<Metrology.Konserv> Konserv { get; set; }
        public DbSet<Metrology.M_Poverk> M_Poverk { get; set; }
        public DbSet<Metrology.Nazn_prib> Nazn_prib { get; set; }
        public DbSet<Metrology.Nm_prib> Nm_prib { get; set; }
        public DbSet<Metrology.Nm_vidiz> Nm_vidiz { get; set; }
        public DbSet<Metrology.Period_pover> Period_pover { get; set; }
        public DbSet<Metrology.Podgrupp> Podgrupp { get; set; }
        public DbSet<Metrology.Podgr_prib> Podgr_prib { get; set; }
        public DbSet<Metrology.Predpr_izg> Predpr_izg { get; set; }
        public DbSet<Metrology.Rod_poverk> Rod_poverk { get; set; }
        public DbSet<Metrology.Spr_cena_del> Spr_cena_del { get; set; }
        public DbSet<Metrology.Spr_klass_tochn> Spr_klass_tochn { get; set; }
        public DbSet<Metrology.Spr_metrol> Spr_metrol { get; set; }
        public DbSet<Metrology.Spr_predel> Spr_predel { get; set; }
        public DbSet<Metrology.Spr_stan> Spr_stan { get; set; }
        public DbSet<Metrology.Tip_pribora> Tip_pribora { get; set; }
        public DbSet<Metrology.Usl_expluat> Usl_expluat { get; set; }
        public DbSet<Metrology.Vid_izmer> Vid_izmer { get; set; }
    }
}