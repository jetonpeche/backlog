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
        public virtual DbSet<Ticket> Tickets { get; set; } = null!;
        public virtual DbSet<TypeCompte> TypeComptes { get; set; } = null!;
        public virtual DbSet<TypeRetour> TypeRetours { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Data Source=desktop-j5htqcs\\sqlserver;Initial Catalog=backlog;Integrated Security=True");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Compte>(entity =>
            {
                entity.ToTable("Compte");

                entity.Property(e => e.Id).HasColumnName("id");

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
                    .HasColumnName("nomEntreprise");

                entity.Property(e => e.Prenom)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("prenom");

                entity.Property(e => e.Tel)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("tel");

                entity.HasOne(d => d.IdTypeCompteNavigation)
                    .WithMany(p => p.Comptes)
                    .HasForeignKey(d => d.IdTypeCompte)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Compte__idTypeCo__0A9D95DB");
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

                entity.Property(e => e.Nom)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("nom");
            });

            modelBuilder.Entity<ProjetCompte>(entity =>
            {
                entity.HasKey(e => new { e.IdCompte, e.IdProjet })
                    .HasName("PK__Projet_C__4A309BE4FA4CFDDC");

                entity.ToTable("Projet_Compte");

                entity.Property(e => e.IdCompte).HasColumnName("idCompte");

                entity.Property(e => e.IdProjet).HasColumnName("idProjet");

                entity.Property(e => e.EstChefProjet).HasColumnName("estChefProjet");

                entity.HasOne(d => d.IdCompteNavigation)
                    .WithMany(p => p.ProjetComptes)
                    .HasForeignKey(d => d.IdCompte)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Projet_Co__idCom__0D7A0286");

                entity.HasOne(d => d.IdProjetNavigation)
                    .WithMany(p => p.ProjetComptes)
                    .HasForeignKey(d => d.IdProjet)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Projet_Co__idPro__0E6E26BF");
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
                    .HasConstraintName("FK__Ticket__idCompte__123EB7A3");

                entity.HasOne(d => d.IdEtatTicketNavigation)
                    .WithMany(p => p.Tickets)
                    .HasForeignKey(d => d.IdEtatTicket)
                    .HasConstraintName("FK__Ticket__idEtatTi__14270015");

                entity.HasOne(d => d.IdProjetNavigation)
                    .WithMany(p => p.Tickets)
                    .HasForeignKey(d => d.IdProjet)
                    .HasConstraintName("FK__Ticket__idProjet__114A936A");

                entity.HasOne(d => d.IdTypeRetourNavigation)
                    .WithMany(p => p.Tickets)
                    .HasForeignKey(d => d.IdTypeRetour)
                    .HasConstraintName("FK__Ticket__idTypeRe__1332DBDC");
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
