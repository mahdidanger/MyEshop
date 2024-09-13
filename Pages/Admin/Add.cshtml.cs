using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MyEshop.Data;
using MyEshop.Models;

namespace MyEshop.Pages.Admin
{
    public class AddModel : PageModel
    {
        private MyEshopContext _context;
        public AddModel(MyEshopContext context)
        {
            _context = context;
        }

        [BindProperty]
        public AddEditProductViewModel Products { get; set; }

        [BindProperty]
        public List<int> selectedGroups { get; set; }
        public void OnGet()
        {
            Products = new AddEditProductViewModel()
            {
                Categories = _context.Categories.ToList() 
            };
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var item = new Item()
            {
                Price = Products.Price,
                QuantityInStock = Products.QuantityInStock
            };
            _context.Add(item);
            _context.SaveChanges();
            var pro = new Product()
            {
                Name = Products.Name,
                Description = Products.Description,
                Item = item
            };
            _context.Add(pro);
            _context.SaveChanges();
            pro.ItemId = pro.Id;

            _context.SaveChanges();

            if (Products.Picture?.Length > 0)
            {
                string filePath = Path.Combine(Directory.GetCurrentDirectory(),
                    "wwwroot",
                    "image",
                    pro.Id + Path.GetExtension(Products.Picture.FileName));
                using (var stream = new FileStream(filePath,FileMode.Create))
                {
                    Products.Picture.CopyTo(stream);
                }
            }

            if (selectedGroups.Any() && selectedGroups.Count > 0)
            {
                foreach (int gr in selectedGroups)
                {
                    _context.CategoryToProduct.Add(new CategoryToProduct()
                    {
                        CategoryId = gr,
                        ProductId = pro.Id
                    }) ;
                }
                _context.SaveChanges();
            }



            return RedirectToPage("Index");
        }
    }
}
