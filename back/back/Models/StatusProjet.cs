using System;
using System.Collections.Generic;

namespace back.Models
{
    public partial class StatusProjet
    {
        public StatusProjet()
        {
            Projets = new HashSet<Projet>();
        }

        public int Id { get; set; }
        public string Nom { get; set; } = null!;

        public virtual ICollection<Projet> Projets { get; set; }
    }
}
