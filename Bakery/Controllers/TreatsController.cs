using Bakery.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Bakery.Controllers
{
  [Authorize]
  public class TreatsController : Controller
  {
    private readonly BakeryContext _db;

    public TreatsController(BakeryContext db)
    {
      _db = db;
    }

    public ActionResult Create()
    {
      return View();
    }

    [HttpPost]
    public ActionResult Create(Treat treat)
    {
      _db.Treats.Add(treat);
      _db.SaveChanges();
      return RedirectToAction("Index", "Home");
    }

    [AllowAnonymous]
    public ActionResult Details(int id)
    {
      ViewData["flavors"] = _db.Flavors.ToList();
      Treat model = _db.Treats
        .Include(treat => treat.FlavorTreats)
        .ThenInclude(ft => ft.Flavor)
        .FirstOrDefault(treat => treat.TreatId == id);
      return View(model);
    }

    [HttpPost]
    public ActionResult Edit(Treat treat)
    {
      _db.Entry(treat).State = EntityState.Modified;
      _db.SaveChanges();
      return RedirectToAction("Details", new {id = treat.TreatId});
    }

    [HttpPost]
    public ActionResult Delete(int TreatId)
    {
      Treat target = _db.Treats.FirstOrDefault(treat => treat.TreatId == TreatId);
      _db.Treats.Remove(target);
      _db.SaveChanges();
      return RedirectToAction("Index", "Home");
    }

    [HttpPost]
    public PartialViewResult AddFlavorTreat(int TreatId, int FlavorId)
    {
      _db.FlavorTreats.Add(new FlavorTreat() {FlavorId = FlavorId, TreatId = TreatId});
      _db.SaveChanges();
      ViewData["flavors"] = _db.Flavors.ToList();
      Treat model = _db.Treats
        .Include(treat => treat.FlavorTreats)
        .ThenInclude(ft => ft.Flavor)
        .FirstOrDefault(treat => treat.TreatId == TreatId);
      return PartialView("_FlavorTreatsPartial", model);
    }

    [HttpPost]
    public PartialViewResult RemoveFlavorTreat(int TreatId, int FlavorId)
    {
      FlavorTreat target = _db.FlavorTreats.FirstOrDefault(ft => ft.FlavorId == FlavorId && ft.TreatId == TreatId);
      _db.FlavorTreats.Remove(target);
      _db.SaveChanges();
      ViewData["flavors"] = _db.Flavors.ToList();
      Treat model = _db.Treats
        .Include(treat => treat.FlavorTreats)
        .ThenInclude(ft => ft.Flavor)
        .FirstOrDefault(treat => treat.TreatId == TreatId);
      return PartialView("_FlavorTreatsPartial", model);
    }
  }
}