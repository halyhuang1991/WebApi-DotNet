using System.ComponentModel.DataAnnotations;

namespace CSharp.Models
{
    public class product
    {
         public int Id { get; set; }

        [Required]
        public string Name { get; set; }
        public decimal Price { get; set; }
    }
}