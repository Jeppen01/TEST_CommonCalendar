using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using TEST_CommonCalendar.CODE.COMMON;
using TEST_CommonCalendar.Models;

namespace TEST_CommonCalendar.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            mEVENT myrecord = new mEVENT();

            myrecord.l_HourFrom = COMMON_FUNCTIONS.getMinutes();
            myrecord.l_HourTo = COMMON_FUNCTIONS.getMinutes();

            return View(myrecord);
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
