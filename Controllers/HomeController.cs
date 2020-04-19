using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using DemoGallery.Models;
using DemoGallery.Models.Services.Application;
using DemoGallery.Models.ViewModels;
using DemoGallery.Models.InputModels;

namespace DemoGallery.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return RedirectToAction("Index", "Album");
        }
    }
}
