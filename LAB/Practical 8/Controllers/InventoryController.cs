using System.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using practical_8.Models;

namespace practical_8.Controllers
{
    public class InventoryController : Controller
    {
        private readonly IConfiguration _configuration;

        public InventoryController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        private string ConnectionString =>
            _configuration.GetConnectionString("InventoryConnection")
            ?? throw new InvalidOperationException(
                "InventoryConnection was not found in appsettings.json.");

        // READ: Show all products
        public IActionResult Index()
        {
            ViewBag.Message = TempData["Message"];
            ViewBag.Error = TempData["Error"];

            try
            {
                return View(GetAllProducts());
            }
            catch (SqlException)
            {
                ViewBag.Error =
                    "Database connection failed. Check InventoryDB and the connection string.";

                return View(new List<Product>());
            }
        }

        // Fetch all records with SqlDataAdapter and DataTable
        private List<Product> GetAllProducts()
        {
            List<Product> products = new List<Product>();

            using (SqlConnection con = new SqlConnection(ConnectionString))
            using (SqlCommand cmd = new SqlCommand(
                "SELECT ProductID, ProductName, Category, Quantity, UnitPrice " +
                "FROM Products ORDER BY ProductID", con))
            using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
            {
                DataTable dt = new DataTable();
                adapter.Fill(dt);

                foreach (DataRow row in dt.Rows)
                {
                    products.Add(new Product
                    {
                        ProductID = Convert.ToInt32(row["ProductID"]),
                        ProductName = Convert.ToString(row["ProductName"])!,
                        Category = Convert.ToString(row["Category"])!,
                        Quantity = Convert.ToInt32(row["Quantity"]),
                        UnitPrice = Convert.ToDecimal(row["UnitPrice"])
                    });
                }
            }

            return products;
        }

        // Fetch one product by ID
        private Product? GetProduct(int id)
        {
            using (SqlConnection con = new SqlConnection(ConnectionString))
            using (SqlCommand cmd = new SqlCommand(
                "SELECT ProductID, ProductName, Category, Quantity, UnitPrice " +
                "FROM Products WHERE ProductID = @ProductID", con))
            {
                cmd.Parameters.Add("@ProductID", SqlDbType.Int).Value = id;

                con.Open();

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        return new Product
                        {
                            ProductID = Convert.ToInt32(reader["ProductID"]),
                            ProductName = Convert.ToString(reader["ProductName"])!,
                            Category = Convert.ToString(reader["Category"])!,
                            Quantity = Convert.ToInt32(reader["Quantity"]),
                            UnitPrice = Convert.ToDecimal(reader["UnitPrice"])
                        };
                    }
                }
            }

            return null;
        }

        // Reusable parameters for INSERT and UPDATE
        private void AddProductParameters(SqlCommand cmd, Product product)
        {
            cmd.Parameters.Add("@ProductName", SqlDbType.NVarChar, 100)
                .Value = product.ProductName.Trim();

            cmd.Parameters.Add("@Category", SqlDbType.NVarChar, 50)
                .Value = product.Category.Trim();

            cmd.Parameters.Add("@Quantity", SqlDbType.Int)
                .Value = product.Quantity;

            SqlParameter price = cmd.Parameters.Add(
                "@UnitPrice", SqlDbType.Decimal);

            price.Precision = 10;
            price.Scale = 2;
            price.Value = product.UnitPrice;
        }

        // CREATE: Display form
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        // CREATE: Insert product
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Product product)
        {
            if (!ModelState.IsValid)
                return View(product);

            try
            {
                using (SqlConnection con = new SqlConnection(ConnectionString))
                using (SqlCommand cmd = new SqlCommand(
                    "INSERT INTO Products " +
                    "(ProductName, Category, Quantity, UnitPrice) " +
                    "VALUES (@ProductName, @Category, @Quantity, @UnitPrice)", con))
                {
                    AddProductParameters(cmd, product);

                    con.Open();
                    cmd.ExecuteNonQuery();
                }

                TempData["Message"] = "Product added successfully.";
                return RedirectToAction(nameof(Index));
            }
            catch (SqlException)
            {
                ModelState.AddModelError("",
                    "Unable to add product. Check the database connection.");

                return View(product);
            }
        }

        // UPDATE: Display existing product
        [HttpGet]
        public IActionResult Edit(int? id)
        {
            if (!id.HasValue)
                return BadRequest();

            Product? product = GetProduct(id.Value);

            if (product == null)
                return NotFound();

            return View(product);
        }

        // UPDATE: Save changes
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Product product)
        {
            if (!ModelState.IsValid)
                return View(product);

            try
            {
                using (SqlConnection con = new SqlConnection(ConnectionString))
                using (SqlCommand cmd = new SqlCommand(
                    "UPDATE Products SET ProductName = @ProductName, " +
                    "Category = @Category, Quantity = @Quantity, " +
                    "UnitPrice = @UnitPrice WHERE ProductID = @ProductID", con))
                {
                    AddProductParameters(cmd, product);

                    cmd.Parameters.Add("@ProductID", SqlDbType.Int)
                        .Value = product.ProductID;

                    con.Open();

                    int rows = cmd.ExecuteNonQuery();

                    if (rows == 0)
                        return NotFound();
                }

                TempData["Message"] = "Product updated successfully.";
                return RedirectToAction(nameof(Index));
            }
            catch (SqlException)
            {
                ModelState.AddModelError("",
                    "Unable to update product. Check the database connection.");

                return View(product);
            }
        }

        // DELETE: Confirmation page
        [HttpGet]
        public IActionResult Delete(int? id)
        {
            if (!id.HasValue)
                return BadRequest();

            Product? product = GetProduct(id.Value);

            if (product == null)
                return NotFound();

            return View(product);
        }

        // DELETE: Remove product
        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(ConnectionString))
                using (SqlCommand cmd = new SqlCommand(
                    "DELETE FROM Products WHERE ProductID = @ProductID", con))
                {
                    cmd.Parameters.Add("@ProductID", SqlDbType.Int).Value = id;

                    con.Open();

                    int rows = cmd.ExecuteNonQuery();

                    if (rows == 0)
                        return NotFound();
                }

                TempData["Message"] = "Product deleted successfully.";
            }
            catch (SqlException)
            {
                TempData["Error"] = "Unable to delete product.";
            }

            return RedirectToAction(nameof(Index));
        }
    }
}