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


    // to store data in Datatable




            // using MySql.Data.MySqlClient;
            // using System.Data;

            // public DataTable ExecuteQuery(string connectionString, string query)
            // {
            //     DataTable dataTable = new DataTable();

            //     using (MySqlConnection connection = new MySqlConnection(connectionString))
            //     {
            //         connection.Open();

            //         using (MySqlCommand command = new MySqlCommand(query, connection))
            //         {
            //             using (MySqlDataReader dataReader = command.ExecuteReader())
            //             {
            //                 dataTable.Load(dataReader);
            //             }
            //         }

            //         connection.Close();
            //     }

            //     return dataTable;
            // }



    // END


    // To Read data out of Datatable

            //         DataTable dataTable = ExecuteQuery(connevtionString, "SELECT * FROM productDetails;");

            // foreach (DataRow row in dataTable.Rows)
            // {
            //     // Iterate through each column in the current row
            //     foreach (DataColumn col in dataTable.Columns)
            //     {
            //         // Access the value of the current cell
            //         object cellValue = row[col];

            //         // Do something with the cell value
            //         Console.WriteLine($"{col.ColumnName}: {cellValue}");
            //     }
            // }

    // END



    // To use parametrized sql commands 

    using System.Data.SqlClient;

// ...

string connectionString = "your_connection_string_here";

// Define the query with parameter placeholders
string query = "SELECT * FROM users WHERE username=@username AND password=@password";

// Create a SqlConnection object
        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            // Create a SqlCommand object with the parameterized query
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                // Add SqlParameter objects for each parameter in the query
                command.Parameters.AddWithValue("@username", username);
                command.Parameters.AddWithValue("@password", password);
                
                // Open the database connection
                connection.Open();
                
                // Execute the query and retrieve the results
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    // Check if any rows were returned
                    if (reader.HasRows)
                    {
                        // Login successful
                    }
                    else
                    {
                        // Login failed
                    }
                }
            }
        }

//END
    public MySqlDataReader resultOfCall()
    {
        // MySqlConnection conn = new MySqlConnection("server=localhost;user=root;database=studentData;port=3306;password=Lion@123");

        // MySqlConnection con = new MySqlConnection("server=localhost;user=root;database=shopApp;port=3306;password=Lion@123");
        MySqlConnection conn = new MySqlConnection("server=localhost;user=root;database=shopApp;port=3306;password=Lion@123");
        conn.Open();
        MySqlCommand cmd = new MySqlCommand("SELECT * FROM productDetails;", conn);
        MySqlDataReader dr = cmd.ExecuteReader();
        // Console.WriteLine(dr["productName"]);
        var storage = dr;
        // while (dr.Read())
        // {
        //     x.Add(dr);
        //     Console.WriteLine(dr["productDesc"]);
        //     // Console.WriteLine(dr.GetString(0));
        //     // Console.WriteLine(dr.GetString(1));
        //     // Console.WriteLine(dr.GetString(2));
        //     // Console.WriteLine(dr.GetString(3));
        //     // Console.WriteLine(dr.GetString(4));
        // }
        dr.Close();
        cmd.Dispose();
        conn.Close();
        return storage;

    }
    public IActionResult Index()
    {
        MySqlConnection conn = new MySqlConnection("server=localhost;user=root;database=shopApp;port=3306;password=Lion@123");

        var res = resultOfCall(conn);
        while (res.Read())
        {
            Console.WriteLine(res["productDesc"]);
            // Console.WriteLine(dr.GetString(0));
            // Console.WriteLine(dr.GetString(1));
            // Console.WriteLine(dr.GetString(2));
            // Console.WriteLine(dr.GetString(3));
            // Console.WriteLine(dr.GetString(4));
        }
        // MySqlConnection conn = new MySqlConnection("server=localhost;user=root;database=shopApp;port=3306;password=Lion@123");
        // // MySqlConnection conn = new MySqlConnection("server=localhost;user=root;database=studentData;port=3306;password=Lion@123");

        // // MySqlConnection con = new MySqlConnection("server=localhost;user=root;database=shopApp;port=3306;password=Lion@123");
        // conn.Open();
        // MySqlCommand cmd = new MySqlCommand("SELECT * FROM productDetails;", conn);
        // MySqlDataReader dr = cmd.ExecuteReader();
        // // Console.WriteLine(dr["productName"]);

        // while (dr.Read())
        // {
        //     Console.WriteLine(dr["productDesc"]);
        //     // Console.WriteLine(dr.GetString(0));
        //     // Console.WriteLine(dr.GetString(1));
        //     // Console.WriteLine(dr.GetString(2));
        //     // Console.WriteLine(dr.GetString(3));
        //     // Console.WriteLine(dr.GetString(4));
        // }
        // dr.Close();
        // cmd.Dispose();
        // conn.Close();
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