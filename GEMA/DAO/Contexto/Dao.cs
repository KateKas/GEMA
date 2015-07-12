using GEMA.Models;
using System.Data.Entity;

namespace GEMA.DAO.Contexto
{
    public class Dao : DbContext
    {
        public Dao()
            : base("name=GEMA")
        {            
        }

        public DbSet<Pessoas> Pessoas { get; set; }
        public DbSet<Materias> Materias { get; set; }
        public DbSet<Eventos> Eventos { get; set; }
        public DbSet<Gerentes> Gerentes { get; set; }
        public DbSet<Jornalistas> Jornalistas { get; set; }
        public DbSet<Publicadores> Publicadores { get; set; }
        public DbSet<Revisores> Revisores { get; set; }
        public DbSet<Secoes> Secoes { get; set; }
        public DbSet<Comentarios> Comentarios { get; set; }

        public DbSet<Papeis> Papeis { get; set; }

    }
}
