using System.Diagnostics;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PX.Models;
using System.Collections;


namespace PX.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    [HttpGet]
    public IActionResult Index()
    {
        return View();
    }

    [HttpPost]

    public IActionResult Privacy()
    {
        return View();
    }

    public IActionResult Search(string Search)
    {
        StreamReader read = new StreamReader("../px/wwwroot/images/tagsList.txt");
        List<string> arr = new List<string>();
        
        string? line;
        while ((line = read.ReadLine()) != null)
        {
            string[] tags = line.Split(",");
            for(int i = 1; i < tags.Length; i++)
            {
                if(tags[i] == Search)
                {
                    arr.Add(tags[0]);
                }
            }
        }
        if (arr.Count == 0)
        {
            return View("noSerchRes");
        }
        else { return View(arr); }
    }

    public IActionResult Add(string TagsForAddElem, IFormFile uploadedFile)
    {
        if (uploadedFile != null)
        {
            string fileName = uploadedFile.FileName;
            string path = "wwwroot/images/" + fileName;
            using (var fileStream = new FileStream(path, FileMode.Create))
                {
                uploadedFile.CopyToAsync(fileStream);
            }
                
        }
            
        return RedirectToAction("Index");
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
