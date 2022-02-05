using System;
using System.Collections.Generic;

namespace back.Models
{
    public partial class EtatTicket
    {
        public EtatTicket()
        {
            Tickets = new HashSet<Ticket>();
        }

        public int Id { get; set; }
        public string Nom { get; set; } = null!;

        public virtual ICollection<Ticket> Tickets { get; }
    }
}
