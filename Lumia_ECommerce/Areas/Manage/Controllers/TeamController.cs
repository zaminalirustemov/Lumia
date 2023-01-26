using Lumia_ECommerce.Context;
using Lumia_ECommerce.Helpers;
using Lumia_ECommerce.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace Lumia_ECommerce.Areas.Manage.Controllers;
[Area("Manage")]
[Authorize(Roles = "SuperAdmin,Admin,Editor")]
public class TeamController : Controller
{
    private readonly LumiaDbContext _lumiaDbContext;
    private readonly IWebHostEnvironment _webHostEnvironment;

    public TeamController(LumiaDbContext lumiaDbContext,IWebHostEnvironment webHostEnvironment )
    {
        _lumiaDbContext = lumiaDbContext;
        _webHostEnvironment = webHostEnvironment;
    }
    //Read-------------------------------------------------------------------------------------------------------------------------
    public IActionResult Index(int page=1)
    {
        var query = _lumiaDbContext.Teams.Include(x => x.Position).Where(x => x.isDeleted == false).AsQueryable();
        var paginatedList = new PaginatedList<Team>(query.Skip((page - 1) * 3).Take(3).ToList(), query.Count(), 3, page);
        return View(paginatedList);
    }
    //Create-----------------------------------------------------------------------------------------------------------------------
    public IActionResult Create()
    {
        ViewBag.Positions = _lumiaDbContext.Positions.Where(x => x.isDeleted == false).ToList();
        return View();
    }
    [HttpPost]
    public IActionResult Create(Team team)
    {
        ViewBag.Positions = _lumiaDbContext.Positions.Where(x => x.isDeleted == false).ToList();
        if(!ModelState.IsValid) return View(team);
        if (team.ImageFile == null)
        {
            ModelState.AddModelError("ImageFile", "Image cannot be empty");
            return View();
        }
        if (team.ImageFile.ContentType!="image/jpeg" && team.ImageFile.ContentType != "image/png")
        {
            ModelState.AddModelError("ImageFile", "You can only upload images in png and jpeg format");
            return View();
        }
        if (team.ImageFile.Length > 2097152)
        {
            ModelState.AddModelError("ImageFile", "You can only upload images that are less than 2 MB in size");
            return View();
        }

        team.ImageName = FileManager.SaveFile(team.ImageFile,_webHostEnvironment.WebRootPath,"uploads/team");
        _lumiaDbContext.Teams.Add(team);
        _lumiaDbContext.SaveChanges();
        return RedirectToAction(nameof(Index));
    }
    //Update-----------------------------------------------------------------------------------------------------------------------
    public IActionResult Update(int id)
    {
        ViewBag.Positions = _lumiaDbContext.Positions.Where(x => x.isDeleted == false).ToList();
        Team team = _lumiaDbContext.Teams.FirstOrDefault(x => x.Id == id);
        if(team == null) return View("Error");
         
        return View(team);
    }
    [HttpPost]
    public IActionResult Update(Team newTeam)
    {
        ViewBag.Positions = _lumiaDbContext.Positions.Where(x => x.isDeleted == false).ToList();
        Team existTeam = _lumiaDbContext.Teams.FirstOrDefault(x => x.Id == newTeam.Id);
        if (existTeam == null) return View("Error");
        if (!ModelState.IsValid) return View(newTeam);
        if (newTeam.ImageFile == null)
        {
            ModelState.AddModelError("ImageFile", "Image cannot be empty");
            return View();
        }
        if (newTeam.ImageFile.ContentType != "image/jpeg" && newTeam.ImageFile.ContentType != "image/png")
        {
            ModelState.AddModelError("ImageFile", "You can only upload images in png and jpeg format");
            return View();
        }
        if (newTeam.ImageFile.Length > 2097152)
        {
            ModelState.AddModelError("ImageFile", "You can only upload images that are less than 2 MB in size");
            return View();
        }
        FileManager.DeleteFile(_webHostEnvironment.WebRootPath, "uploads/team", existTeam.ImageName);
        existTeam.ImageName = FileManager.SaveFile(newTeam.ImageFile, _webHostEnvironment.WebRootPath, "uploads/team");

        existTeam.PositionId = newTeam.PositionId;
        existTeam.Fulllname = newTeam.Fulllname;
        existTeam.Description = newTeam.Description;
        existTeam.TwURl = newTeam.TwURl;
        existTeam.FbUrl = newTeam.FbUrl;
        existTeam.InstUrl = newTeam.InstUrl;
        existTeam.LInUrl = newTeam.LInUrl;
        _lumiaDbContext.SaveChanges();

        return RedirectToAction(nameof(Index));
    }
    //SoftDelete-------------------------------------------------------------------------------------------------------------------
    public IActionResult SoftDelete(int id)
    {
        Team team = _lumiaDbContext.Teams.FirstOrDefault(x => x.Id == id);
        if(team == null) return View("Error");

        team.isDeleted = true;
        _lumiaDbContext.SaveChanges();
        //return RedirectToAction(nameof(Index));
        return Ok();
    }
}

