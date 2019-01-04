using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Blog.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Blog.Controllers
{
    public class PostController : Controller
    {
        private readonly ApplicationDbContext db;

        public PostController(ApplicationDbContext db)
        {
            this.db = db;

        }

        [Authorize]
        [HttpGet]
        [Route("/addPost")]
        public IActionResult AddPost()
        {
            return View();
        }


        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("/addPost")]
        public IActionResult AddPost([Bind("title", "content")]Post post)
        {

            if (ModelState.IsValid)
            {
                post.time = DateTime.Now;
                post.description = GetDescrip(post.content);
                db.Posts.Add(post);
                db.SaveChanges();
                return Redirect("/page/");
            }

            return View(post);
        }


        [HttpGet]
        [Route("/editPost/{id}")]
        public IActionResult EditPost(int id)
        {
            Post p = db.Posts.FirstOrDefault(a => a.id == id);
            return View(p);
        }


        [HttpPost]
        [Route("/editPost/")]
        public IActionResult EditPost(Post post)
        {
            Post p = db.Posts.FirstOrDefault(a => a.id == post.id);
            if (p != null)
            {
                p.content = post.content;
                p.title = post.title;
                p.description = GetDescrip(post.content);



                db.Posts.Update(p);
                db.SaveChanges();

                return Redirect("/page/");
            }

            return View(post);
        }




        string GetDescrip(string content)
        {
            string tempStr = "";

            if (content != null)
            {
                Regex regex = new Regex(@"<[^>]+>|</[^>]+>");
                content = regex.Replace(content, "");
                // content = Regex.Replace(content, @"<[^>]*>", "");
                tempStr = content.Substring(0, 255) + "...";
                return tempStr;
            }

            return null;

        }

        [HttpGet]
        //[Route("/post/{id}")]
        public IActionResult ShowPost(int id)
        {
            Post p = db.Posts.FirstOrDefault(a => a.id == id);

            if (p != null)
            {
                return View(p);
            }
            else
            {

            }

            return View();
        }

        public static int pageCount = 2;

        [HttpGet]
        [Route("/page/{page=1}")]
        public IActionResult Page(int page = 1)
        {
            List<Post> posts = db.Posts.Skip((page - 1) * pageCount).Take(pageCount).ToList();

            ViewData["currPage"] = page;
            ViewData["maxPage"] =Convert.ToInt32( Math.Ceiling(db.Posts.Count()*1.0f/ pageCount));

            return View(posts);
        }


    }
}