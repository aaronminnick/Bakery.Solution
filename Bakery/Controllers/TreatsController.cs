using Bakery.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Bakery.Controllers
{
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

    public ActionResult Details(int id)
    {
      Treat model = _db.Treats.FirstOrDefault(treat => treat.TreatId == id);
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
  }
}