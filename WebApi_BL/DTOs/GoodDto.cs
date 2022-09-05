using System;
using System.ComponentModel.DataAnnotations;

namespace WebApi_BL.DTOs
{
    public class GoodDto
    {
        public Guid Id { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public double Price { get; set; }
        [Required]
        public DateTime CreatedAt { get; set; }
    }
}
