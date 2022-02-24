using System;
using System.Collections.Generic;

namespace back.Models
{
    public partial class Projet
    {
        public Projet()
        {
            ProjetComptes = new HashSet<ProjetCompte>();
            Tickets = new HashSet<Ticket>();
        }

        public int Id { get; set; }
        public string Nom { get; set; } = null!;
        public string Description { get; set; } = null!;
        public int? IdStatus { get; set; }
        public int IdCompteClient { get; set; }

        public virtual Compte IdCompteClientNavigation { get; set; } = null!;
        public virtual StatusProjet? IdStatusNavigation { get; set; }
        public virtual ICollection<ProjetCompte> ProjetComptes { get; set; }
        public virtual ICollection<Ticket> Tickets { get; set; }
    }
}
