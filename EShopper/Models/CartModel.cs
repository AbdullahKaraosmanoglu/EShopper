using System.ComponentModel.DataAnnotations;

namespace EShopper.Models
{
    public class CartModel
    {
        [Key]
        public int UserId { get; set; }
        public int ProductId { get; set; }
        public string ImagePath { get; set; }
    }
}