using System;
using System.Collections.Generic;

namespace back.Models
{
    public partial class ProjetCompte
    {
        public int IdCompte { get; set; }
        public int IdProjet { get; set; }
        public int? EstChefProjet { get; set; }

        public virtual Compte IdCompteNavigation { get; set; } = null!;
        public virtual Projet IdProjetNavigation { get; set; } = null!;
    }
}
