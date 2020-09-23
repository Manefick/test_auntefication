using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace test_auntefication.Controllers
{
    public class AuthController : Controller
    {
        [Authorize]
        public ViewResult Index()
        {
            return View();
        }
    }
}
