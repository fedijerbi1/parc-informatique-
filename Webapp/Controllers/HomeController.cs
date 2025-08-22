using System.Diagnostics;
using System.Linq; 
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Webapp.Models; 
using Webapp.Data;
using Microsoft.AspNetCore.Authorization; 
using Microsoft.AspNetCore.Identity;

namespace Webapp.Controllers
{
    public class HomeController : Controller
    {
        private  ILogger<HomeController> _logger;
        private  AppDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public HomeController(ILogger<HomeController> logger, AppDbContext context, UserManager<ApplicationUser> userManager)
        {
            _logger = logger;
            _context = context;
            _userManager = userManager;
        }
        [Authorize]
            [HttpGet]

        public async Task<IActionResult> Index()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return RedirectToAction("Login");
            }
            ViewBag.UserName = user.FullName;
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        private async Task LoadAllDataToViewBag()
        {
            ViewBag.AllEmployees = await _context.Employees.ToListAsync();
            ViewBag.AllEquipment = await _context.Equipment.ToListAsync();
            ViewBag.AllAffectations = await _context.Affectations
                .Include(a => a.Employee)
                .Include(a => a.Equipment)
                .OrderByDescending(a => a.DateAffectation)
                .ToListAsync();
        }
       
        public async Task<IActionResult> Affectation()
        {
            await LoadAllDataToViewBag();
            ViewBag.Employees = await _context.Employees.Where(e => e.IsActif).ToListAsync();
            ViewBag.Equipment = await _context.Equipment.Where(e => e.Statut == "En service").ToListAsync();

            var affectations = await _context.Affectations
                .Where(a => a.IsActif)
                .Include(a => a.Employee)
                .Include(a => a.Equipment)
                .OrderByDescending(a => a.DateAffectation)
                .ToListAsync();
            ViewBag.Affectations = affectations;

            return View(new Affectation());
        }

        [HttpPost] 
        [Authorize(Roles = "Admin")] 
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Affectation(Affectation affectation)
        {
            if (ModelState.IsValid)
            {
                // Vérifier que l'équipement est disponible
                var equipment = await _context.Equipment.FindAsync(affectation.EquipmentId);
                if (equipment != null && equipment.Statut == "En service")
                {
                 
                    _context.Affectations.Add(affectation);
                    
            
                    
                    equipment.EmployeeId = affectation.EmployeeId;                    
                    equipment.DateDerniereAffectation = DateTime.Now; 
                    await _context.SaveChangesAsync();
                    TempData["SuccessMessage"] = "Équipement affecté avec succès.";
                    return RedirectToAction("Affectation");
                }
                else
                {
                    ModelState.AddModelError("", "L'équipement sélectionné n'est pas disponible.");
                }
            }

            ViewBag.Employees = await _context.Employees.Where(e => e.IsActif).ToListAsync();
            ViewBag.Equipment = await _context.Equipment.Where(e => e.Statut == "En service").ToListAsync(); 
            await LoadAllDataToViewBag();
           
            return View(affectation);
        }

