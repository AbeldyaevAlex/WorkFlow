using Asu.Core.Domain.DirectoryOfMaterialCodifiers;
using Asu.Core.Domain.Logging;
using Asu.Core.Domain.Metrology;
using Asu.Core.Domain.Msi;
using Asu.Core.Domain.TypicalTechnologicalOperations;
//using Asu.Core.Domain.UsersTask;
using Asu.Core.Domain.Work;
using System.Data.Entity;

namespace Asu.Data
{
    public class WorkContext : DbContext
    {
        public WorkContext() : base("AsuAviaContextDependency")
        {

        }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Spr_tto>().Property(e => e.Nrm).HasPrecision(38, 7);
            modelBuilder.Entity<Spr_tto>().Property(e => e.Nrvp).HasPrecision(38, 7);
            //modelBuilder.Entity<SprSkm>().Property(e => e.Ves).HasPrecision(38, 7);
            //modelBuilder.Entity<SprCenMater>().Property(e => e.Cmat).HasPrecision(38, 7);
            modelBuilder.Entity<Spr_prim_dse>().Property(e => e.Masizd).HasPrecision(38, 7);
            modelBuilder.Entity<Spr_obozn_mater>().Property(e => e.Mas1sh).HasPrecision(38, 7);
            modelBuilder.Entity<Spr_rasc_vert>().Property(e => e.Npp).HasPrecision(38, 7);
            //modelBuilder.Entity<Der_izd>().Property(e => e.masizd).HasPrecision(38, 7);
            //modelBuilder.Entity<Der_izd>().Property(e => e.mas1sh).HasPrecision(38, 7);
            modelBuilder.Entity<DirectiveWork>().Property(e => e.Directive_work_sdeln_izg).HasPrecision(38, 5);
            modelBuilder.Entity<DirectiveWork>().Property(e => e.Directive_work_povr_izg).HasPrecision(38, 5);
            modelBuilder.Entity<DirectiveWork>().Property(e => e.Directive_work_sdeln_usl).HasPrecision(38, 5);
            modelBuilder.Entity<DirectiveWork>().Property(e => e.Directive_work_povr_usl).HasPrecision(38, 5);
        }
        public DbSet<Spr_cex> Spr_cex { get; set; }
        public DbSet<Spr_tematik> Spr_tematik { get; set; }
        public DbSet<Spr_Tehnizg> Spr_Tehnizg { get; set; }
        public DbSet<Spr_tem> Spr_tem { get; set; }
        public DbSet<Spr_Zakaz> Spr_Zakaz { get; set; }
        public DbSet<Spr_kdan> Spr_kdan { get; set; }
        public DbSet<Spr_agr> Spr_agr { get; set; }
        public DbSet<Spr_Perizd> Spr_Perizd { get; set; }
        public DbSet<Spr_mash> Spr_mash { get; set; }
        //public DbSet<Spr_cex_prizn> Spr_cex_prizn { get; set; }
        public DbSet<Spr_Razd_Izd> Spr_Razd_Izd { get; set; }
        public DbSet<Spr_Razd_DSE> Spr_Razd_DSE { get; set; }
        public DbSet<Raz_det> Raz_det { get; set; }
        public DbSet<Spr_pkp> Spr_pkp { get; set; }
        public DbSet<Spr_nmdet> Spr_nmdet { get; set; }
        public DbSet<Spr_obozn> Spr_obozn { get; set; }
        public DbSet<Spr_mater> Spr_mater { get; set; }
        public DbSet<GR_RAZDIZD> GR_RAZDIZD { get; set; }
        public DbSet<Spr_kompl> Spr_kompl { get; set; }
        public DbSet<Spr_rascex> Spr_rascex { get; set; }
        public DbSet<Spr_rascex_poln> Spr_rascex_poln { get; set; }
        public DbSet<Spr_rasc_vert> Spr_rasc_vert { get; set; }
        public DbSet<Spr_obozn_mater> Spr_obozn_mater { get; set; }
        public DbSet<Spr_mash_sg> Spr_mash_sg { get; set; }
        public DbSet<Spr_specif> Spr_specif { get; set; }
        public DbSet<spr_grup_prim> spr_grup_prim { get; set; }
        public DbSet<Spr_prim_dse> Spr_prim_dse { get; set; }
        public DbSet<Spr_poln_rascex> Spr_poln_rascex { get; set; }
        public DbSet<Core.Domain.StatusDirectory.DocumentStatus> DocumentStatus { get; set; }
        public DbSet<Der_izd> Der_izd { get; set; }
        public DbSet<Core.Domain.Pvi.Spr_pvi> Spr_pvi { get; set; }
        //public DbSet<Tasks> Tasks { get; set; }
        //public DbSet<Spr_nm_task> Spr_nm_task { get; set; }
        public DbSet<Spr_tto> Spr_tto { get; set; }
        public DbSet<Spr_prpokr> Spr_prpokr { get; set; }
        //public DbSet<DokumObosnov> Dokum_Obosnov { get; set; }
        //public DbSet<GostMater> GOST_mater { get; set; }
        //public DbSet<SprEizm> Spr_eizm { get; set; }
        //public DbSet<MarkMater> Mark_mater { get; set; }
        //public DbSet<DirectoryOfMaterialName> Nm_mater { get; set; }
        //public DbSet<PredprPostav> Predpr_Postav { get; set; }
        //public DbSet<SprBalSch> Spr_balsch { get; set; }
        //public DbSet<SprCenMater> Spr_cen_mater { get; set; }
        //public DbSet<SprOgt> SPR_OGT { get; set; }
        //public DbSet<SprGrMater> Spr_GR_Mater { get; set; }
        //public DbSet<SprKgr> Spr_kgr { get; set; }
        //public DbSet<SprOts> SPR_OTS { get; set; }
        //public DbSet<SprPrKm> SPR_PRKM { get; set; }
        //public DbSet<SprSkm> Spr_skm { get; set; }
        //public DbSet<SprSortam> SPR_sortam { get; set; }
        //public DbSet<SortMater> Sort_Mater { get; set; }
        //public DbSet<UslSkm> Usl_Skm { get; set; }
        public DbSet<Gos_ree> Gos_ree { get; set; }
        public DbSet<Grupp> Grupp { get; set; }
        public DbSet<Konserv> Konserv { get; set; }
        public DbSet<M_Poverk> M_Poverk { get; set; }
        public DbSet<Nazn_prib> Nazn_prib { get; set; }
        public DbSet<Nm_prib> Nm_prib { get; set; }
        public DbSet<Nm_vidiz> Nm_vidiz { get; set; }
        public DbSet<Period_pover> Period_pover { get; set; }
        public DbSet<Podgrupp> Podgrupp { get; set; }
        public DbSet<Podgr_prib> Podgr_prib { get; set; }
        public DbSet<Predpr_izg> Predpr_izg { get; set; }
        public DbSet<Rod_poverk> Rod_poverk { get; set; }
        public DbSet<Spr_cena_del> Spr_cena_del { get; set; }
        public DbSet<Spr_klass_tochn> Spr_klass_tochn { get; set; }
        public DbSet<Spr_metrol> Spr_metrol { get; set; }
        public DbSet<Spr_predel> Spr_predel { get; set; }
        public DbSet<Spr_stan> Spr_stan { get; set; }
        public DbSet<Tip_pribora> Tip_pribora { get; set; }
        public DbSet<Usl_expluat> Usl_expluat { get; set; }
        public DbSet<Vid_izmer> Vid_izmer { get; set; }
        public DbSet<DirectiveWork> DirectiveWork { get; set; }
        public DbSet<DirectoryOfTypesOfWork> DirectoryOfTypesOfWork { get; set; }
        public DbSet<ExceptionForWork> ExceptionForWork { get; set; }
        //public DbSet<Log> Log { get; set; }
        //public DbSet<ActivityLog> ActivityLog { get; set; }
        //public DbSet<ActivityLogType> ActivityLogType { get; set; }
    }
}
