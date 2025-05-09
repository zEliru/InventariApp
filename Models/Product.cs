using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace InventariApp.Models
{
    public class Product
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }
        [Required]
        [StringLength(100)]
        public string Description { get; set; }
        [Required]
        [Range(0, int.MaxValue, ErrorMessage = "Quantity can't be negative")]
        public double Quantity { get; set; }

        [DisplayName("Price (€)")]
        [Required]
        [Range(0, int.MaxValue, ErrorMessage = "Price can't be negative")]
        public double Price { get; set; }


        public override string ToString()
        {
            return $"{Id} - {Name}";
        }
    }
}
