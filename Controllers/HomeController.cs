using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using MyEshop.Data;
using MyEshop.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace MyEshop.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        private MyEshopContext _context;


        public HomeController(ILogger<HomeController> logger , MyEshopContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
        {
            var products = _context.Product.ToList();
            return View(products);
        }

        public IActionResult Detail(int id)
        {
            var product = _context.Product
                .Include(p => p.Item)
                .SingleOrDefault(p => p.Id == id);
            if (product == null)
            {
                return NotFound();
            }

            var categories = _context.Product
                .Where(p => p.Id == id)
                .SelectMany(c => c.CategoryToProducts)
                .Select(ca => ca.Category)
                .ToList();

            var vm = new DetailsViewModel()
            {
                Product = product,
                Categories = categories
            };

            return View(vm);
        }

        [Authorize]
        public IActionResult AddToCart(int itemId)
        {
            var product = _context.Product.Include(p => p.Item)
                .SingleOrDefault(p => p.ItemId == itemId);
            if (product!= null)
            {
                int userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier).ToString());
                var order = _context.Order.FirstOrDefault(o => o.UserId == userId && !o.IsFinally);
                if (order != null)
                {
                    var orderDetail = _context.OrderDetail
                        .FirstOrDefault(d => d.OrderId == order.OrderId && d.ProductId == product.Id);
                    if (orderDetail != null)
                    {
                        orderDetail.Count += 1;
                    }
                    else
                    {
                        _context.OrderDetail.Add(new OrderDetail()
                        {
                            OrderId = order.OrderId,
                            ProductId = product.Id,
                            Price = product.Item.Price,
                            Count = 1

                        });
                    }
                }
                else
                {
                    order = new Order()
                    {
                        IsFinally = false,
                        CreateDate = DateTime.Now,
                        UserId = userId
                    };
                    _context.Order.Add(order);
                    _context.SaveChanges();
                    _context.OrderDetail.Add(new OrderDetail()
                    { 
                       OrderId = order.OrderId,
                       ProductId = product.Id,
                       Price = product.Item.Price,
                       Count = 1

                    });
                }
                _context.SaveChanges();

            }
            return RedirectToAction("ShowCart");
        }
        [Authorize]
        public IActionResult ShowCart()
        {
            int userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier).ToString());
            var order = _context.Order.Where(o => o.UserId == userId && !o.IsFinally)
                .Include(o => o.OrderDetails)
                .ThenInclude(o => o.Product)
                .FirstOrDefault();
            return View(order);
        }

        public IActionResult RemoveCart(int detailId)
        {
            var orderDetail = _context.OrderDetail.Find(detailId);
            if (orderDetail.Count <= 1)
            {
                _context.Remove(orderDetail);
            }
            else
            {
                 orderDetail.Count -=1;
   
            }
            _context.SaveChanges();
            return RedirectToAction("ShowCart");
        }

        [Route("ContactUs")]
        public IActionResult ContactUs()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
