using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SkrinTestTask.Model.Entities
{
    [PrimaryKey("OrderId", "ProductId")]
    public class OrderItem
    {
        [ForeignKey("OrderId")]
        public int OrderId { get; set; }
        public Order Order{ get; set; } = null!;

        [ForeignKey("ProductId")]
        public int ProductId { get; set; }
        public Product Product { get; set; } = null!;

        [Required]
        public int Amount { get; set; }
    }
}