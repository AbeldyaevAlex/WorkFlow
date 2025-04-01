using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Asu.Web.Models
{
    public class PGDbContext : DbContext
    {
        public PGDbContext() : base("PgMalaxitContext")
        {

        }
        public virtual DbSet<Spr_cex_test> Workshop { get; set; }
    }
}