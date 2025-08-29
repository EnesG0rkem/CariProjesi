using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CariProjesi.Models;

namespace CariProje.Models
{
    public class MovementRow
    {
        public Movement Movement { get; set; }
        public decimal RunningTotal { get; set; }

        public MovementRow(Movement movement, decimal runningTotal)
        {
            Movement = movement;
            RunningTotal = runningTotal;
        }
    }
}