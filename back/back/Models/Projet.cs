using System;
using System.Collections.Generic;

namespace back.Models
{
    public partial class Projet
    {
        public Projet()
        {
            Tickets = new HashSet<Ticket>();
            IdComptes = new HashSet<Compte>();
        }

        public int Id { get; set; }
        public string Nom { get; set; } = null!;

        public virtual ICollection<Ticket> Tickets { get; set; }

        public virtual ICollection<Compte> IdComptes { get; set; }
    }
}
