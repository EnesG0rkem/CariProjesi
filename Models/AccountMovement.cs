using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CariProjesi.Models
{
    public class AccountMovement : BaseEntity
    {
        public Guid AccountId { get; set; }
        public DateTime MovementDate { get; set; }
        public string? MovementDescription { get; set; }
        public decimal MovementDebit { get; set; }
        public decimal MovementCredit { get; set; }
    }
}