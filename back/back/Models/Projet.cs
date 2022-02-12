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

        public virtual ICollection<ProjetCompte> ProjetComptes { get; set; }
        public virtual ICollection<Ticket> Tickets { get; set; }
    }
}
