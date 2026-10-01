using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using lab_6.Models;

namespace lab_6.Controllers
{
    public class ProductsController : Controller
    {
        private static readonly List<Product> products = new List<Product>
        {
            new Product
            {
                ProductId = 101,
                Name = "HP Pavilion 15",
                Description = "15.6-inch laptop with Intel Core i5 processor, 16GB RAM and 512GB SSD.",
                Price = 64999,
                Category = "Laptop"
            },

            new Product
            {
                ProductId = 201,
                Name = "Samsung Galaxy S24",
                Description = "Premium smartphone with AMOLED display, advanced camera and 5G connectivity.",
                Price = 74999,
                Category = "Mobile"
            },

            new Product
            {
                ProductId = 301,
                Name = "Sony WH-1000XM5",
                Description = "Wireless noise-cancelling headphones with premium sound and long battery life.",
                Price = 29999,
                Category = "Headphones"
            }
        };

        // Display all products
        public IActionResult Index()
        {
            return View(products);
        }

        // Display selected product
        public IActionResult Details(int id)
        {
            Product? product = products.FirstOrDefault(
                p => p.ProductId == id);

            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // Show Add Product form
        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        // Add new product
        [HttpPost]
        public IActionResult Add(Product product)
        {
            if (string.IsNullOrWhiteSpace(product.Name) ||
                string.IsNullOrWhiteSpace(product.Description) ||
                string.IsNullOrWhiteSpace(product.Category) ||
                product.Price <= 0)
            {
                ViewBag.Message = "Please enter valid product details.";
                return View(product);
            }

            product.ProductId = products.Max(p => p.ProductId) + 1;
            products.Add(product);

            return RedirectToAction("Index");
        }
    }
}