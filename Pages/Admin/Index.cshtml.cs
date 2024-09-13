using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MyEshop.Data;
using MyEshop.Models;

namespace MyEshop.Pages.Admin
{
    public class IndexModel : PageModel
    {
        private MyEshopContext _context;
        public IndexModel(MyEshopContext context)
        {
            _context = context;
        }
        public IEnumerable<Product> Product1 { get; set; }
        public void OnGet()
        {
            Product1 = _context.Product.Include(p => p.Item);
        }
        public void OnPost()
        {

        }
    }
}
