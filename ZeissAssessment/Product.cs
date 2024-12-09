using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Build.Framework;

namespace ZeissAssessment
{
    public class Product
    {
        public int Id { get; set; } // Auto-generated 6-digit ID
        [Required]
        public string Name { get; set; }
        [Required]
        public int AvailableQuantity { get; set; }
        public decimal Price { get; set; } // Example of another field
    }
}
