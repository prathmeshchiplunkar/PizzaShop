using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PizzaOrderingSystem.DataAccess.Models
{
    public class PizzaInventory
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Type { get; set; }
        public int Section { get; set; }
        public int Reguler { get; set; }
        public int Medium { get; set; }
        public int Large { get; set; }
        public int Category { get; set; }
        public string Description { get; set; }
    }
}
