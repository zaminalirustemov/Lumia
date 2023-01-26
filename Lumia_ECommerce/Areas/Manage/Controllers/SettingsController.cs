using Lumia_ECommerce.Context;
using Lumia_ECommerce.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace Lumia_ECommerce.Areas.Manage.Controllers;
[Area("Manage")]
[Authorize(Roles = "SuperAdmin,Admin,Editor")]
public class SettingsController : Controller
{
    private readonly LumiaDbContext _lumiaDbContext;

    public SettingsController(LumiaDbContext lumiaDbContext)
    {
        _lumiaDbContext = lumiaDbContext;
    }
    public IActionResult Index()
    {
        List<Settings> settings = _lumiaDbContext.Settings.ToList();
        return View(settings);
    }
    //Update------------------------------------------------
    public IActionResult Update(int id)
    {
        Settings settings = _lumiaDbContext.Settings.FirstOrDefault(x => x.Id == id);
        if(settings==null) return View("Error");
        return View(settings);
    }
    [HttpPost]
    public IActionResult Update(Settings newSettings)
    {
        Settings existSettings = _lumiaDbContext.Settings.FirstOrDefault(x => x.Id == newSettings.Id);
        if(existSettings==null) return View("Error");

        existSettings.Value=newSettings.Value;

        _lumiaDbContext.SaveChanges();
        return RedirectToAction(nameof(Index));
    }

}

