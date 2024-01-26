using blogv2.Data;
using blogv2.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace blogv2.Controllers
{
    public class ProfileController : Controller
    {
        private readonly blogv2Context _context;
        public ProfileController(blogv2Context context)
        {

            _context = context;

        }
        /* UNUTMAAAAAAAAAAAAAAAAAAAAAAAAAAAA */
        public async Task<IActionResult> Settings()
        {
            var Email = HttpContext.Session.GetString("Email");
            var kullanici = _context.Kullanicilar
               .FirstOrDefault(k => EF.Functions.Collate(k.Email, "Turkish_CI_AI") == Email);
            if (kullanici == null)
            {
                return RedirectToAction("Index", "Home");
            }
            ViewBag.User = kullanici.Username;
            ViewBag.Ad = kullanici.Name;
            ViewBag.Soyad = kullanici.Surname;
            ViewBag.profileImage = kullanici.profileImage;
            ViewBag.about = kullanici.About;
            ViewBag.Email = kullanici.Email;
            ViewBag.Pass = kullanici.Pass;
            ViewBag.ID = kullanici.id;
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> UpdateProfile(int userId, Kullanicilar updatedUser)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var existingUser = _context.Kullanicilar.FirstOrDefault(u => u.id == userId);
                    if (existingUser != null)
                    {
                        existingUser.Name = updatedUser.Name;
                        existingUser.Surname = updatedUser.Surname;
                        existingUser.About = updatedUser.About;
                        existingUser.profileImage = updatedUser.profileImage;
                        existingUser.Username = updatedUser.Username;

                        _context.Kullanicilar.Update(existingUser);
                        await _context.SaveChangesAsync();
                        ViewBag.SuccessMessage = "Profil Başarıyla Güncellendi";
                        return View();
                    }
                    else
                    {
                        ViewBag.ErrorMessage = "Kullanıcı bulunamadı.";
                    }
                }
                catch (Exception ex)
                {

                }
            }
            return View(updatedUser);
        }
            public async Task<IActionResult> Index(string username)
        {
            if (!username.StartsWith("@"))
            {
                return RedirectToAction("Index", "Home");
            }
            string User = username.StartsWith("@") ? username.Substring(1) : username;

            var kullanici = _context.Kullanicilar
               .FirstOrDefault(k => EF.Functions.Collate(k.Username, "Turkish_CI_AI") == User);
            if (kullanici == null)
            {
                return RedirectToAction("Index", "Home");
            }
            ViewBag.User = kullanici.Username;
            ViewBag.Ad = kullanici.Name;
            ViewBag.Soyad = kullanici.Surname;
            ViewBag.profileImage = kullanici.profileImage;
            ViewBag.about = kullanici.About;

            return View(await _context.Makaleler.ToListAsync()); 
        }
    }
}
