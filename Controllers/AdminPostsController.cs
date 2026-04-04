using IlaydaTechBlog.Data;
using IlaydaTechBlog.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace IlaydaTechBlog.Controllers
{
    public class AdminPostsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AdminPostsController(ApplicationDbContext context)
        {
            _context = context;
        }

        private bool IsAdminLoggedIn()
        {
            return HttpContext.Session.GetString("IsAdminLoggedIn") == "true";
        }

        public async Task<IActionResult> Index()
        {
            if (!IsAdminLoggedIn())
                return RedirectToAction("Login", "Admin");

            return View(await _context.Posts.OrderByDescending(p => p.CreatedAt).ToListAsync());
        }

        public IActionResult Create()
        {
            if (!IsAdminLoggedIn())
                return RedirectToAction("Login", "Admin");

            var post = new Post
            {
                Author = "İlayda"
            };

            return View(post);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Post post)
        {
            if (!IsAdminLoggedIn())
                return RedirectToAction("Login", "Admin");

            if (string.IsNullOrWhiteSpace(post.Author))
                post.Author = "İlayda";

            if (ModelState.IsValid)
            {
                post.CreatedAt = DateTime.Now;
                _context.Posts.Add(post);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(post);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (!IsAdminLoggedIn())
                return RedirectToAction("Login", "Admin");

            if (id == null) return NotFound();

            var post = await _context.Posts.FindAsync(id);
            if (post == null) return NotFound();

            return View(post);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Post post)
        {
            if (!IsAdminLoggedIn())
                return RedirectToAction("Login", "Admin");

            if (id != post.Id) return NotFound();

            if (string.IsNullOrWhiteSpace(post.Author))
                post.Author = "İlayda";

            if (ModelState.IsValid)
            {
                _context.Update(post);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(post);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (!IsAdminLoggedIn())
                return RedirectToAction("Login", "Admin");

            if (id == null) return NotFound();

            var post = await _context.Posts.FirstOrDefaultAsync(p => p.Id == id);
            if (post == null) return NotFound();

            return View(post);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (!IsAdminLoggedIn())
                return RedirectToAction("Login", "Admin");

            var post = await _context.Posts.FindAsync(id);

            if (post != null)
            {
                _context.Posts.Remove(post);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction("Index");
        }
    }
}