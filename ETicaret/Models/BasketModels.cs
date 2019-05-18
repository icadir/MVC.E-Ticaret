using System.Collections.Generic;

namespace ETicaret.Models
{
    public class BasketModels
    {
        public Dictionary<int, int> Products { get; set; }

        public BasketModels()
        {
            this.Products = new Dictionary<int, int>();
        }
    }
}