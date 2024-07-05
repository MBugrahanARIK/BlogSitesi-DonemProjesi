using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using WebApplication1.Context;
using WebApplication1.Models;
using Microsoft.EntityFrameworkCore;

namespace WebApplication1.Controllers
{
    public class SearchController : Controller
    {
        private readonly DBContext db;

        public SearchController(DBContext db)
        {
            this.db = db;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            string param1 = HttpContext.Request.Query["q"];
            var searchedContent = db.contents
                .Include(u => u.user)
                .Include(c => c.comments)
                .Where(x => x.title.Contains(param1) || x.content.Contains(param1)).ToList();
            return View(Tuple.Create(searchedContent, db.contents.ToList(), db.categories.ToList()));
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
