using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using ДЗ_на_25_мая_Тимур_Жуков.Models;
using ДЗ_на_25_мая_Тимур_Жуков.Repositories;
using ДЗ_на_25_мая_Тимур_Жуков.ViewModels;

namespace ДЗ_на_25_мая_Тимур_Жуков.Controllers
{
    public class AccountController : Controller
    {
        private readonly IAccountRepo _accountRepo;
        private readonly IStudentProfileRepo _studentProfileRepo;

        public AccountController(IAccountRepo accountRepo, IStudentProfileRepo studentProfileRepo)
        {
            _accountRepo = accountRepo;
            _studentProfileRepo = studentProfileRepo;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View(new LoginViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> Login(string Email, string Password)
        {
            if (string.IsNullOrEmpty(Email) || string.IsNullOrEmpty(Password))
            {
                ViewBag.Error = "Заполните все поля";
                return View(new LoginViewModel());
            }

            var account = await _accountRepo.GetByEmailAsync(Email);

            if (account == null)
            {
                ViewBag.Error = $"Пользователь '{Email}' не найден";
                return View(new LoginViewModel { Email = Email });
            }

            if (account.PasswordHash != Password)
            {
                ViewBag.Error = "Неверный пароль";
                return View(new LoginViewModel { Email = Email });
            }

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, account.Id.ToString()),
                new Claim(ClaimTypes.Email, account.Email),
                new Claim(ClaimTypes.Role, account.Role.ToString())
            };

            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));

            return RedirectToAction("Index", "Cabinet");
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View(new RegisterViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> Register(string Email, string Password, string FullName, string GroupName)
        {
            if (string.IsNullOrEmpty(Email) || string.IsNullOrEmpty(Password))
            {
                ViewBag.Error = "Заполните все поля";
                return View(new RegisterViewModel());
            }

            var existing = await _accountRepo.GetByEmailAsync(Email);
            if (existing != null)
            {
                ViewBag.Error = "Пользователь уже существует";
                return View(new RegisterViewModel { Email = Email });
            }

            var account = new AccountModel
            {
                Email = Email,
                PasswordHash = Password,
                Role = AccountRole.Student
            };

            await _accountRepo.CreateAsync(account);

            var profile = new StudentProfileModel
            {
                AccountId = account.Id,
                FullName = FullName,
                GroupName = GroupName
            };

            await _studentProfileRepo.CreateAsync(profile);

            return RedirectToAction("Login");
        }

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login");
        }

        [HttpGet]
        public IActionResult AccessDenied()
        {
            return View();
        }
    }
}