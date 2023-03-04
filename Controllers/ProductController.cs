using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using aspShop.Models;

namespace aspShop.Controllers;

public class ProductController : Controller
{
    private readonly ILogger<ProductController> _logger;

    public ProductController(ILogger<ProductController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index()
    {
        return View();
    }

    public IActionResult ShowProd(string id)
    {
        Console.WriteLine(id + id);
        return View();
    }

    public IActionResult AddProd([FromForm] Product product)
    {
        string name = product.Name;
        string price = product.Price;
        string quantity = product.Quantity;
        string description = product.Description;
        string category = product.Category;
        ViewBag.nameVal= name;
        if (name == "f")
            return RedirectToAction("ShowProd", "Product", new { id = name });
        // return ShowProd(name);
        // Console.WriteLine(name);
        // Console.WriteLine(price);


        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}