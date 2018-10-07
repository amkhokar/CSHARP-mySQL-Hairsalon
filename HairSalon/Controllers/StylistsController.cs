using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using HairSalon.Models;

namespace HairSalon.Controllers
{
    public class StylistsController : Controller
    {
        [HttpGet("/stylist")]
        public ActionResult Index()
        {
            List<Stylist> allStylist = Stylist.GetAllStylist();
            return View(allStylist);
        }
        [HttpGet("/stylist/new")]
        public ActionResult CreateForm()
        {
            return View();
        }
        [HttpGet("/stylist/{id}")]
        public ActionResult Details(int id)
        {
            Dictionary<string, object> model = new Dictionary<string, object>();
            Stylist selectedStylist = Stylist.Find(id);
            List<Client> stylistClient = selectedStylist.GetClient();
            List<Specialty> stylistSpecialty = selectedStylist.GetSpecialty();
            List<Client> allClients = Client.GetAllClient();
            List<Specialty> allSpecialties = Specialty.GetAll();
            model.Add("stylist", selectedStylist);
            model.Add("stylistSpecialties", stylistSpecialty);
            model.Add("client", stylistClient);
            model.Add("allClient", allClients);
            model.Add("allSpecialties", allSpecialties);
            return View(model);
        }
        [HttpGet("/stylist/{stylistId}/update")]
        public ActionResult UpdateForm (int stylistId)
        {
            Dictionary<string, object> model = new Dictionary<string, object>();
            Stylist thisStylist = Stylist.Find(stylistId);
            model.Add("stylist", thisStylist);
            return View("UpdateForm", model);
        }
        [HttpGet("/stylist/delete")]
        public ActionResult DeleteAll()
        {
            Stylist.DeleteAll();
            return RedirectToAction("Index");
        }
        [HttpGet("/stylist/{stylistId}/delete")]
        public ActionResult DeleteOne(int stylistId)
        {
            Stylist thisStylist = Stylist.Find(stylistId);
            thisStylist.Delete();
            return RedirectToAction("Index");
        }
        [HttpPost("/stylist/{stylistId}/update")]
        public ActionResult UpdateStylist (int stylistId)
        {
            Stylist thisStylist = Stylist.Find(stylistId);
            thisStylist.Edit(Request.Form["new-stylist-name"]);
            return RedirectToAction("Index");
        }
        [HttpPost("/stylist/{stylistId}/specialty/new")]
        public ActionResult AddSpecialty (int stylistId)
        {
            Stylist stylist = Stylist.Find(stylistId);
            Specialty specialty = Specialty.Find(int.Parse(Request.Form["specialty-id"]));
            stylist.AddSpecialty(specialty);
            return RedirectToAction("Details", new {id = stylistId});
        }
        [HttpPost("/stylist/{stylistId}/client/new")]
        public ActionResult AddStylist (int stylistId)
        {
            Stylist stylist = Stylist.Find(stylistId);
            Client client = Client.Find(int.Parse(Request.Form["client-id"]));
            stylist.AddClient(client);
            return RedirectToAction("Details", new {id = stylistId});
        }
        [HttpPost("/stylist")]
        public ActionResult CreateStylist()
        {
            Stylist NewStylist = new Stylist(Request.Form["new-stylist"]);
            NewStylist.Save();
            List<Stylist> allStylist = Stylist.GetAllStylist();
            return RedirectToAction("Index");
        }
        [HttpPost("/client")]
        public ActionResult CreateClient(int stylistId)
        {
            Dictionary<string, object> model = new Dictionary<string, object>();
            Stylist foundStylist = Stylist.Find(stylistId);
            Client newClient = new Client(Request.Form["new-client"]);
            newClient.Save();
            foundStylist.AddClient(newClient);
            List<Client> stylistClient = foundStylist.GetClient();
            model.Add("client", stylistClient);
            model.Add("stylist", foundStylist);
            return RedirectToAction("Index", new{id = stylistId});
        }
        [HttpPost("/specialty")]
        public ActionResult CreateSpecialty(int stylistId)
        {
            Dictionary<string, object> model = new Dictionary<string, object>();
            Stylist foundStylist = Stylist.Find(stylistId);
            Specialty newSpecialty = new Specialty(Request.Form["new-specialties"]);
            newSpecialty.Save();
            foundStylist.AddSpecialty(newSpecialty);
            List<Specialty> stylistSpecialty = foundStylist.GetSpecialty();
            model.Add("specialty", stylistSpecialty);
            model.Add("stylist", foundStylist);
            return RedirectToAction("Index", new{id = stylistId});
        }
    }
}
