using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MTGCollectionManager.Models.WishList
{
    public class ViewWishList
    {
        public int id {  get; set; }
        public string CardName { get; set; }

        public int Quantity { get; set; }
    }
}
