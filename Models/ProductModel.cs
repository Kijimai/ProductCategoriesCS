#pragma warning disable CS8618
namespace ProductsCategories.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class Product
{
  [Key]
  public int ProductId { get; set; }

  [Required]
  [Display(Name = "Product Name")]
  [MinLength(5, ErrorMessage = "Product name must be longer than 5 characters.")]
  [MaxLength(45, ErrorMessage = "Product name must be less than 45 characters.")]
  public string Name { get; set; }

  [Required]
  [Display(Name = "Product Description")]
  public string Description { get; set; }

  [Required]
  [Range(0, 1000000)]
  [DataType(DataType.Currency)]
  public double Price { get; set; }

  public DateTime createdAt { get; set; } = DateTime.Now;
  public DateTime updatedAt { get; set; } = DateTime.Now;

  public List<Category> Categories { get; set; } = new List<Category>();
}