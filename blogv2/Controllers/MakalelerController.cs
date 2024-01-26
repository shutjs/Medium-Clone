using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using blogv2.Data;
using blogv2.Models;
using Microsoft.AspNetCore.Authorization;

namespace blogv2.Controllers
{
    public class MakalelerController : Controller
    {
        private readonly blogv2Context _context;

        public MakalelerController(blogv2Context context)
        {
            _context = context;
        }
      

        public async Task<IActionResult> Index()
        {
            return View(await _context.Makaleler.ToListAsync());
        }


        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var makaleler = await _context.Makaleler
                .FirstOrDefaultAsync(m => m.id == id);
            if (makaleler == null)
            {
                return NotFound();
            }

            return View(makaleler);
        }
 
       
        [Authorize]
        public IActionResult Create()
        {
            return View();
        }

        [Authorize]
        [HttpPost]
        public IActionResult Create(Makaleler makale)
        {
            if (ModelState.IsValid)
            {

                _context.Makaleler.Add(makale);
                _context.SaveChanges();


                ViewBag.SuccessMessage = "Makale başarıyla oluşturuldu.";

                return View();
            }


            return View(makale);
        }
     




    
       

     
    }
}
