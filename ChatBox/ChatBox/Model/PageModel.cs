using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatBox.Model
{
    public class PageModel
    {
        public int CustomerCount { get; set; }
        public string Introduce { get; set; }
        public DateOnly Birthday { get; set; }
        public decimal TransactionValue { get; set; }
        public TimeOnly ShipmentDelivery { get; set; }
        public bool LocationStatus { get; set; }

    }
}
