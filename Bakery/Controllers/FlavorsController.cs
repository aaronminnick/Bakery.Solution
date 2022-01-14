using Bakery.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Bakery.Controllers
{
  [Authorize]
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

    [AllowAnonymous]
    public ActionResult Details(int id)
    {
      ViewData["treats"] = _db.Treats.ToList();
      Flavor model = _db.Flavors
        .Include(flavor => flavor.FlavorTreats)
        .ThenInclude(ft => ft.Treat)
        .FirstOrDefault(flavor => flavor.FlavorId == id);
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

    [HttpPost]
    public PartialViewResult AddFlavorTreat(int TreatId, int FlavorId)
    {
      _db.FlavorTreats.Add(new FlavorTreat() {FlavorId = FlavorId, TreatId = TreatId});
      _db.SaveChanges();
      ViewData["treats"] = _db.Treats.ToList();
      Flavor model = _db.Flavors
        .Include(flavor => flavor.FlavorTreats)
        .ThenInclude(ft => ft.Treat)
        .FirstOrDefault(flavor => flavor.FlavorId == FlavorId);
      return PartialView("_FlavorTreatsPartial", model);
    }

    [HttpPost]
    public PartialViewResult RemoveFlavorTreat(int TreatId, int FlavorId)
    {
      FlavorTreat target = _db.FlavorTreats.FirstOrDefault(ft => ft.FlavorId == FlavorId && ft.TreatId == TreatId);
      _db.FlavorTreats.Remove(target);
      _db.SaveChanges();
      ViewData["treats"] = _db.Treats.ToList();
      Flavor model = _db.Flavors
        .Include(flavor => flavor.FlavorTreats)
        .ThenInclude(ft => ft.Treat)
        .FirstOrDefault(flavor => flavor.FlavorId == FlavorId);
      return PartialView("_FlavorTreatsPartial", model);
    }
  }
}