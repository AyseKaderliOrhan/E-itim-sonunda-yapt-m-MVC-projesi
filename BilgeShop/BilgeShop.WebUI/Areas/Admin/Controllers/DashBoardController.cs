using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BilgeShop.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")] // program.cs'deki area:exists kısmı ile eşleşir.
    [Authorize(Roles = "Admin")] // Claim'lerdeki claims.Add(new Claim(ClaimTypes.Role, userDto.UserType.ToString())); kısım ile bağlantılı(authcontroller).

    // Yukarda yazdığım authorize sayesinde, yetkisi admin olmayan kişiler, bu controller'a istek atamaz.

    public class DashBoardController : Controller
    {        
        public IActionResult Index()
        {
            return View();
        }
    }
}
