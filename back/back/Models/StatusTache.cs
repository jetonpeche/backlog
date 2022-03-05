using System;
using System.Collections.Generic;

namespace back.Models
{
    public partial class StatusTache
    {
        public StatusTache()
        {
            ProjetTaches = new HashSet<ProjetTache>();
        }

        public int Id { get; set; }
        public string Nom { get; set; } = null!;
        public string CouleurFont { get; set; } = null!;

        public virtual ICollection<ProjetTache> ProjetTaches { get; set; }
    }
}
