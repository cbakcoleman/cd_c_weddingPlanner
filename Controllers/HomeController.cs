using System;
using System.Linq;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using cd_c_weddingPlanner.Models;

namespace cd_c_weddingPlanner.Controllers
{
    public class HomeController : Controller
    {
        private MyContext _context;
        public HomeController(MyContext context)
        {
            _context = context;
        }

        [HttpGet("")]
        public IActionResult Index()
        {
            return RedirectToAction("LoginRegistration");
        }

        [HttpGet("LoginRegistration")]
        public IActionResult LoginRegistration()
        {
            return View();
        }
    }
}