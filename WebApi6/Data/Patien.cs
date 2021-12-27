using System.ComponentModel.DataAnnotations;

namespace WebApi6.Data
{
    public class Patien
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public string Email { get; set; }

        public DateTime CreatedDate { get; set; } = DateTime.Now;
        
    }
}
