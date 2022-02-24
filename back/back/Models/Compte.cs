using System;
using System.Collections.Generic;

namespace back.Models
{
    public partial class Compte
    {
        public Compte()
        {
            ProjetComptes = new HashSet<ProjetCompte>();
            Projets = new HashSet<Projet>();
            Tickets = new HashSet<Ticket>();
        }

        public int Id { get; set; }
        public string Nom { get; set; } = null!;
        public string Prenom { get; set; } = null!;
        public string Mail { get; set; } = null!;
        public string Mdp { get; set; } = null!;
        public string Tel { get; set; } = null!;
        public string? Adresse { get; set; }
        public string? NomEntreprise { get; set; }
        public int IdTypeCompte { get; set; }

        public virtual TypeCompte IdTypeCompteNavigation { get; set; } = null!;
        public virtual ICollection<ProjetCompte> ProjetComptes { get; set; }
        public virtual ICollection<Projet> Projets { get; set; }
        public virtual ICollection<Ticket> Tickets { get; set; }
    }
}
