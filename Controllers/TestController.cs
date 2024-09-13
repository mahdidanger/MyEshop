using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyEshop.Controllers
{
    public class TestController : Controller
    {
        [Authorize]
        public string test1()
        {
            return "test 1";
        }
        //[AllowAnonymous]
        public string test2()
        {
            return "test 2";
        }
    }
}
