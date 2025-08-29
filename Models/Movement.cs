using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CariProjesi.Models
{
    public class Movement : BaseEntity
    {
        public string? AccountCode { get; set; }
        public string? AccountName { get; set; }
        public Guid MovementId { get; set; }
        public DateTime MovementDate { get; set; }
        public string? MovementDescription { get; set; }
        public bool MovementType { get; set; }
        public decimal MovementChange { get; set; }
    }
}