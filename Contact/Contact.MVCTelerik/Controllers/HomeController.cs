using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Contact.MVC.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Contact.MVC.Filters;
using Contact.Application.CQRS.Core;
using Contact.Application.Models.Response;
using Newtonsoft.Json;
using System.Net;
using System.Text;

namespace Contact.MVC.Controllers;

[Auth]
public class HomeController : Controller
{
    public HomeController()
    {

    }

    public async Task<IActionResult> Index()
    {
        return View();
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}

