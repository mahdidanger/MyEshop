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
    public class DeleteModel : PageModel
    {
        private MyEshopContext _context;
        public DeleteModel(MyEshopContext context)
        {
            _context = context;
        }


        [BindProperty]
        public Product Product { get; set; }

        public void OnGet(int id)
        {
            Product = _context.Product.FirstOrDefault(p => p.Id == id);
        }

        public IActionResult OnPost()
        {


            var product = _context.Product.Find(Product.Id);
            var item = _context.Item.First(i => i.Id == product.ItemId);

            _context.Item.Remove(item);
            _context.Product.Remove(product);

            _context.SaveChanges();



            string filePath = Path.Combine(Directory.GetCurrentDirectory(),
                "wwwroot",
                "image",
                product.Id + ".jpg");
            if (System.IO.File.Exists(filePath))
            {
                System.IO.File.Delete(filePath);
            }




            return RedirectToPage("Index");
        }
    }
}
