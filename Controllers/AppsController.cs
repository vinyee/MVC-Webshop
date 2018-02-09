using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Web;
using System.Web.Mvc;
using DemoWithUsers.Models;

namespace DemoWithUsers.Controllers
{
    public class AppsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Apps
        public ActionResult Index()
        {
            return View(db.Products.ToList());
        }

        public ActionResult AddToBasket(int? id)
        {
            if (Session[User.Identity.Name] == null)
            {
                Sessions temp = new Sessions
                {
                    UserEmail = User.Identity.Name,
                    Apps = new Dictionary<int, Product>()                 
                };

                if (!temp.Apps.ContainsKey((id.Value)))
                {
                    temp.Apps.Add(id.Value, db.Products.Find(id));

                    temp.TotalPrice += db.Products.Find(id).Price;
                    TempData["msg"] = "<script>alert('Denna app har lagts till i din appkorg!');</script>";
                }
                else
                    TempData["msg"] = "<script>alert('Denna app finns redan i din appkorg!');</script>";

                Session.Add(User.Identity.Name, temp);
            }
            else
            {
                Sessions temp = (Sessions)Session[User.Identity.Name];

                if (!temp.Apps.ContainsKey((id.Value)))
                {
                    temp.Apps.Add(id.Value, db.Products.Find(id));
                    temp.TotalPrice += db.Products.Find(id).Price;

                    TempData["msg"] = "<script>alert('Denna app har lagts till i din appkorg!');</script>";
                }
                else
                    TempData["msg"] = "<script>alert('Denna app finns redan i din appkorg!');</script>";

                Session[User.Identity.Name] = temp;
            }
            return RedirectToAction("Index");
        }

        // GET: Apps/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // GET: Apps/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Apps/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,Price, ImageUrl")] Product product)
        {
            if (ModelState.IsValid)
            {
                db.Products.Add(product);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(product);
        }

        // GET: Apps/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // POST: Apps/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,Price")] Product product)
        {
            if (ModelState.IsValid)
            {
                db.Entry(product).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(product);
        }

        // GET: Apps/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // POST: Apps/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Product product = db.Products.Find(id);
            db.Products.Remove(product);
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
