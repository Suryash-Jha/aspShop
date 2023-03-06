using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using aspShop.Models;
using MySql.Data.MySqlClient;

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
        MySqlConnection conn = new MySqlConnection("server=localhost;user=root;database=shopApp;port=3306;password=Lion@123");
        // MySqlConnection conn = new MySqlConnection("server=localhost;user=root;database=studentData;port=3306;password=Lion@123");

        // MySqlConnection con = new MySqlConnection("server=localhost;user=root;database=shopApp;port=3306;password=Lion@123");
        conn.Open();
        MySqlCommand cmd = new MySqlCommand("SELECT * FROM productDetails;", conn);
        MySqlDataReader dr = cmd.ExecuteReader();
        // Console.WriteLine(dr["productName"]);
        while (dr.Read())
        {
            Console.WriteLine(dr["productDesc"]);
            // Console.WriteLine(dr.GetString(0));
            // Console.WriteLine(dr.GetString(1));
            // Console.WriteLine(dr.GetString(2));
            // Console.WriteLine(dr.GetString(3));
            // Console.WriteLine(dr.GetString(4));
        }
        dr.Close();
        cmd.Dispose();
        conn.Close();
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
        MySqlConnection conn = new MySqlConnection("server=localhost;user=root;database=shopApp;port=3306;password=Lion@123");
        conn.Open();
        if (description != null)
        // description = "No description";
        {
            string query = "INSERT INTO productDetails VALUES (NULL, \"" + name + "\", \"" + category + "\", " + price + ", " + quantity + ", \"" + description + "\")";
            // "INSERT INTO productDetails VALUES (NULL, \"Check\", \"fish\", 300, 7, \"Yo its working\")"
            try
            {
                MySqlCommand cmd = new MySqlCommand(query, conn);
                Console.WriteLine(cmd);
                MySqlDataReader dr = cmd.ExecuteReader();
                Console.WriteLine("Success");
            }
            catch
            {
                Console.WriteLine("Error");
            }
        }
        else
        {
            Console.WriteLine("No description");
        }

        ViewBag.nameVal = name;
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