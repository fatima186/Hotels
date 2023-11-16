using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Hotels.Models;
using Hotels.Data;

namespace Hotels.Controllers;

public class HomeController : Controller
{
    private readonly ApplicationDbContext _context;

    public HomeController(ApplicationDbContext context){
        _context = context;
            
    }
    public IActionResult CreateNewRecord(Hotel hotels){

        if (ModelState.IsValid)
        {
            _context.hotel.Add(hotels);
            _context.SaveChanges();
            return RedirectToAction("Index");

        }

        var hotel = _context.hotel.ToList();
        return View("Index", hotel);
    }
    public IActionResult Delete(int id){
       
        var hotelDelete = _context.hotel.SingleOrDefault(x => x.Id == id); //search
        
        _context.hotel.Remove(hotelDelete); // Delete
        _context.SaveChanges();
        return RedirectToAction("Index");

    }
    public IActionResult Edit(int id){
        var hotelEdit = _context.hotel.SingleOrDefault(x => x.Id == id);
        return View(hotelEdit);
    }
    public IActionResult Update(Hotel hotel)
    {
        _context.hotel.Update(hotel);
        _context.SaveChanges();
        return RedirectToAction("Index");
    }

    [HttpPost]
    public IActionResult Index(string city)
    {
        var hotelFilter = _context.hotel.Where(x => x.City.Contains(city));
        return View(hotelFilter);
    }

    public IActionResult Index()
    {
        var hotel = _context.hotel.ToList();

        return View(hotel);
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

