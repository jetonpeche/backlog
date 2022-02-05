using System;
using System.Collections.Generic;

namespace back.Models
{
    public partial class Compte
    {
        public Compte()
        {
            Tickets = new HashSet<Ticket>();
            IdProjets = new HashSet<Projet>();
        }

        public int Id { get; set; }
        public string Nom { get; set; } = null!;
        public string Prenom { get; set; } = null!;
        public string Mail { get; set; } = null!;
        public string Mdp { get; set; } = null!;
        public int IdEntreprise { get; set; }
        public int IdTypeCompte { get; set; }

        public virtual Entreprise IdEntrepriseNavigation { get; set; } = null!;
        public virtual TypeCompte IdTypeCompteNavigation { get; set; } = null!;
        public virtual ICollection<Ticket> Tickets { get; set; }

        public virtual ICollection<Projet> IdProjets { get; set; }
    }
}
