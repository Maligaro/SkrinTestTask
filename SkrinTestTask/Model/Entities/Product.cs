using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SkrinTestTask.Model.Entities
{
    public class Product
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [Unicode(false)]
        [MaxLength(100)]
        public string Name { get; set; } = null!;

        [Required]
        [Column(TypeName = "money")]
        public decimal Price { get; set; }

        public byte[]? Picture { get; set; }

        public ICollection<OrderItem> OrderItems { get; set;}
    }
}