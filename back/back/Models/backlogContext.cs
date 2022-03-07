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
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Data Source=DESKTOP-U41J905\\SQLEXPRESS;Initial Catalog=backlog;Integrated Security=True");
            }
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
                    .HasConstraintName("FK__Compte__idTypeCo__300424B4");
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
                    .HasConstraintName("FK__Projet__idCompte__34C8D9D1");

                entity.HasOne(d => d.IdStatusNavigation)
                    .WithMany(p => p.Projets)
                    .HasForeignKey(d => d.IdStatus)
                    .HasConstraintName("FK__Projet__idStatus__33D4B598");
            });

            modelBuilder.Entity<ProjetCompte>(entity =>
            {
                entity.HasKey(e => new { e.IdCompte, e.IdProjet })
                    .HasName("PK__Projet_C__4A309BE4F4D86528");

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
                    .HasConstraintName("FK__Projet_Co__idCom__3C69FB99");

                entity.HasOne(d => d.IdProjetNavigation)
                    .WithMany(p => p.ProjetComptes)
                    .HasForeignKey(d => d.IdProjet)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Projet_Co__idPro__3D5E1FD2");
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
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Projet_Ta__idPro__38996AB5");

                entity.HasOne(d => d.IdStatusTacheNavigation)
                    .WithMany(p => p.ProjetTaches)
                    .HasForeignKey(d => d.IdStatusTache)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Projet_Ta__idSta__37A5467C");
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
                    .HasConstraintName("FK__Ticket__idCompte__412EB0B6");

                entity.HasOne(d => d.IdEtatTicketNavigation)
                    .WithMany(p => p.Tickets)
                    .HasForeignKey(d => d.IdEtatTicket)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Ticket__idEtatTi__4316F928");

                entity.HasOne(d => d.IdProjetNavigation)
                    .WithMany(p => p.Tickets)
                    .HasForeignKey(d => d.IdProjet)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Ticket__idProjet__403A8C7D");

                entity.HasOne(d => d.IdTypeRetourNavigation)
                    .WithMany(p => p.Tickets)
                    .HasForeignKey(d => d.IdTypeRetour)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Ticket__idTypeRe__4222D4EF");
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
