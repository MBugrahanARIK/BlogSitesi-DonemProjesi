using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using System.Collections;
using System.Diagnostics;
using System.Linq;
using WebApplication1.Context;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class AdminController : Controller
    {
        private readonly DBContext db;

        public AdminController(DBContext db)
        {
            this.db = db;
        }
        [HttpGet]
        [HttpPost]
        public IActionResult Index()
        {
            if (HttpContext.Request.Method == "POST")
            {
                var form = HttpContext.Request.Form;
                if (form["username"] != "" && form["password"] != "")
                {
                    db.user = db.users.FirstOrDefault(x => x.username == form["username"].ToString() && x.password == form["password"].ToString());
                    if (db.user != null)
                    {
                        HttpContext.Session.SetString("userId", db.user.id.ToString());
                        return RedirectToAction("Dashboard");
                    }
                    else { return View(02); }
                }
                else { return View(01); }
            }
            return View(00);
        }
        public IActionResult Dashboard()
        {
            if (HttpContext.Session.GetString("userId") != null)
            {
                return View();
            }
            else
                return RedirectToAction("Index");
        }
        public IActionResult AboutMe()
        {
            if (HttpContext.Session.GetString("userId") != null)
                return View();
            else
                return RedirectToAction("Index");
        }

        [HttpGet]
        [HttpPost]
        public IActionResult NewContent()
        {
            if (HttpContext.Session.GetString("userId") != null)
            {
                // POST isteğine yanıt vermek için
                if (HttpContext.Request.Method == "POST")
                {
                    var form = HttpContext.Request.Form;
                    Content content = new Content();
                    content.title = form["title"];
                    content.Categories = db.categories.FirstOrDefault(x => x.id == int.Parse(form["category"]));
                    content.image = form["image"];
                    content.content = form["content"];
                    content.releaseDate = Convert.ToDateTime(form["releaseDate"]);
                    content.date = DateTime.Now;
                    content.lastUpdate = DateTime.Now;
                    content.isDeleted = false;
                    content.userId = int.Parse(HttpContext.Session.GetString("userId"));

                    db.contents.Add(content);
                    db.SaveChanges();

                    string[] keyWords = form["keyword"].ToString().Split(',');
                    List<KeyWords> keyWordCheckingList = db.keyWords.ToList();
                    List<KeyWords> addedKeyWords = new List<KeyWords>();
                    int i = 0;
                    foreach (string word in keyWords)
                    {
                        keyWords[i] = word.TrimEnd(' ').TrimStart(' ');
                        if (!keyWordCheckingList.Contains(new KeyWords() { keyWord = keyWords[i] }))
                        {
                            db.keyWords.Add(new KeyWords() { keyWord = keyWords[i] });
                            addedKeyWords.Add(new KeyWords() { keyWord = keyWords[i] });
                        }
                    }
                    db.SaveChanges();

                    foreach (KeyWords word in addedKeyWords)
                    {
                        db.keyWordContainers.Add(new KeyWordContainer() { Content = content, Keyword = word });
                    }
                    db.SaveChanges();
                    return View(Tuple.Create("Sent", true, db.categories.ToList()));
                }
                return View(Tuple.Create("Index", false, db.categories.ToList()));
            }
            else
                return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Contents(int maxIndex, int username, DateTime firstDate, DateTime lastDate)
        {
            if (HttpContext.Session.GetString("userId") != null)
            {
                var content = db.contents.Include(x => x.user).Include(x => x.comments).ToList();

                if (maxIndex>0)
                {
                    content = content.Take(maxIndex).ToList();
                }
                if(username!=0)
                {
                    content = content.Where(x => x.userId == username).ToList();
                }
                if (firstDate!=DateTime.MinValue)
                {
                    content = content.Where(x => x.date > firstDate).ToList();
                }
                if (lastDate != DateTime.MinValue)
                {
                    content = content.Where(x => x.date > lastDate).ToList();
                }

                return View(Tuple.Create(content, db.users.ToList()));
            }
            else
                return RedirectToAction("Index");
        }



        [HttpPost]
        public IActionResult DeleteContent(int id)
        {
            if (HttpContext.Session.GetString("userId") != null)
            {
                if (HttpContext.Request.Method == "POST")
                {
                    var form = HttpContext.Request.Form;
                    db.contents.Remove(db.contents.FirstOrDefault(x => x.id == id));
                    db.SaveChanges();
                }

                return RedirectToAction("Contents");
            }
            else
                return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult CommentDelete(int id)
        {
            if (HttpContext.Session.GetString("userId") != null)
            {
                if (HttpContext.Request.Method == "POST")
                {
                    var form = HttpContext.Request.Form;
                    db.comments.Remove(db.comments.FirstOrDefault(x => x.id == id));
                    db.SaveChanges();
                }

                return RedirectToAction("Comments");
            }
            else
                return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult CommentAllow(int id)
        {
            if (HttpContext.Session.GetString("userId") != null)
            {
                if (HttpContext.Request.Method == "POST")
                {
                    var form = HttpContext.Request.Form;
                    var comment = db.comments.FirstOrDefault(x => x.id == id);
                    db.SaveChanges();
                }

                return RedirectToAction("Comments");
            }
            else
                return RedirectToAction("Index");
        }


        public IActionResult Comments()
        {
            if (HttpContext.Session.GetString("userId") != null)
                return View(db.comments.Include(x=> x.Content).ToList());
            else
                return RedirectToAction("Index");
        }


        public IActionResult Contacts()
        {
            if (HttpContext.Session.GetString("userId") != null)
                return View();
            else
                return RedirectToAction("Index");
        }
        public IActionResult Logout()
        {
            // Oturumu sonlandır
            HttpContext.Session.Clear();
            // Ana sayfaya yönlendir
            return RedirectToAction("Index");
        }
    }
}
