using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StokTakipSistemi.Data;
using StokTakipSistemi.ViewModels;

namespace StokTakipSistemi.Controllers
{
    public class AccountController : Controller
    {
        private readonly StokTakipSistemiDbContext _dbContext;
        private readonly LoginVM _admin;

        public AccountController(StokTakipSistemiDbContext dbContext, LoginVM options)
        {
            _dbContext = dbContext;
            _admin = options;
        }

        [AllowAnonymous]
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Unauthorized()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginVM user)
        {
            if (LoginUser(user.Email, user.Sifre))
            {
                var birthDate = "1981";
                //var birthDate = _dbContext.User.Where(u => u.Email == user.Email).FirstOrDefault().Birthdate.Year.ToString();

                var claims = new List<Claim>()
                {
                    new Claim(ClaimTypes.Email, user.Email),
                    new Claim(ClaimTypes.DateOfBirth, birthDate)
                };

                var userIdentity = new ClaimsIdentity(claims, "cookie");
                var userPrincipal = new ClaimsPrincipal(userIdentity);
                await HttpContext.SignInAsync(userPrincipal);

                return RedirectToAction("Index", "Fatura");
            }

            return RedirectToAction("Unauthorized", "Account");
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();

            return RedirectToAction("Login");
        }

        [HttpPost]
        private bool LoginUser(string email, string sifre)
        {
            if (IsAdmin(email, sifre)) return true;
            return _dbContext.Kullanici.Any(u => u.Email == email && u.Sifre == u.Sifre);
        }

        private bool IsAdmin(string email, string password)
        {
            if (email == _admin.Email && password == _admin.Sifre) return true;
            return false;
        }
    }
}