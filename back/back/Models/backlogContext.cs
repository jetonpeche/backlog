using Microsoft.EntityFrameworkCore;

namespace back.Models
{
    public partial class backlogContext : DbContext
    {
        public backlogContext()
        {
        }

        public backlogContext(DbContextOptions<backlogContext> options): base(options)
        {
        }

        public virtual DbSet<Compte> Comptes { get; set; } = null!;
        public virtual DbSet<Entreprise> Entreprises { get; set; } = null!;
        public virtual DbSet<EtatTicket> EtatTickets { get; set; } = null!;
        public virtual DbSet<Projet> Projets { get; set; } = null!;
        public virtual DbSet<Ticket> Tickets { get; set; } = null!;
        public virtual DbSet<TypeCompte> TypeComptes { get; set; } = null!;
        public virtual DbSet<TypeRetour> TypeRetours { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Compte>(entity =>
            {
                entity.ToTable("Compte");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.IdEntreprise).HasColumnName("idEntreprise");

                entity.Property(e => e.IdTypeCompte).HasColumnName("idTypeCompte");

                entity.Property(e => e.Mail)
                    .HasMaxLength(300)
                    .IsUnicode(false)
                    .HasColumnName("mail");

                entity.Property(e => e.Mdp)
                    .HasMaxLength(300)
                    .IsUnicode(false)
                    .HasColumnName("mdp");

                entity.Property(e => e.Nom)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("nom");

                entity.Property(e => e.Prenom)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("prenom");

                entity.HasOne(d => d.IdEntrepriseNavigation)
                    .WithMany(p => p.Comptes)
                    .HasForeignKey(d => d.IdEntreprise)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Compte__idEntrep__4CA06362");

                entity.HasOne(d => d.IdTypeCompteNavigation)
                    .WithMany(p => p.Comptes)
                    .HasForeignKey(d => d.IdTypeCompte)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Compte__idTypeCo__4BAC3F29");

                entity.HasMany(d => d.IdProjets)
                    .WithMany(p => p.IdComptes)
                    .UsingEntity<Dictionary<string, object>>(
                        "ProjetCompte",
                        l => l.HasOne<Projet>().WithMany().HasForeignKey("IdProjet").OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("FK__Projet_Co__idPro__5070F446"),
                        r => r.HasOne<Compte>().WithMany().HasForeignKey("IdCompte").OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("FK__Projet_Co__idCom__4F7CD00D"),
                        j =>
                        {
                            j.HasKey("IdCompte", "IdProjet").HasName("PK__Projet_C__4A309BE42A54E6B1");

                            j.ToTable("Projet_Compte");

                            j.IndexerProperty<int>("IdCompte").HasColumnName("idCompte");

                            j.IndexerProperty<int>("IdProjet").HasColumnName("idProjet");
                        });
            });

            modelBuilder.Entity<Entreprise>(entity =>
            {
                entity.ToTable("Entreprise");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Nom)
                    .HasMaxLength(300)
                    .IsUnicode(false)
                    .HasColumnName("nom");
            });

            modelBuilder.Entity<EtatTicket>(entity =>
            {
                entity.ToTable("EtatTicket");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Nom)
                    .HasMaxLength(200)
                    .IsUnicode(false)
                    .HasColumnName("nom");
            });

            modelBuilder.Entity<Projet>(entity =>
            {
                entity.ToTable("Projet");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Nom)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("nom");
            });

            modelBuilder.Entity<Ticket>(entity =>
            {
                entity.ToTable("Ticket");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.IdCompte).HasColumnName("idCompte");

                entity.Property(e => e.IdEtatTicket).HasColumnName("idEtatTicket");

                entity.Property(e => e.IdProjet).HasColumnName("idProjet");

                entity.Property(e => e.IdTypeRetour).HasColumnName("idTypeRetour");

                entity.Property(e => e.Msg)
                    .HasMaxLength(1000)
                    .IsUnicode(false)
                    .HasColumnName("msg");

                entity.HasOne(d => d.IdCompteNavigation)
                    .WithMany(p => p.Tickets)
                    .HasForeignKey(d => d.IdCompte)
                    .HasConstraintName("FK__Ticket__idCompte__5441852A");

                entity.HasOne(d => d.IdEtatTicketNavigation)
                    .WithMany(p => p.Tickets)
                    .HasForeignKey(d => d.IdEtatTicket)
                    .HasConstraintName("FK__Ticket__idEtatTi__5629CD9C");

                entity.HasOne(d => d.IdProjetNavigation)
                    .WithMany(p => p.Tickets)
                    .HasForeignKey(d => d.IdProjet)
                    .HasConstraintName("FK__Ticket__idProjet__534D60F1");

                entity.HasOne(d => d.IdTypeRetourNavigation)
                    .WithMany(p => p.Tickets)
                    .HasForeignKey(d => d.IdTypeRetour)
                    .HasConstraintName("FK__Ticket__idTypeRe__5535A963");
            });

            modelBuilder.Entity<TypeCompte>(entity =>
            {
                entity.ToTable("TypeCompte");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Nom)
                    .HasMaxLength(300)
                    .IsUnicode(false)
                    .HasColumnName("nom");
            });

            modelBuilder.Entity<TypeRetour>(entity =>
            {
                entity.ToTable("TypeRetour");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Nom)
                    .HasMaxLength(300)
                    .IsUnicode(false)
                    .HasColumnName("nom");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
