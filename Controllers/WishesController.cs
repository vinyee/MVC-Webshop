using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using DemoWithUsers.Models;

namespace DemoWithUsers.Controllers
{
    public class WishesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Wishes
        public ActionResult Index()
        {
            List<Product> Apps = new List<Product>();

            if (Session[User.Identity.Name] == null)
            {
                Sessions temp = new Sessions
                {
                    UserEmail = User.Identity.Name,
                    Apps = new Dictionary<int, Product>()
                };

                Session.Add(User.Identity.Name, temp);
            }

            //hitta alla tabeller i wishes som tillhör dig ala samma email :O
            foreach (var item in db.Wishes.Where(x => x.UserIdentity == User.Identity.Name).ToList())
            {
                //då vi har fått fram id från varje wishes där email stämmer med inloggade
                Product app = db.Products.Find(item.AppId);

                //extra kontroll + lägger till i listan som vi skickar till vyn :D
                if (app != null)
                    if (!Apps.Contains(app))
                        Apps.Add(app);
            }

            return View(Apps.ToList());
        }

        public ActionResult AddToBasket(int? id)
        {

            Wish wish = db.Wishes.Find(id);

            //ifall wish är null betyder vi ej har appen i listan
            if (wish == null)
            {
                wish = new Wish
                {
                    AppId = id.Value,
                    UserIdentity = User.Identity.Name
                };

                db.Wishes.Add(wish);
                db.SaveChanges();

                TempData["msg"] = "<script>alert('Denna app har lagts till i din önskelista!');</script>";
            }
            else
            {
                TempData["msg"] = "<script>alert('Denna app finns redan i din önskelista!');</script>";
            }

            return RedirectToAction("Details", "Apps", new {id = id.Value });
        }

        // GET: Wishes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            //Vi går direkt till appvyn istället
            return RedirectToAction("Details", "Apps", new { id = id.Value });
        }

        // GET: Wishes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Wishes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,UserIdentity,AppId")] Wish wish)
        {
            if (ModelState.IsValid)
            {
                db.Wishes.Add(wish);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(wish);
        }

        // GET: Wishes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Wish wish = db.Wishes.Find(id);
            if (wish == null)
            {
                return HttpNotFound();
            }
            return View(wish);
        }

        // POST: Wishes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,UserIdentity,AppId")] Wish wish)
        {
            if (ModelState.IsValid)
            {
                db.Entry(wish).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(wish);
        }

        // GET: Wishes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Product wish = db.Products.Find(id);

            return View(wish);
        }

        // POST: Wishes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Wish wish = new Wish();

            //lite jobbigt men vi behöver loopa igenom alla för det är appid och inte unika idn..
            foreach (var item in db.Wishes.Where(x => x.UserIdentity == User.Identity.Name).ToList())
            {
                //vi kollar så det är appen vi söker
                if (item.AppId == id)
                {
                    //får fram wishobjektet med rätt id nu
                    wish = db.Wishes.Find(item.Id);
                    if (wish != null)
                        break; //avslutar loopen då vi hittat
                }
            }

            db.Wishes.Remove(wish);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
