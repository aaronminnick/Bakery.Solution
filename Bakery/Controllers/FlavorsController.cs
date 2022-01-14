using Bakery.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Bakery.Controllers
{
  public class FlavorsController : Controller
  {
    private readonly BakeryContext _db;

    public FlavorsController(BakeryContext db)
    {
      _db = db;
    }

    public ActionResult Create()
    {
      return View();
    }

    [HttpPost]
    public ActionResult Create(Flavor flavor)
    {
      _db.Flavors.Add(flavor);
      _db.SaveChanges();
      return RedirectToAction("Index", "Home");
    }

    public ActionResult Details(int id)
    {
      Flavor model = _db.Flavors.FirstOrDefault(flavor => flavor.FlavorId == id);
      return View(model);
    }

    [HttpPost]
    public ActionResult Edit(Flavor flavor)
    {
      _db.Entry(flavor).State = EntityState.Modified;
      _db.SaveChanges();
      return RedirectToAction("Details", new {id = flavor.FlavorId});
    }

    [HttpPost]
    public ActionResult Delete(int FlavorId)
    {
      Flavor target = _db.Flavors.FirstOrDefault(flavor => flavor.FlavorId == FlavorId);
      _db.Flavors.Remove(target);
      _db.SaveChanges();
      return RedirectToAction("Index", "Home");
    }
  }
}