        [HttpPost] 
        [Authorize(Roles = "Admin")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RetournerEquipement(int affectationId)
        {
            var affectation = await _context.Affectations
                .Include(a => a.Equipment)
                .FirstOrDefaultAsync(a => a.Id == affectationId && a.IsActif); 
            var equip = await _context.Equipment.FirstOrDefaultAsync(e => e.Id == affectation.EquipmentId);

            if (affectation != null)
            {
                // Marquer l'affectation comme inactive
                affectation.IsActif = false;
                affectation.DateRetour = DateTime.Now;

                // Remettre l'équipement en disponible
                if (affectation.Equipment != null)
                {
                    affectation.Equipment.Statut = "En service";
                    affectation.Equipment.EmployeeId = null; 
                    
                    if (equip != null)
                    {
                        equip.DateDerniereAffectation = DateTime.Now; // ✅ Correct : dernière fois qu'il a été retourné
                    }
                }

                await _context.SaveChangesAsync();
                TempData["SuccessMessage"] = "Équipement retourné avec succès.";
            }
            else
            {
                TempData["ErrorMessage"] = "Affectation introuvable.";
            }

            return RedirectToAction("Affectation");
        }
        [Authorize]
        public IActionResult Composants()
        {

            return View();
        }

        
      

        public IActionResult AjouterEmployer()
        {
            return View(); 
        }


        public  IActionResult AjouterEquip()
        {
            return View ();
        }
        [HttpPost] 
        [ValidateAntiForgeryToken ]
        public async Task<IActionResult> Create(Equipment equipe)
        {


            if (ModelState.IsValid)
            {
                _context.Equipment.Add(equipe);
                await _context.SaveChangesAsync();
                TempData["SuccessMessage"] = "Équipement ajouté avec succès.";
                return RedirectToAction("Equipement");
            }

            return View("Index");
        }
        
        [HttpGet]
        public async Task<IActionResult> Equipement()
        {
            var list = await _context.Equipment.ToListAsync();
            return View(list);
        }

        public async Task<IActionResult> CreateE(Employee employe)
        {
            if (ModelState.IsValid)
            {
                _context.Employees.Add(employe);
                await _context.SaveChangesAsync();
                return RedirectToAction("Employer");
            }
            else
            {
                return View("AjouterEmployer");
            }

        }
        [HttpGet] 
        [Authorize(Roles = "Admin")] 
        public IActionResult EditEquip(int id)
        {
            var equip = _context.Equipment.FirstOrDefault(e => e.Id == id);
            if (equip == null)
            {
                return NotFound();
            }
            return View(equip);
        }
        [HttpPost] 
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditE(Equipment equipe)
        {
            if (!ModelState.IsValid)
            {
                return View("EditEquip", equipe);
            }

            _context.Equipment.Update(equipe);
            await _context.SaveChangesAsync(); 
                            TempData["SuccessMessage"] = "Équipement modifié avec succès.";

            return RedirectToAction("Equipement");
        }  
        public async Task<IActionResult> Employer()
        {
            var list = await _context.Employees.ToListAsync();
            return View(list);
        }
        public async Task<IActionResult> Alert()
        {
            var list = await _context.Equipment.ToListAsync();
            return View(list);
        } 
        [HttpPost]
        [Authorize(Roles = "Admin")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Supprime(int id)
        { 
           

           
            var e = await _context.Equipment.FirstOrDefaultAsync(e => e.Id == id);

            if (e == null)
            {
                return NotFound();
            }

            else
            {
                var er = await _context.Affectations.Where(e => e.EquipmentId == id).ToListAsync();
                if (er.Any())
                {
                    _context.Affectations.RemoveRange(er);
                }

                _context.Equipment.Remove(e);
                await _context.SaveChangesAsync();
                TempData["DeleteMessage"] = $"Équipement '{e.Type}' supprimé avec succès.";
                return RedirectToAction("Equipement");
            } 
            
        
           

            

        }
        [HttpGet]
        [Authorize(Roles = "Admin")] 
        public async Task<IActionResult> EditEmployer(int id)
        {
            var employe = await _context.Employees.FirstOrDefaultAsync(e => e.Id == id);
            if (employe == null)
            {
                return NotFound();
            }

            return View(employe);
        }
        [HttpPost]
        [ValidateAntiForgeryToken] 
        public async Task<IActionResult> EditEm(Employee employe)
        {
            if (!ModelState.IsValid)
            {
                return View("EditEmployer", employe);
            }

            _context.Employees.Update(employe);
            await _context.SaveChangesAsync();
            TempData["EditMessage"] = $"Employé '{employe.Nom}' modifié avec succès.";
            return RedirectToAction("Employer");
        }
        [Authorize(Roles = "Admin")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteEm(int id)
        {
            var employe = await _context.Employees.FirstOrDefaultAsync(e => e.Id == id);

            if (employe == null)
            {
                return NotFound();
            }
            var e = await _context.Affectations.Where(e => e.EmployeeId == id).ToListAsync();
            if (e.Any())
            {
                _context.Affectations.RemoveRange(e);
            }

            _context.Employees.Remove(employe);
            await _context.SaveChangesAsync();
            TempData["DeleteMessage"] = $"Employé '{employe.Nom}' supprimé avec succès.";
            return RedirectToAction("Employer");
        }





        public async Task<IActionResult> DetailsEmployer(int id)
        {

            var employe = await _context.Employees.FirstOrDefaultAsync(e => e.Id == id);
            if (employe == null)
            {
                return NotFound();
            }
            return View(employe);
        }

        public async Task<IActionResult> DetailsEquip(int id)

        {

            var equipement = await _context.Equipment.FirstOrDefaultAsync(e => e.Id == id);
            if (equipement == null)
            {
                return NotFound();
            }
            return View(equipement);
        } 
        public async Task<IActionResult> Dashboard()
        {
            var data = await _context.Equipment.ToListAsync(); 
            if (data == null || !data.Any())
            {
                return NotFound();
            }
           
            
                var repartition = await _context.Equipment
                    .GroupBy(e => new { e.Type, e.Statut })
                    .Select(g => new
                    {
                        Type = g.Key.Type,
                        Statut = g.Key.Statut,
                        Count = g.Count()
                     }
                    )
                .ToListAsync(); 


                 var repType = await _context.Equipment
        .GroupBy(e => e.Type)
        .Select(g => new { Label = g.Key, Value = g.Count() })
        .ToListAsync();

    ViewBag.RepType = repType;
            
           ViewBag.Repartition = repartition;
            return View(data);
        } 

       

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }

}
