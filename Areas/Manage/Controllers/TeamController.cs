using Microsoft.AspNetCore.Mvc;
using WebApplication11.DAL;
using WebApplication11.Models;

namespace WebApplication11.Areas.Manage.Controllers
{
    [Area("Manage")]
    public class TeamController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _environment;

        public TeamController(AppDbContext context,IWebHostEnvironment environment)
        {
            _context = context;
            _environment = environment;
        }
        public IActionResult Index()
        {
            return View(_context.Teams.ToList());
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Teams teams)
        {
            if (!ModelState.IsValid)
            {
                return View(teams);
            }
            string path = _environment.WebRootPath + @"\Upload\Team";
            string filname = Guid.NewGuid() + teams.ImgFile.FileName;
            using (FileStream stream = new FileStream(path + filname, FileMode.Create)) 
            {
                teams.ImgFile.CopyTo(stream);
            }
            teams.ImgUrl = filname;
            _context.Teams.Add(teams);
            _context.SaveChanges();

                return RedirectToAction("Index");
        }
        public IActionResult Update(int id)
        {

            Teams teams = _context.Teams.FirstOrDefault(t => t.Id == id);
            if (teams == null) 
            {
                return RedirectToAction("Index");
            }
            return View(teams);
        }
        [HttpPost]
        public IActionResult Update(Teams teams) 
        {
            Teams oldTeams = _context.Teams.FirstOrDefault(x=>x.Id == teams.Id);
            if(oldTeams==null) return NotFound();
            if (teams.ImgFile != null)
            {
                string path = _environment.WebRootPath + @"\Upload\Team\";
                string filname = Guid.NewGuid() + teams.ImgFile.FileName;
                using (FileStream stream = new FileStream(path + filname, FileMode.Create))
                {
                    teams.ImgFile.CopyTo(stream);
                }
                oldTeams.ImgUrl = filname;
            }

            oldTeams.Name = teams.Name;
            oldTeams.Position = teams.Position;
            _context.SaveChanges();

            return RedirectToAction("Index");
           
        }
    }
}
