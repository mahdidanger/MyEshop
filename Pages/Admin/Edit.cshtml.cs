using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MyEshop.Data;
using MyEshop.Models;

namespace MyEshop.Pages.Admin
{
    public class EditModel : PageModel
    {
        private MyEshopContext _context;
        public EditModel(MyEshopContext context)
        {
            _context = context;
        }
        [BindProperty]
        public AddEditProductViewModel Product1 { get; set; }

        [BindProperty]
        public List<int> selectedGroups { get; set; }
        public List<int> GroupsProduct { get; set; }

        public void OnGet(int id)
        {
            var product = _context.Product
                .Include(p => p.Item)
                .Where(p => p.Id == id)
                .Select(s => new AddEditProductViewModel()
                {
                    Id = s.Id,
                    Name = s.Name,
                    Description = s.Description,
                    QuantityInStock = s.Item.QuantityInStock,
                    Price = s.Item.Price
                }).FirstOrDefault();

            Product1 = product;
            product.Categories = _context.Categories.ToList();
            GroupsProduct = _context.CategoryToProduct.Where(c=> c.ProductId == id)
                .Select(s=> s.CategoryId).ToList();
        }
        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var product = _context.Product.Find(Product1.Id);
            var item = _context.Item.First(i => i.Id == product.ItemId);

            product.Name = Product1.Name;
            product.Description = Product1.Description;
            item.Price = Product1.Price;
            item.QuantityInStock = Product1.QuantityInStock;

            _context.SaveChanges();


            if (Product1.Picture?.Length > 0)
            {
                string filePath = Path.Combine(Directory.GetCurrentDirectory(),
                    "wwwroot",
                    "image",
                    product.Id + Path.GetExtension(Product1.Picture.FileName));
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    Product1.Picture.CopyTo(stream);
                }
            }

            _context.CategoryToProduct.Where(c => c.ProductId == Product1.Id).ToList()
                .ForEach(g => _context.CategoryToProduct.Remove(g));

            if (selectedGroups.Any() && selectedGroups.Count > 0)
            {
                foreach (int gr in selectedGroups)
                {
                    _context.CategoryToProduct.Add(new CategoryToProduct()
                    {
                        CategoryId = gr,
                        ProductId = Product1.Id
                    });
                }
                _context.SaveChanges();
            }


            return RedirectToPage("Index");
        }
    }
}
