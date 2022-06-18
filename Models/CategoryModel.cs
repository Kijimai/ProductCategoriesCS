#pragma warning disable CS8618

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class Category
{
  [Key]
  public int CategoryId { get; set; }

  [Required]
  [MinLength(2, ErrorMessage = "Category name must be at least 2 characters long.")]
  public string Name { get; set; }

  public DateTime createdAt { get; set; } = DateTime.Now;
  public DateTime updatedAt { get; set; } = DateTime.Now;

  public List<Product> Products { get; set; } = new List<Product>();
}