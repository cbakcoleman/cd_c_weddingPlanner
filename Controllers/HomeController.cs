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

        [HttpPost("Register")]
        public IActionResult Register(User usertoreg)
        {
            if(ModelState.IsValid)
            {
                if(_context.Users.Any(u => u.Email == usertoreg.Email))
                {
                    ModelState.AddModelError("Email", "The email address you entered is already in use.");

                    return View("LoginRegistration");
                }

                PasswordHasher<User> CBHash = new PasswordHasher<User>();
                usertoreg.Password = CBHash.HashPassword(usertoreg, usertoreg.Password);

                
                _context.Add(usertoreg);
                _context.SaveChanges();

                HttpContext.Session.SetInt32("loggedinuser", usertoreg.UserId);

                // MAYBE CHANGE THIS BACK TO LOGINREGISTRATION IF CAN NOT MAKE WORK

                return RedirectToAction("Dashboard");
            }
            else
            {
                return View("LoginRegistration");
            }
        }

        [HttpPost("Login")]
        public IActionResult Login(LoginUser usertolog)
        {
            if(ModelState.IsValid)
            {
                var userinDb = _context.Users.FirstOrDefault(u => u.Email == usertolog.LoginEmail);

                if(userinDb == null)
                {
                    ModelState.AddModelError("LoginEmail", "Invalid Email/Password.");
                    return View("LoginRegistration");
                }
                var CBHash= new PasswordHasher<LoginUser>();

                var result = CBHash.VerifyHashedPassword(usertolog, userinDb.Password, usertolog.LoginPassword);

                if(result == 0)
                {
                    ModelState.AddModelError("LoginEmail", "Invalid Email/Password.");

                    return View("LoginRegistration");
                }

                HttpContext.Session.SetInt32("loggedinuser", userinDb.UserId);

                return RedirectToAction("Dashboard");
            }
            else
            {
                return View("LoginRegistration");
            }
        }

        [HttpGet("Logout")]
        public RedirectToActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("LoginRegistration");
        }

        [HttpGet("Dashboard")]
        public IActionResult Dashboard()
        {
            int? LoggedinuserId = HttpContext.Session.GetInt32("loggedinuser");
            if(LoggedinuserId == null)
            {
                return RedirectToAction("LoginRegistration");
            }

            List<Wedding> AllWeddings = _context.Weddings
                .Include(w => w.Guests)
                .ToList();

            DashboardView ToDisplay = new DashboardView(AllWeddings, (int)LoggedinuserId);
            System.Console.WriteLine(AllWeddings);

            return View(ToDisplay);
        }

        [HttpGet("wedding/add")]
        public IActionResult NewWedding()
        {
            int? LoggedinuserId = HttpContext.Session.GetInt32("loggedinuser");
            if(LoggedinuserId == null)
            {
                return RedirectToAction("LoginRegistration");
            }

            return View();
        }

        [HttpPost("submitWedding/add")]
        public IActionResult CreateWedding(Wedding newWedding)
        {
            int? LoggedinuserId = HttpContext.Session.GetInt32("loggedinuser");
            if(LoggedinuserId == null)
            {
                return RedirectToAction("LoginRegistration");
            }

            if(ModelState.IsValid)
            {
                newWedding.UserId = (int) LoggedinuserId;

                _context.Add(newWedding);
                _context.SaveChanges();

                return RedirectToAction("ThisWedding", newWedding);
            }
            else
            {
                return View("NewWedding");
            }
        }

        [HttpGet("wedding/{weddingId}/attend")]
        public RedirectToActionResult Attend(int weddingId)
        {
            int? LoggedinuserId = HttpContext.Session.GetInt32("loggedinuser");
            if(LoggedinuserId == null)
            {
                return RedirectToAction("LoginRegistration");
            }
            
            Guest NewRsvp = new Guest()
            {
                WeddingId = weddingId,
                UserId = (int)LoggedinuserId
            };
            _context.Add(NewRsvp);
            _context.SaveChanges();

            return RedirectToAction("Dashboard");
        }

        [HttpGet("wedding/{weddingId}/decline")]
        public RedirectToActionResult Decline(int weddingId)
        {
            int? LoggedinuserId = HttpContext.Session.GetInt32("loggedinuser");
            if(LoggedinuserId == null)
            {
                return RedirectToAction("LoginRegistration");
            }
            
            Guest UnRsvp = _context.Guests.FirstOrDefault(g => g.WeddingId == weddingId && g.UserId == (int)LoggedinuserId);

            _context.Remove(UnRsvp);
            _context.SaveChanges();
            return RedirectToAction("Dashboard");
        }

        [HttpGet("wedding/{weddingId}/delete")]
        public RedirectToActionResult CallItOff(int weddingId)
        {
            int? LoggedinuserId = HttpContext.Session.GetInt32("loggedinuser");
            if(LoggedinuserId == null)
            {
                return RedirectToAction("LoginRegistration");
            }

            Wedding toCallOff = _context.Weddings.FirstOrDefault(w => w.WeddingId == weddingId && w.UserId == (int)LoggedinuserId);

            _context.Remove(toCallOff);
            _context.SaveChanges();
            return RedirectToAction("Dashboard");
        }
        [HttpGet("wedding/{weddingId}")]
        public IActionResult ThisWedding(int weddingId)
        {
            int? LoggedinuserId = HttpContext.Session.GetInt32("loggedinuser");
            if(LoggedinuserId == null)
            {
                return RedirectToAction("LoginRegistration");
            }

            Wedding Awedding = _context.Weddings
                .Include(w => w.Guests)
                .ThenInclude(g => g.User)
                .FirstOrDefault(w => w.WeddingId == weddingId);

            System.Console.WriteLine();

            WeddingView ToDisplay = new WeddingView(Awedding, (int)LoggedinuserId);
        
            return View(ToDisplay);
        }

    }
}