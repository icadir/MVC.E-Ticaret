using System.Collections.Generic;

namespace ETicaret.Models.i
{
    public class ProductModels
    {
        public DB.Products Product { get; set; }
        public List<DB.Comments> Comments { get; set; }
    }
}