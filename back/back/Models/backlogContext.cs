using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace back.Models
{
    public partial class backlogContext : DbContext
    {
        public backlogContext()
        {
        }

        public backlogContext(DbContextOptions<backlogContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Compte> Comptes { get; set; } = null!;
        public virtual DbSet<EtatTicket> EtatTickets { get; set; } = null!;
        public virtual DbSet<Projet> Projets { get; set; } = null!;
        public virtual DbSet<ProjetCompte> ProjetComptes { get; set; } = null!;
        public virtual DbSet<ProjetTache> ProjetTaches { get; set; } = null!;
        public virtual DbSet<StatusProjet> StatusProjets { get; set; } = null!;
        public virtual DbSet<StatusTache> StatusTaches { get; set; } = null!;
        public virtual DbSet<Ticket> Tickets { get; set; } = null!;
        public virtual DbSet<TypeCompte> TypeComptes { get; set; } = null!;
        public virtual DbSet<TypeRetour> TypeRetours { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Compte>(entity =>
            {
                entity.ToTable("Compte");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Adresse)
                    .HasMaxLength(300)
                    .IsUnicode(false)
                    .HasColumnName("adresse")
                    .HasDefaultValueSql("('')");

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

                entity.Property(e => e.NomEntreprise)
                    .HasMaxLength(300)
                    .IsUnicode(false)
                    .HasColumnName("nomEntreprise")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Prenom)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("prenom");

                entity.Property(e => e.Tel)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("tel")
                    .IsFixedLength();

                entity.HasOne(d => d.IdTypeCompteNavigation)
                    .WithMany(p => p.Comptes)
                    .HasForeignKey(d => d.IdTypeCompte)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Compte__idTypeCo__2704CA5F");
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

                entity.Property(e => e.Description)
                    .HasMaxLength(1500)
                    .IsUnicode(false)
                    .HasColumnName("description");

                entity.Property(e => e.IdCompteClient).HasColumnName("idCompteClient");

                entity.Property(e => e.IdStatus)
                    .HasColumnName("idStatus")
                    .HasDefaultValueSql("((2))");

                entity.Property(e => e.Nom)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("nom");

                entity.HasOne(d => d.IdCompteClientNavigation)
                    .WithMany(p => p.Projets)
                    .HasForeignKey(d => d.IdCompteClient)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Projet__idCompte__2BC97F7C");

                entity.HasOne(d => d.IdStatusNavigation)
                    .WithMany(p => p.Projets)
                    .HasForeignKey(d => d.IdStatus)
                    .HasConstraintName("FK__Projet__idStatus__2AD55B43");
            });

            modelBuilder.Entity<ProjetCompte>(entity =>
            {
                entity.HasKey(e => new { e.IdCompte, e.IdProjet })
                    .HasName("PK__Projet_C__4A309BE4153FBF6D");

                entity.ToTable("Projet_Compte");

                entity.Property(e => e.IdCompte).HasColumnName("idCompte");

                entity.Property(e => e.IdProjet).HasColumnName("idProjet");

                entity.Property(e => e.EstChefProjet)
                    .HasColumnName("estChefProjet")
                    .HasDefaultValueSql("((0))");

                entity.HasOne(d => d.IdCompteNavigation)
                    .WithMany(p => p.ProjetComptes)
                    .HasForeignKey(d => d.IdCompte)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Projet_Co__idCom__336AA144");

                entity.HasOne(d => d.IdProjetNavigation)
                    .WithMany(p => p.ProjetComptes)
                    .HasForeignKey(d => d.IdProjet)
                    .HasConstraintName("FK__Projet_Co__idPro__345EC57D");
            });

            modelBuilder.Entity<ProjetTache>(entity =>
            {
                entity.ToTable("Projet_Tache");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Description)
                    .HasMaxLength(1500)
                    .IsUnicode(false)
                    .HasColumnName("description");

                entity.Property(e => e.IdProjet).HasColumnName("idProjet");

                entity.Property(e => e.IdStatusTache).HasColumnName("idStatusTache");

                entity.HasOne(d => d.IdProjetNavigation)
                    .WithMany(p => p.ProjetTaches)
                    .HasForeignKey(d => d.IdProjet)
                    .HasConstraintName("FK__Projet_Ta__idPro__2F9A1060");

                entity.HasOne(d => d.IdStatusTacheNavigation)
                    .WithMany(p => p.ProjetTaches)
                    .HasForeignKey(d => d.IdStatusTache)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Projet_Ta__idSta__2EA5EC27");
            });

            modelBuilder.Entity<StatusProjet>(entity =>
            {
                entity.ToTable("StatusProjet");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Nom)
                    .HasMaxLength(200)
                    .IsUnicode(false)
                    .HasColumnName("nom");
            });

            modelBuilder.Entity<StatusTache>(entity =>
            {
                entity.ToTable("StatusTache");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.CouleurFont)
                    .HasMaxLength(7)
                    .IsUnicode(false)
                    .HasColumnName("couleurFont")
                    .IsFixedLength();

                entity.Property(e => e.Nom)
                    .HasMaxLength(200)
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
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Ticket__idCompte__382F5661");

                entity.HasOne(d => d.IdEtatTicketNavigation)
                    .WithMany(p => p.Tickets)
                    .HasForeignKey(d => d.IdEtatTicket)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Ticket__idEtatTi__3A179ED3");

                entity.HasOne(d => d.IdProjetNavigation)
                    .WithMany(p => p.Tickets)
                    .HasForeignKey(d => d.IdProjet)
                    .HasConstraintName("FK__Ticket__idProjet__373B3228");

                entity.HasOne(d => d.IdTypeRetourNavigation)
                    .WithMany(p => p.Tickets)
                    .HasForeignKey(d => d.IdTypeRetour)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Ticket__idTypeRe__39237A9A");
            });

            modelBuilder.Entity<TypeCompte>(entity =>
            {
                entity.ToTable("TypeCompte");

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("id");

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
