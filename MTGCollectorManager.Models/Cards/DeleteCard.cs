using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MTGCollectionManager.Models.Cards
{
    public class DeleteCard
    {
        public int Id { get; set; }
        public string CardName { get; set; }
        public int Quantity { get; set; }
    }
}
