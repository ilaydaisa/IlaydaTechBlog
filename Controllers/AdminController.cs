using IlaydaTechBlog.Models;
using Microsoft.AspNetCore.Mvc;

namespace IlaydaTechBlog.Controllers
{
    public class AdminController : Controller
    {
        private const string AdminUsername = "admin";
        private const string AdminPassword = "1234";

        public IActionResult Login()
        {
            if (HttpContext.Session.GetString("IsAdminLoggedIn") == "true")
            {
                return RedirectToAction("Index", "AdminPosts");
            }

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Login(AdminLoginViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            if (model.Username == AdminUsername && model.Password == AdminPassword)
            {
                HttpContext.Session.SetString("IsAdminLoggedIn", "true");
                return RedirectToAction("Index", "AdminPosts");
            }

            ViewBag.ErrorMessage = "Kullanıcı adı veya şifre hatalı.";
            return View(model);
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Remove("IsAdminLoggedIn");
            return RedirectToAction("Login", "Admin");
        }
    }
}