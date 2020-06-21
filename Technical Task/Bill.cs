using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Technical_Task
{
    public class Bill
    {
        public int id { get; set; }
        public string Item { get; set; }
        public string Unit { get; set; }
        public string Price { get; set; }
        public string Qty { get; set; }
        public string Total { get; set; }
        public string Discount { get; set; }
        public string Invoice_No { get; set; }
        public DateTime Invoice_Date { get; set; }
        public string Net { get; set; }
        public string Taxes { get; set; }
        public string Store { get; set; }

    }
}
