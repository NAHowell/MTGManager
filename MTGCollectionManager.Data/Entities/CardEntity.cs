using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MTGCollectionManager.Data.Entities
{
    public class CardEntity
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("AspNetUsers")]
        public string OwnerId { get; set; }

        [Required]
        public string CardName { get; set; }

        [Required]
        public int Quantity { get; set; }
    }
}
