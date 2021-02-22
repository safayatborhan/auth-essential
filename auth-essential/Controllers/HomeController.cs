using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace auth_essential.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        //Guard an action
        [Authorize]
        public IActionResult Secret()
        {
            return View();
        }

        public IActionResult Authenticate()
        {
            /*
             * First you need some kind of thing just like passport or id card that can help you to recognize in outer world.
             * For this, you need claim here, they might have name, email and some other relative information.
             */
            var safayatClaims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, "Safayat"),
                new Claim(ClaimTypes.Email, "safayat.borhan@selise.ch"),
                new Claim("Friend.Says", "Good boy.")
            };

            var safayatLicenseClaims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, "Safayat Borhan"),
                new Claim("DrivingLicense", "A+")
            };

            var safayatIdentity = new ClaimsIdentity(safayatClaims, "Safayat identity");
            var safayatLicenceIdentity = new ClaimsIdentity(safayatLicenseClaims, "Government");

            var userPrincipal = new ClaimsPrincipal(new []
            {
                safayatIdentity,
                safayatLicenceIdentity
            });

            // Everything we have done here is to store information into cookie and authenticate using these.
            // So, we build user identity here and after that we are sending these identity to authorize the user. 
            // This will basically create a cookie object in browser.
            // In ConfigureServices, 
            /*
             *services.AddAuthentication("CookieAuth")
                .AddCookie("CookieAuth", config =>
                {
                    //You won't getting in if you don't have Safayat's cookie.
                    config.Cookie.Name = "Safayat.Cookie";
                    config.LoginPath = "/Home/Authenticate";
                });
             *
             */
            // And after that user can be authenticated.
            HttpContext.SignInAsync(userPrincipal);

            return RedirectToAction("Index");
        }
    }
}
