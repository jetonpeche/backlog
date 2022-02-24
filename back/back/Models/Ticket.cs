using System;
using System.Collections.Generic;

namespace back.Models
{
    public partial class Ticket
    {
        public int Id { get; set; }
        public int IdProjet { get; set; }
        public int IdCompte { get; set; }
        public int IdTypeRetour { get; set; }
        public int IdEtatTicket { get; set; }
        public string Msg { get; set; } = null!;

        public virtual Compte IdCompteNavigation { get; set; } = null!;
        public virtual EtatTicket IdEtatTicketNavigation { get; set; } = null!;
        public virtual Projet IdProjetNavigation { get; set; } = null!;
        public virtual TypeRetour IdTypeRetourNavigation { get; set; } = null!;
    }
}
