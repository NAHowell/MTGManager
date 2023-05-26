using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MTGCollectionManager.Models.Cards
{
    public class CardAdd
    {
        public string CardName { get; set; }
        public int Quantity { get; set; }
    }
}
