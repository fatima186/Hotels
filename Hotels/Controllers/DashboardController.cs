using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Hotels.Data;
using Hotels.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Hotels.Controllers
{
    public class DashboardController : Controller
    {

        // GET: /<controller>/

        private readonly ApplicationDbContext _context;
        public DashboardController(ApplicationDbContext context)
        {
            _context = context;

        }

        public IActionResult CreateNewHotel(Hotel hotels)
        {
            if (ModelState.IsValid)
            {
                _context.hotel.Add(hotels);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            var hotel = _context.hotel.ToList();
            return View("Index", hotel);

        }
        public IActionResult Delete(int id)
        {
            var hotelDelete = _context.hotel.SingleOrDefault(x => x.Id == id);
            if (hotelDelete != null)
            {
                _context.hotel.Remove(hotelDelete);
                _context.SaveChanges();
                TempData["Del"] = "Ok";
            }
            return RedirectToAction("Index");
        }
        public IActionResult DeleteRoom(int id)
        {
            var roomDelete = _context.rooms.SingleOrDefault(x => x.Id == id);
            if (roomDelete != null)
            {
                _context.rooms.Remove(roomDelete);
                _context.SaveChanges();
                TempData["Del"] = "Ok";
            }
            return RedirectToAction("Rooms");
        }
        public IActionResult DeleteRoomDetails(int id)
        {
            var roomDetailsDelete = _context.roomDetails.SingleOrDefault(x => x.Id == id);
            if (roomDetailsDelete != null)
            {
                _context.roomDetails.Remove(roomDetailsDelete);
                _context.SaveChanges();
                TempData["Del"] = "Ok";
            }
            return RedirectToAction("RoomDetails");
        }
        public IActionResult Edit(int id)
        {
            var hotelEdit = _context.hotel.SingleOrDefault(x => x.Id == id);
            return View(hotelEdit);
        }
        public IActionResult EditRoom(int id)
        {
            var hotel = _context.hotel.ToList();
            ViewBag.hotel = hotel;

            var roomEdit = _context.rooms.SingleOrDefault(x => x.Id == id);
            return View(roomEdit);
        }

        public IActionResult EditRoomDetails(int id)
        {
            var hotel = _context.hotel.ToList();
            ViewBag.hotel = hotel;

            var rooms = _context.rooms.ToList();
            ViewBag.rooms = rooms;

            var roomDetailsEdit = _context.roomDetails.SingleOrDefault(x => x.Id == id);
            return View(roomDetailsEdit);
        }

        public IActionResult Update(Hotel hotel)
        {
            _context.hotel.Update(hotel);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
        


        public IActionResult UpdateRoom(Rooms room)
        {
            _context.rooms.Update(room);
            _context.SaveChanges();
            return RedirectToAction("Rooms");
        }


        public IActionResult UpdateRoomDetails(RoomDetails roomDetails)
        {
            _context.roomDetails.Update(roomDetails);
            _context.SaveChanges();
            return RedirectToAction("RoomDetails");
        }

        [HttpPost]
        public IActionResult Index(string city)
        {
            if (city == "الكل")
            {
                var hotel = _context.hotel.ToList();
                return View(hotel);
            }
            else
            {
                var hotelFilter = _context.hotel.Where(x => x.City.Contains(city));
                return View(hotelFilter);
            }
        }
        public IActionResult Index()
        {

            var hotel = _context.hotel.ToList();

            return View(hotel);
        }
        
        public IActionResult Rooms()
        {
            var hotel = _context.hotel.ToList();
            ViewBag.hotel = hotel;

            var rooms = _context.rooms.ToList();
            return View(rooms);
        }
        public IActionResult CreateNewRooms(Rooms rooms)
        {
            _context.rooms.Add(rooms);
            _context.SaveChanges();
            return RedirectToAction("Rooms");
        }
        public IActionResult CreateNewRoomsDetails(RoomDetails roomDetails)
        {
            _context.roomDetails.Add(roomDetails);
            _context.SaveChanges();
            return RedirectToAction("RoomDetails");
        }
        public IActionResult RoomDetails()

        {
            var hotel = _context.hotel.ToList();
            ViewBag.hotel = hotel;


            var rooms = _context.rooms.ToList();
            ViewBag.rooms = rooms;

            var roomDetails = _context.roomDetails.ToList();
            return View(roomDetails);
        }
        
    }

}