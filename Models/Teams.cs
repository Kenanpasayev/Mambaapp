using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication11.Models
{
    public class Teams
    {
        public int Id { get; set; }
        [Required]
        [MinLength(5)]
        [MaxLength(50)]
        public string Name { get; set; }
        [Required]
        [MinLength(5)]
        [MaxLength(50)]
        public string? Position { get; set; }
        public string? ImgUrl { get; set; }
        [NotMapped]
        public IFormFile ImgFile { get; set; }
    }
}
