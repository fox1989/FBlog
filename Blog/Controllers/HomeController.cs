using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Blog.Models;
using Microsoft.AspNetCore.Http;

namespace Blog.Controllers
{
    public class HomeController : Controller
    {

        private readonly ApplicationDbContext db;
        public HomeController(ApplicationDbContext db)
        {
            this.db = db;

        }



        public IActionResult Index()
        {
            int pageCount = PostController.pageCount;
            int page = 1;
            List<Post> posts = db.Posts.Skip((page - 1) * pageCount).Take(pageCount).ToList();

            ViewData["currPage"] = page;
            ViewData["maxPage"] = Convert.ToInt32(Math.Ceiling(db.Posts.Count() * 1.0f / pageCount));

            return View(posts);

        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }


        public IActionResult Test()
        {

            return View();

        }
    }
}
