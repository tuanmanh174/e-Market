using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class Product
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public float? Price { get; set; }
        public string Content { get; set; }
        public string LinkImage1 { get; set; }
        public string LinkImage2 { get; set; }
        public string LinkImage3 { get; set; }
    }
}
