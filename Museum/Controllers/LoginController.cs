using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Museum.Contexts;
using Museum.Models;
using System.Security.Cryptography;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;

namespace Museum.Controllers
{
    public class LoginController(UserContext context) : Controller
    {
        UserContext _context = context;

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult SignUp()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index(LoginModel model)
        {
            if (ModelState.IsValid)
            {
                foreach (var user in _context.GetAllUsers())
                {
                    if (user.Login != model.Login || user.Pass != Convert.ToBase64String(GenerateSha256Hash(model.Password, user.Salt)))
                        continue;

                    await Authenticate(user);
                    return RedirectToAction("Index", "Home");
                }

                ModelState.AddModelError("", "Некорректные логин и(или) пароль");
            }

            return View(model);
        }

        private async Task Authenticate(User user)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimsIdentity.DefaultNameClaimType, user.Login),
                new Claim(ClaimsIdentity.DefaultRoleClaimType, user.Role.ToString())
            };

            ClaimsIdentity id = new ClaimsIdentity(claims, "ApplicationCookie", ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);

            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(id));
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            return RedirectToAction("Index", "Home");
        }

        private byte[] GenerateSha256Hash(string password, string salt) 
        {
            string saltedPass = salt + password + salt;
            byte[] passwordBytes = Encoding.UTF8.GetBytes(saltedPass);
            
            return SHA256.HashData(passwordBytes);
        }
    }
}
