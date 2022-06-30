using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using SubmissionManager.WebApp.Models;
using SubmissionManager.Data.Entities;
using SubmissionManager.Data;

namespace SubmissionManager.WebApp.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly SubmissionContext _context;
    private readonly IHostEnvironment _environment;

    public HomeController(ILogger<HomeController> logger, SubmissionContext context, IHostEnvironment environment)
    {
        _logger = logger;
        _context = context;
        _environment = environment;
    }

    public IActionResult Index()
    {
        return View();
    }

    [HttpGet]
    public IActionResult Submit()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Submit(Submission model)
    {
        if (ModelState.IsValid)
        {
            var file = model.Document.File;

            if (file.Length > 0)
            {
                var dir = new DirectoryInfo(_environment.ContentRootPath).Parent + @"\Uploads";
                var newFileName = $"{model.Author.Replace(" ","")}_{model.Title.Replace(" ","")}{Path.GetExtension(file.FileName)}";
                var path = Path.Combine(dir, newFileName);

                using (var fileStream = new FileStream(path, FileMode.Create, FileAccess.Write))
                {
                    await file.CopyToAsync(fileStream);
                }

                model.Document.DocumentPath = path;
            }

            _context.Add(model);
            await _context.SaveChangesAsync();

            return RedirectToAction("Index");
        }
        else return View();
        
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
