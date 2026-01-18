
using Microsoft.AspNetCore.Mvc;
using RegionalTempleInfo.Data;
using RegionalTempleInfo.Models;
using Microsoft.EntityFrameworkCore;
using FuzzySharp;
using FuzzySharp.SimilarityRatio;
using System.Collections.Generic;
using System.Linq;

namespace RegionalTempleInfo.Controllers
{
    public class TempleinfoController : Controller
    {
        private readonly ApplicationDbContext _db;

        public TempleinfoController(ApplicationDbContext db)
        {
            _db = db;
        }

        public IActionResult event_festivals()
        {
            
            return View();
        }
        public IActionResult find_temple(string godName, string location, string searchTerm)
        {
            IQueryable<Information> query = _db.information;

            
            if (!string.IsNullOrEmpty(godName))
            {
                var fuzzyGodNames = FuzzyMatchGodName(godName);
                query = query.Where(t => fuzzyGodNames.Contains(t.Name_of_god));
            }

            
            if (!string.IsNullOrEmpty(location))
            {
                var fuzzylocation = FuzzyMatchlocation(location);
                query = query.Where(t => fuzzylocation.Contains(t.location));
            }

            if (!string.IsNullOrEmpty(searchTerm))
            {
                query = query.Where(t => t.Name_of_god.Contains(searchTerm) ||
                                         t.name_of_mandir.Contains(searchTerm) ||
                                         t.location.Contains(searchTerm));
            }

            var model = query.ToList();
            return View(model);
        }

        private List<string> FuzzyMatchGodName(string inputGodName)
        {
            
            var knownGodNames = _db.information.Select(t => t.Name_of_god).ToList();

           
            var matches = Process.ExtractTop(inputGodName, knownGodNames, limit: 5, scorer: FuzzySharp.Scorer.LevenshteinRatio);
            var matchedGodNames = matches.Where(m => m.Score >= 50).Select(m => m.Value).ToList();

            return matchedGodNames;
        }
        private List<string> FuzzyMatchlocation(string inputlocation)
        {

            var knownlocation = _db.information.Select(t => t.location).ToList();
            var matches = Process.ExtractTop(inputlocation, knownlocation, limit: 5, scorer: FuzzySharp.Scorer.LevenshteinRatio);
            var matchedlocation = matches.Where(m => m.Score >= 50).Select(m => m.Value).ToList();

            return matchedlocation;
        }
        public IActionResult admin_page()
        {
            var model = _db.information.ToList();
            return View(model);
        }

        [HttpPost]
        public IActionResult AddInformation(Information information)
        {
            if (ModelState.IsValid)
            {
                _db.information.Add(information);
                _db.SaveChanges();
                TempData["success"] = "Information added successfully";
            }
            else
            {
                TempData["failed"] = "Failed to add information";
            }

            return RedirectToAction("admin_page");
        }

        [HttpPost]
        public IActionResult DeleteInformation(int id)
        {
            var information = _db.information.Find(id);
            if (information != null)
            {
                _db.information.Remove(information);
                _db.SaveChanges();
            }
            return RedirectToAction("admin_page");
        }

        public IActionResult admin_login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult admin_login(string username, string password)
        {
            
            string adminUsername = "admin";
            string adminPassword = "Admin@123";

           
            if (username == adminUsername && password == adminPassword)
            {
                TempData["success"] = "Login successful";
                return RedirectToAction("admin_page"); 
            }
            else
            {
                TempData["failed"] = "Invalid username or password";
                return View(); 
            }
        }

        public IActionResult homepage()
        {
            return View();
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Index(Registration reg)
        {
            _db.registrations.Add(reg);
            _db.SaveChanges();

            return RedirectToAction("login");
        }

        public IActionResult login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult login(string email, string password)
        {
            var data = _db.registrations.FirstOrDefault(x => x.email == email && x.password == password);
            if (data != null)
            {
                TempData["success"] = "Login successfully";
                return RedirectToAction("find_temple");
            }
            else
            {
                TempData["failed"] = "Login failed";
                return RedirectToAction("Index");
            }
        }
    }
}
