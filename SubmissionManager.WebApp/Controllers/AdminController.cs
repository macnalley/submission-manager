using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using SubmissionManager.WebApp.Models;
using SubmissionManager.Data.Entities;
using SubmissionManager.Data;
using Microsoft.EntityFrameworkCore;
using System.Net;

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
    public async Task<IActionResult> Index()
    {
        return View(await _context.Submissions
            .Include(s => s.Document)
            .ToListAsync());
    }

    [HttpGet]
    public async Task<IActionResult> Download(int id)
    {
        if (id != null)
        {
            var submission = await _context.GetByIdAsync(id);

            }  
        return View();
    }
}