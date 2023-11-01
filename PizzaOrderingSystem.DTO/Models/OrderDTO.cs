using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PizzaOrderingSystem.DTO.Models
{
    public class OrderDTO
    {
        public int Id { get; set; }
        public string CustomerName { get; set; } = string.Empty;
        public DateTime TimeStamp { get; set; }
        public int OrderStatus { get; set; }

    }
}
