using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CariProjesi.Models
{
    public class Account : BaseEntity
    {
        public string? AccountCode { get; set; }
        public string? AccountName { get; set; }
        public string? AccountAddress { get; set; }
        public string? AccountDistrict { get; set; }
        public string? AccountCity { get; set; }
        public string? AccountCountry { get; set; }
        public string? AccountPhone { get; set; }
        public string? AccountEmail { get; set; }
        public decimal AccountDebit { get; set; }
        public decimal AccountCredit { get; set; }
    }
}