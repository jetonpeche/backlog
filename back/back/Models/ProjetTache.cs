using System;
using System.Collections.Generic;

namespace back.Models
{
    public partial class ProjetTache
    {
        public int Id { get; set; }
        public string Description { get; set; } = null!;
        public int IdStatusTache { get; set; }
        public int IdProjet { get; set; }

        public virtual Projet IdProjetNavigation { get; set; } = null!;
        public virtual StatusTache IdStatusTacheNavigation { get; set; } = null!;
    }
}
