using Lumia_ECommerce.Context;
using Lumia_ECommerce.Models;
using Lumia_ECommerce.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Lumia_ECommerce.Controllers;
public class HomeController : Controller
{
    private readonly LumiaDbContext _lumiaDbContext;

    public HomeController(LumiaDbContext lumiaDbContext)
    {
        _lumiaDbContext = lumiaDbContext;
    }
    public IActionResult Index()
    {
        HomeViewModel homeViewModel=new HomeViewModel
        {
            Teams= _lumiaDbContext.Teams.Include(x => x.Position).Where(x => x.isDeleted == false).ToList()
        };
        return View(homeViewModel);
    }

}
