using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PortariaInteligente.Models;


namespace PortariaInteligente.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //As chaves primárias e estrangeiras do Identity são criadas em base.OnModelCreating(modelBuilder)
           base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Convite>()
            .HasKey(ev => new { ev.ReuniaoID, ev.VisitanteID });
           modelBuilder.Entity<Convite>()
               .HasOne(ev => ev.Reunioes)
               .WithMany(e => e.Convites)
               .HasForeignKey(ev => ev.ReuniaoID);
           modelBuilder.Entity<Convite>()
               .HasOne(ev => ev.Visitantes)
               .WithMany(v => v.Convites)
               .HasForeignKey(ev => ev.VisitanteID);

            modelBuilder.Entity<PapelVisitado>()
                .HasKey(up => new { up.VisitadoID, up.PapelID });
            modelBuilder.Entity<PapelVisitado>()
                .HasOne(up => up.Visitados)
                .WithMany(u => u.PapeisVisitados)
                .HasForeignKey(up => up.VisitadoID);
            modelBuilder.Entity<PapelVisitado>()
                .HasOne(up => up.Papeis)
                .WithMany(p => p.PapeisVisitados)
                .HasForeignKey(up => up.PapelID);
        }
        
        public DbSet<Visitado> Visitados { get; set; }
        public DbSet<Papel> Papeis { get; set; }
        public DbSet<PapelVisitado> PapeisVisitado { get; set; }
        public DbSet<Visitante> Visitantes { get; set; }
        public DbSet<Documento> Documentos { get; set; }
        public DbSet<Reuniao> Reunioes { get; set; }
        public DbSet<Convite> Convites { get; set; }     

    }
}
