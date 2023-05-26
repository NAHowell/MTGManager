using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MTGCollectionManager.Models.Decks
{
    public class UpdateDeck
    {
    
        public int Id { get; set; }

        public string Name { get; set; }

        public string Format { get; set; }
    }
}
