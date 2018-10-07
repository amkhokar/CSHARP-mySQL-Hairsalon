using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using HairSalon.Models;

namespace HairSalon.Controllers
{
    public class SpecialtiesController : Controller
    {
        [HttpGet("/specialty")]
        public ActionResult Index()
        {
            List<Specialty> allspecialties = Specialty.GetAll();
            return View(allspecialties);
        }
        [HttpGet("/specialty/new")]
        public ActionResult CreateForm()
        {
            return View();
        }
        [HttpGet("/specialty/{specialtiesId}")]
        public ActionResult Details(int specialtiesId)
        {
            Dictionary<string, object> model = new Dictionary<string, object>();
            Specialty specialty = Specialty.Find(specialtiesId);
            List<Stylist> allStylists = Stylist.GetAllStylist();
            List<Stylist> stylist = specialty.GetStylist();
            model.Add("specialty", specialty);
            model.Add("specialtyStylist", stylist);
            model.Add("allStylists", allStylists);
            return View(model);
        }
        [HttpPost("/specialty/{specialtiesId}")]
        public ActionResult AddStylistToSpecialty(int specialtiesId)
        {
            Specialty specialty = Specialty.Find(specialtiesId);
            Stylist stylist = Stylist.Find(int.Parse(Request.Form["stylist-id"]));
            specialty.AddStylist(stylist);
            return RedirectToAction("Details", new { id = specialtiesId });
        }
    }
}
