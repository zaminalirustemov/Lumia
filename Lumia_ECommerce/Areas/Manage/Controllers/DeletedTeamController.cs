using Lumia_ECommerce.Context;
using Lumia_ECommerce.Helpers;
using Lumia_ECommerce.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace Lumia_ECommerce.Areas.Manage.Controllers;
[Area("Manage")]
[Authorize(Roles = "SuperAdmin,Admin,Editor")]
public class DeletedTeamController : Controller
{
    private readonly LumiaDbContext _lumiaDbContext;
    private readonly IWebHostEnvironment _webHostEnvironment;

    public DeletedTeamController(LumiaDbContext lumiaDbContext, IWebHostEnvironment webHostEnvironment)
    {
        _lumiaDbContext = lumiaDbContext;
        _webHostEnvironment = webHostEnvironment;
    }
    //Read-------------------------------------------------------------------------------------------------------------------------
    public IActionResult Index(int page=1)
    {
        var query = _lumiaDbContext.Teams.Include(x => x.Position).Where(x => x.isDeleted == true).AsQueryable();
        var paginatedList = new PaginatedList<Team>(query.Skip((page - 1) * 3).Take(3).ToList(), query.Count(), 3, page);
        return View(paginatedList);
    }
    //Restore----------------------------------------------------------------------------------------------------------------------
    public IActionResult Restore(int id)
    {
        Team team = _lumiaDbContext.Teams.FirstOrDefault(x => x.Id == id);
        if (team == null) return View("Error");

        team.isDeleted = false;
        _lumiaDbContext.SaveChanges();
        return RedirectToAction(nameof(Index));
    }
    //HardDelete-------------------------------------------------------------------------------------------------------------------
    public IActionResult HardDelete(int id)
    {
        Team team = _lumiaDbContext.Teams.FirstOrDefault(x => x.Id == id);
        if (team == null) return View("Error");

        FileManager.DeleteFile(_webHostEnvironment.WebRootPath, "uploads/team", team.ImageName);
        _lumiaDbContext.Teams.Remove(team);
        _lumiaDbContext.SaveChanges();
        return Ok();
    }
}

