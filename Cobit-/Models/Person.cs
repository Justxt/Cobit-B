using System.ComponentModel.DataAnnotations;

namespace Cobit.Models
{
    public class Person
    {
        public int PersonID { get; set; }
        [Required]
        [MaxLength(10)]
        public string Cedula { get; set; }
        [Required]
        [MaxLength(50)]
        public string Name { get; set; }
        [Required]
        [MaxLength(50)]
        public string LastName { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        [Phone]
        public string Phone { get; set; }
        [Required]
        [MinLength(8)]
        public string Password { get; set; }
        [Required]
        public string Role { get; set; }
    }
}
