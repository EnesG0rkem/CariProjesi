using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CariProjesi.Models
{
    public abstract class BaseEntity
    {
        public bool IsDeleted { get; set; } = false;
    }
}