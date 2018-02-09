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
    public class CartsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        public ActionResult Index()
        {
            if (Session[User.Identity.Name] != null)
            {
                Sessions temp = (Sessions)Session[User.Identity.Name];

                List<Product> Carts = new List<Product>();

                foreach (var item in temp.Apps.Keys.ToList())
                    Carts.Add(db.Products.Find(item));

                return View(Carts.ToList());
            }
            else
            {
                Sessions temp = new Sessions
                {
                    UserEmail = User.Identity.Name,
                    Apps = new Dictionary<int, Product>()
                };

                Session.Add(User.Identity.Name, temp);
            }

            List<Product> Temp = new List<Product>();

            return View(Temp.ToList());


            /*
            //Vi kollar ifall det redan finns inkorg ala session
            if (Session["Cart"] == null)
                Session["Cart"] = new List<int>();

            //Skapar lista från session
            List<int> MyApps = new List<int>((List<int>)Session["Cart"]);
            */
        }

        // GET: Carts/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Carts cart = db.Carts.Find(id);
            if (cart == null)
            {
                return HttpNotFound();
            }
            return View(cart);
        }

        // GET: Carts/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Carts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "UserId")] Carts cart)
        {
            if (ModelState.IsValid)
            {
                db.Carts.Add(cart);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(cart);
        }

        // GET: Carts/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Carts cart = db.Carts.Find(id);
            if (cart == null)
            {
                return HttpNotFound();
            }
            return View(cart);
        }

        // POST: Carts/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "UserId")] Carts cart)
        {
            if (ModelState.IsValid)
            {
                db.Entry(cart).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(cart);
        }

        // GET: Carts/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }


            //Vi kollar ifall det finns en session först :P finns det inte = ingen varukorg = inga appar = ERROR :(
            if (Session[User.Identity.Name] == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Sessions temp = (Sessions)Session[User.Identity.Name];

            if (!temp.Apps.ContainsKey(id.Value))
            {
                return HttpNotFound();
            }

            return View();
        }

        // POST: Carts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            /*
            Cart cart = db.Carts.Find(id);
            db.Carts.Remove(cart);
            db.SaveChanges();
            */

            if (Session[User.Identity.Name] == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Sessions temp = (Sessions)Session[User.Identity.Name];

            if (temp.Apps.ContainsKey(id))
                temp.Apps.Remove(id);

            temp.TotalPrice -= db.Products.Find(id).Price;

            //Ersätter gamla med nya uppdaterade session
            Session.Add(User.Identity.Name, temp);

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
