using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AplicacaoRevisao.Repository.Context
{
    public class AplicacaoRevisaoContext : DbContext
    {
        public AplicacaoRevisaoContext()
        {
        }

        public AplicacaoRevisaoContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

        }
    }
}
