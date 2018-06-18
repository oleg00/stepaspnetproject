using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebAppBlog.UI.Data;
using WebAppBlog.UI.Models;
using WebAppBlog.UI.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Hosting;
using System.IO;
using Microsoft.AspNetCore.Identity;

namespace WebAppBlog.UI.Controllers
{
    public class PostsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private IHostingEnvironment _env;
        private UserManager<ApplicationUser> _userManager;

        public PostsController(ApplicationDbContext context, IHostingEnvironment env,
            UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _env = env;
            _userManager = userManager;
        }

        // GET: Posts
        public async Task<IActionResult> Index(int page = 1)
        {
            int pageSize = 2;

            var posts = _context.Posts.Include(p => p.ApplicationUser).Include(p => p.Comments).ToList();
            var items = posts.Skip((page - 1) * pageSize).ToList();
            var count = items.Count;

            if (items.Count < pageSize)
            {
                while (items.Count != pageSize)
                    pageSize--;

                items = items.Take(pageSize).ToList();
            }

            items = items.Take(pageSize).ToList();

            PageViewModel pageViewModel = new PageViewModel(count, pageSize, page);

            PostViewModel postViewModel = new PostViewModel
            {
                PageViewModel = pageViewModel,
                Posts = items,
                Users = _context.Users.ToList()
            };

            return View(postViewModel);
        }

        // GET: Posts/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var post = await _context.Posts.SingleOrDefaultAsync(m => m.Id == id);
            if (post == null)
            {
                return NotFound();
            }

            return View(post);
        }

        // GET: Posts/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Posts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,FullContent,PublishDate,ShortContent,Title")] Post post, IFormFile ImageFile)
        {
            post.ApplicationUserId = _context.Users.Single(u => u.UserName == User.Identity.Name).Id;

            if (ImageFile != null)
            {
                string name = ImageFile.FileName;
                string path = $"/files/{name}";
                string serverPath = _env.WebRootPath + path;

                FileStream fs = new FileStream(serverPath, FileMode.Create, FileAccess.Write);
                await ImageFile.CopyToAsync(fs);

                post.ImageUrl = path;
            }
            
                _context.Add(post);   
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
        }

        // GET: Posts/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var post = await _context.Posts.SingleOrDefaultAsync(m => m.Id == id);
            if (post == null)
            {
                return NotFound();
            }
            return View(post);
        }

        [HttpPost]
        public ActionResult ToComment(string id, PostViewModel model)
        {
            Comment com = new Comment
            {
                ApplicationUserId = _context.Users.Single(u => u.UserName == User.Identity.Name).Id,
                PublishDate = DateTime.Now,
                PostId = int.Parse(id),
                Content = model.TextAreaComment
            };

            _context.Comments.Add(com);
            _context.SaveChanges();

            return RedirectToAction("Index", 1);
        }
        

        // POST: Posts/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,ApplicationUserId,FullContent,PublishDate,ShortContent,Title")] Post post)
        {
            if (id != post.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(post);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PostExists(post.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Index");
            }
            return View(post);
        }

        // GET: Posts/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var post = await _context.Posts.SingleOrDefaultAsync(m => m.Id == id);
            if (post == null)
            {
                return NotFound();
            }

            return View(post);
        }

        // POST: Posts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var post = await _context.Posts.SingleOrDefaultAsync(m => m.Id == id);
            _context.Posts.Remove(post);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool PostExists(int id)
        {
            return _context.Posts.Any(e => e.Id == id);
        }
    }
}
