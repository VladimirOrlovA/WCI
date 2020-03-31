using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WCI.DAL
{
    public class Currency
    {
        public string Title { get; set; }
        public string Link { get; set; }

        public List<Item> items = new List<Item>();
    }

    public class Item
    {
        public string Title { get; set; }
        public DateTime PubDate { get; set; }
        public double Description { get; set; }
        public int Quant { get; set; }
        public string Index { get; set; }
        public string Change { get; set; }
    }
}
