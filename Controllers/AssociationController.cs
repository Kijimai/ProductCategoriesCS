using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using ProductsCategories.Models;

public class AssociationController : Controller
{
  private ProductCategoryContext _context;

  public AssociationController(ProductCategoryContext context)
  {
    _context = context;
  }

  [HttpPost("association/category")]
  public IActionResult AssociateProduct()
  {
    return View("Category");
  }
}