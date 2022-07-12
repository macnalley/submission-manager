using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using SubmissionManager.WebApp.Models;
using SubmissionManager.Data.Entities;
using SubmissionManager.Data;
using Microsoft.EntityFrameworkCore;
using System.Net;
using System.IO;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace SubmissionManager.WebApp.Controllers;

public class AdminController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly SubmissionContext _context;

    public AdminController(ILogger<HomeController> logger, SubmissionContext context)
    {
        _logger = logger;
        _context = context;
    }

    [HttpGet]
    public async Task<IActionResult> Index(string filter)
    {
        var status = new Status();
        var submissions = new List<Submission>();

        switch (filter)
        {
            case "New":
                status = Status.New;
                break;
            case "Advanced":
                status = Status.Advanced;
                break;
            case "Rejected":
                status = Status.Rejected;
                break;
            case "Accepted":
                status = Status.Accepted;
                break;
            default:
                status = Status.New;
                break;
        }
        
        if (filter == "All")
        {
            submissions = await _context.Submissions
            .Include(s => s.Document)
            .ToListAsync();
        }
        else
        {
            submissions = await _context.Submissions
            .Where(s => s.Status == status)
            .Include(s => s.Document)
            .ToListAsync();
        }
        
        return View(submissions);
    }

    [HttpGet("download")]
    public async Task<IActionResult> Download(int id)
    {
        if (id != null)
        {
            var submission = await _context.GetByIdAsync(id);

            var file = submission.Document.DocumentPath;

            var fileContents = System.IO.File.ReadAllBytes(file);

            return File(fileContents, "application/octet-stream", submission.Document.FileName);
        }
        else return RedirectToAction("Index");  
    }

    [HttpGet]
    public async Task<IActionResult> Details(int id)
    {
        var model = await _context.GetByIdAsync(id);

        return View(model);
    }    
    

    public async Task<IActionResult> Advance(int id)
    {
        var model = await _context.GetByIdAsync(id);

        model.Status = Status.Advanced;

        _context.SaveChanges();
        
        return RedirectToAction("Index");
    }

    public async Task<IActionResult> Reject(int id)
    {
        var model = await _context.GetByIdAsync(id);

        model.Status = Status.Rejected;

        _context.SaveChanges();
        
        return RedirectToAction("Index");
    }

    public async Task<IActionResult> Accept(int id)
    {
        var model = await _context.GetByIdAsync(id);

        model.Status = Status.Accepted;

        _context.SaveChanges();
        
        return RedirectToAction("Index");
    }

}