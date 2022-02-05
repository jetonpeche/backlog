using System;
using System.Collections.Generic;

namespace back.Models
{
    public partial class Entreprise
    {
        public Entreprise()
        {
            Comptes = new HashSet<Compte>();
        }

        public int Id { get; set; }
        public string Nom { get; set; } = null!;

        public virtual ICollection<Compte> Comptes { get; }
    }
}
