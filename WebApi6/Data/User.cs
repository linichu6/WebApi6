using System.ComponentModel.DataAnnotations;

namespace WebApi6.Data
{
    public class User
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Username { get; set; }
        [Required]
        public string Password { get; set; }

    }
}
