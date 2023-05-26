using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MTGCollectionManager.Data.Entities
{
    public class DeckEntity
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("AspNetUsers")]
        public string OwnerId { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Format { get; set; }

    }
}
