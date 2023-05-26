using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MTGCollectionManager.Models.WishList
{
    public class UpdateWishList
    {
        public int Id { get; set; }

        public int Quantity { get; set; }

        public string CardName { get; set; }

        
    }
}
