using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ДЗ_на_25_мая_Тимур_Жуков.Models;
using ДЗ_на_25_мая_Тимур_Жуков.Services.Cabinet;
using ДЗ_на_25_мая_Тимур_Жуков.ViewModels;

namespace ДЗ_на_25_мая_Тимур_Жуков.Controllers;

[Authorize]
public class CabinetController : Controller
{
    private readonly IAdminCabinetService _adminCabinetService;

    public CabinetController(IAdminCabinetService adminCabinetService)
    {
        _adminCabinetService = adminCabinetService;
    }

    [HttpGet]
    public IActionResult Index()
    {
        var role = User.FindFirst(ClaimTypes.Role)?.Value;

        if (role == "Admin")
            return RedirectToAction("Admin");

        if (role == "Student")
            return RedirectToAction("Student");

        return RedirectToAction("Login", "Account");
    }

    [HttpGet]
    public IActionResult Student()
    {
        return View();
    }

    [HttpGet]
    [Authorize]
    public async Task<IActionResult> Admin()
    {
        try
        {
            var accounts = await _adminCabinetService.GetAllStudentAccountsAsync();
            var profiles = await _adminCabinetService.GetAllStudentProfilesAsync();

            var students = profiles.Select(profile => new AdminCabinetStudentViewModel
            {
                AccountId = profile.AccountId,
                FullName = profile.FullName,
                GroupName = profile.GroupName,
                PhotoUrl = profile.PhotoUrl,
                IsEmailConfirmed = _adminCabinetService.IsEmailConfirmed(profile.AccountId)
            }).ToList();

            return View(new AdminCabinetViewModel { Students = students });
        }
        catch
        {
            return View(new AdminCabinetViewModel { Students = new List<AdminCabinetStudentViewModel>() });
        }
    }

    [HttpPost]
    [Authorize]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> ConfirmStudentEmail(int id)
    {
        await _adminCabinetService.ConfirmStudentEmailAsync(id);
        return RedirectToAction("Admin");
    }
}