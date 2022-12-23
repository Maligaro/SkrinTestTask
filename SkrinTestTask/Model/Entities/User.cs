using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace SkrinTestTask.Model.Entities
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        ICollection<Order> Orders { get; set; }

        [Required]
        [Unicode(false)]
        [MaxLength(20)]
        public string PhoneNumber { get; set; } = null!;

        [Required]
        [Unicode(false)]
        [MaxLength(100)]
        public string Email { get; set; } = null!;

        [Required]
        [Unicode(false)]
        [MaxLength(64)]
        [MinLength(64)]
        public string HashedPassword { get; set; } = null!;

        [Required]
        [Unicode(false)]
        [MaxLength(20)]
        [MinLength(20)]
        public string PasswordSalt { get; set; } = null!;

        [Required]
        [Unicode(false)]
        [MaxLength(100)]
        public string Name { get; set; } = null!;
    }
}
