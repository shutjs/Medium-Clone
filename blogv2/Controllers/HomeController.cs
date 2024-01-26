using blogv2.Data;
using blogv2.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using Microsoft.Extensions.Logging;
using Microsoft.CodeAnalysis.Elfie.Diagnostics;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using System.Reflection.Emit;
using System.Collections.Generic;
using System.Net.Mail;
using System.Net;
using MimeKit;
using MailKit.Net.Smtp;
using SmtpClient = MailKit.Net.Smtp.SmtpClient;




namespace blogv2.Controllers
{
    public class HomeController : Controller
    {

        private readonly blogv2Context _context;

        public HomeController(blogv2Context context)
        {

            _context = context;

        }

        public async Task<IActionResult> Index()
        {

            if (User.Identity.IsAuthenticated)
            {
                var currentUser = HttpContext.User;
                var userInfo = new List<string>
          {
        currentUser.Identity.Name,
        currentUser.FindFirst(ClaimTypes.Email)?.Value,
        currentUser.FindFirst(ClaimTypes.GivenName)?.Value,
        currentUser.FindFirst(ClaimTypes.Surname)?.Value,
        currentUser.FindFirst(ClaimTypes.NameIdentifier)?.Value,
        currentUser.FindFirst("ProfileImage")?.Value
             };
                ViewBag.UserInfo = userInfo;

            }
            return View(await _context.Makaleler.ToListAsync());
        }


        public IActionResult Login()
        {

            return View();
        }

        public IActionResult about()
        {
            return View();
        }

        [HttpPost]

        public async Task<IActionResult> Login(string email, string parola)
        {


            var user = _context.Kullanicilar.FirstOrDefault(u => u.Email == email && u.Pass == parola);

            if (user == null || string.IsNullOrEmpty(user.profileImage))
            {
                string defaultProfileImage = "/images/Remove-bg.ai_1701444511316.png";
                HttpContext.Session.SetString("ProfileImage", defaultProfileImage);
            }
            else
            {
                HttpContext.Session.SetString("ProfileImage", user.profileImage);
            }

            if (user != null)
            {
                var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, user.Username),
            new Claim(ClaimTypes.Email, user.Email),
            new Claim(ClaimTypes.GivenName, user.Name),
            new Claim(ClaimTypes.Surname, user.Surname),
            new Claim(ClaimTypes.NameIdentifier, user.id.ToString()),
            new Claim("ProfileImage", user.profileImage)

        };
                HttpContext.Session.SetString("Email", user.Email);
                HttpContext.Session.SetString("name", user.Name);
                HttpContext.Session.SetString("MailVerifDurum", user.MailVerifDurum);
                var userID = new ClaimsIdentity(claims, "Login");

                ClaimsPrincipal principal = new ClaimsPrincipal(userID);
                await HttpContext.SignInAsync(principal);




                return RedirectToAction("Index", "Home");
            }
            return RedirectToAction("Index", "Home");
        }
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> MailOnay(string code)
        {
            var mailkodDurum = "";
            var mailkod = "";

            var currentUser = HttpContext.User;
            var username = currentUser.FindFirst(ClaimTypes.Name)?.Value;
            var idm = currentUser.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (username != null)
            {
                var user = await _context.Kullanicilar.FirstOrDefaultAsync(u => u.id == Convert.ToInt32(idm));

                if (user != null)
                {
                    mailkod = user.MailCode?.ToString();
                    mailkodDurum = user.MailVerifDurum;

                    if (code == mailkod)
                    {
                        user.MailVerifDurum = "evet";
                        await _context.SaveChangesAsync();
                        HttpContext.Session.SetString("MailVerifDurum", user.MailVerifDurum);

                    }
                }
            }

            return RedirectToAction("Index", "Home");
        }
        [HttpPost]
        public async Task<IActionResult> Register(string username, string emailreg, string passwordreg, string name, string surname)
        {
            var existingUserWithEmail = _context.Kullanicilar.FirstOrDefault(u => u.Email == emailreg);
            var existingUserWithUsername = _context.Kullanicilar.FirstOrDefault(u => u.Username == username);


            if (existingUserWithEmail != null)
            {
                ModelState.AddModelError("Email", "Bu e-posta adresi zaten kullanýmda.");
            }

            if (existingUserWithUsername != null)
            {
                ModelState.AddModelError("Username", "Bu kullanýcý adý zaten kullanýmda.");
            }

            if (!ModelState.IsValid)
            {
                return View("Index");
            }
            Random rnd = new Random();
            int code = rnd.Next(100000, 900000);
            var user = new Kullanicilar
            {
                Username = username,
                Email = emailreg,
                Pass = passwordreg,
                Name = name,
                Surname = surname,
                MailCode = code,
                MailVerifDurum = "hayir",
                profileImage = "/images/Remove-bg.ai_1701444511316.png"
            };
            _context.Kullanicilar.Add(user);
            await _context.SaveChangesAsync();

            MimeMessage minimessage = new MimeMessage();
            MailboxAddress mailboxFrom = new MailboxAddress("Medium Verify", "mailverify223@gmail.com");
            MailboxAddress mailboxAdressTo = new MailboxAddress("User", user.Email);

            minimessage.From.Add(mailboxFrom);
            minimessage.To.Add(mailboxAdressTo);

            var bodyBuilder = new BodyBuilder();
            bodyBuilder.HtmlBody = @"<!DOCTYPE html>
                                <html lang=""en"">
                                <head>
                                    <meta charset=""UTF-8"">
                                    <meta name=""viewport"" content=""width=device-width, initial-scale=1.0"">
                                    <title>Medium Onay Kod</title>
                                </head>
                                <body style=""font-family: 'Arial', sans-serif; text-align: center; background-color: #f4f4f4; padding: 20px;"">
                                    <h2 style=""color: #333;"">Medium Onay Kodu</h2>
                                    <p style=""color: #666;"">Merhaba, " + username + @"</p>
                                    <p style=""color: #666;"">Kayıt olabilmek için aşağıdaki onay kodunu sitemize girmeniz gerekmektedir:</p>
                                    <p style=""font-size: 24px; color: #4285f4; font-weight: bold;"">" + code + @"</p>
                                    <p style=""color: #666;"">Bu onay kodu sadece bu işlem için geçerlidir ve güvenliğiniz için oluşturulmuştur.</p>
                                    <p style=""color: #666;"">İyi günler dileriz.</p>
                                </body>
                                </html>";

            minimessage.Body = bodyBuilder.ToMessageBody();
            minimessage.Subject = "Medium Onay Kod";

            SmtpClient smtpClient = new SmtpClient();
            smtpClient.Connect("smtp.gmail.com", 587, false);
            smtpClient.Authenticate("mailverify223@gmail.com", "sewznjwikhvbqufl");

            smtpClient.Send(minimessage);
            smtpClient.Disconnect(true);
            //mailverify223@gmail.com
            //Burakinr1
            return RedirectToAction("Index", "Home");
        }
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
        
        [Authorize]

        public IActionResult Profile(string username)
        {

            if (string.IsNullOrEmpty(username))
            {
                return NotFound();
            }

            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
