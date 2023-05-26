using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MTGCollectionManager.Models.Decks
{
    public class DeckAdd
    { 
        public string Name { get; set; }
        public string Format { get; set; }
    }
}
