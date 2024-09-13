﻿using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyEshop.Models
{
    public class AddEditProductViewModel
    {
        public int Id{ get; set; }
        public string Name{ get; set; }
        public string Description{ get; set; }
        public Decimal Price{ get; set; }
        public int QuantityInStock{ get; set; }
        public IFormFile Picture { get; set; }
        public List<Category> Categories { get; set; } 

    }
}
