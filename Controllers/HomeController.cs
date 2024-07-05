using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Net;
using WebApplication1.Context;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class HomeController : Controller
    {
        private readonly DBContext db;

        public HomeController(DBContext db)
        {
            this.db = db;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            //var x = from text in db.contents
            //        join user in db.users
            //        on text.userId equals user.id

            //        select text;
            var content = db.contents
            .Include(u => u.user)
            .Include(ct => ct.Categories)
            .Include(c => c.comments)
            .ToList();
            content.ForEach(x => x.content = WebUtility.HtmlDecode(x.content));
            return View(Tuple.Create(content, db.categories.ToList()));
        }

        [HttpGet]
        [HttpPost]
        public async Task<IActionResult> Detail(int id)
        {
            // POST isteğine yanıt vermek için
            if (HttpContext.Request.Method == "POST")
            {
                var form = HttpContext.Request.Form;
                db.comments.Add(new Comment
                {
                    contentId = id,
                    name = form["name"],
                    surname = form["surname"],
                    mail = form["email"],
                    title = form["title"],
                    comment = form["message"],
                    date = DateTime.Now,
                    status = 0
                });
                db.SaveChanges();
            }
            var content = db.contents
            .Include(u => u.user)
            .Include(ct => ct.Categories)
            .Include(c => c.comments).Where(x => x.id == id)
            .FirstOrDefault();
            return View(Tuple.Create(content,db.categories.ToList(),db.contents.ToList()));
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
