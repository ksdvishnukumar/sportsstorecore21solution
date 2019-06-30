using System;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SportsStoreCore21WebApp.Models.Entities
{
  public class Product
  {
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int ProductId { get; set; }

    [Display(Name = "Product Name"), StringLength(100)]
    [Required(ErrorMessage = "Product Name cannot be empty")]
    public string ProductName { get; set; }

    [StringLength(250)]
    [Required(ErrorMessage = "Description cannot be empty")]
    public string Description { get; set; }

    [Required(ErrorMessage = "Price cannot be empty")]
    [Column(TypeName = "decimal(18,2)")]
    public decimal Price { get; set; }

    [StringLength(100)]
    [Required(ErrorMessage = "Catgory cannot be empty")]
    public string Category { get; set; }
    public string PhotoUrl { get; set; }
  }
}
