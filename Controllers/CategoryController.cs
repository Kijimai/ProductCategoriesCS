using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using ProductsCategories.Models;

public class CategoryController : Controller
{
  private ProductCategoryContext _context;

  public CategoryController(ProductCategoryContext context)
  {
    _context = context;
  }

  [HttpGet("categories")]
  public IActionResult ShowCategories()
  {
    List<Category> ListOfCategories = _context.Categories.ToList();
    ViewBag.ListOfCategories = ListOfCategories;
    return View("Category");
  }

  [HttpPost("categories/create")]
  public IActionResult CreateCategory(Category newCategory)
  {
    if (ModelState.IsValid)
    {
      _context.Categories.Add(newCategory);
      _context.SaveChanges();
      return RedirectToAction("ShowCategories");
    }
    return ShowCategories();
  }

}