using System.ComponentModel.DataAnnotations;

namespace WebApi_BL.DTOs
{
    public class CreateGoodDto
    {
        [Required]
        public string Title { get; set; }
        [Required]
        public double Price { get; set; }
    }
}
