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
    [Authorize(Roles = "Admin")]
    public class OrderDetailsAdminController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: OrderDetailsAdmin
        public ActionResult Index()
        {
            List<OrderDetailViewModels> OrderDetailsViews = new List<OrderDetailViewModels>();

            foreach (var item in db.OrderDetails.ToList())
            {
                if (item != null)
                {
                    OrderDetailViewModels temp = new OrderDetailViewModels
                    {
                        Id = item.Id,
                        Firstname = item.Firstname,
                        Lastname = item.Lastname,
                        OrderId = item.OrderId,
                        PhoneNumber = item.PhoneNumber,
                        PostalCode = item.PostalCode,
                        Address = item.Address,
                        City = item.City,
                        Email = db.Orders.Where(x => x.OrderId == item.OrderId).FirstOrDefault().UserIdentity,

                        //så vi får fram totalprice från tabellen orders till vår vymodell som ärvt från OrderDetail :D
                        TotalPrice = db.Orders.Where(x => x.OrderId == item.OrderId).FirstOrDefault().OrderTotalPrice
                    };

                    //Sparar objektet i listan som vi sedan skickar till vyn med TotalPrice :D
                    OrderDetailsViews.Add(temp);
                }
            }

            return View(OrderDetailsViews.ToList());
        }

        //tog direkt från vanliga OrderDetails
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            OrderDetail orderDetail = db.OrderDetails.Find(id);
            OrderDetailViewModels temp = new OrderDetailViewModels
            {
                Apps = new List<Product>()
            };

            //använder orderdetailviewmodels så vi kan få fram totalprice också
            if (orderDetail != null)
            {

                temp.Id = orderDetail.Id;
                temp.Firstname = orderDetail.Firstname;
                temp.Lastname = orderDetail.Lastname;
                temp.OrderId = orderDetail.OrderId;
                temp.PhoneNumber = orderDetail.PhoneNumber;
                temp.PostalCode = orderDetail.PostalCode;
                temp.Address = orderDetail.Address;
                temp.City = orderDetail.City;

                //så vi får fram totalprice från tabellen orders till vår vymodell som ärvt från OrderDetail :D
                temp.TotalPrice = db.Orders.Where(x => x.OrderId == orderDetail.OrderId).FirstOrDefault().OrderTotalPrice;

                foreach (var dbItem in db.OrderItems.ToList())
                {
                    if (dbItem.OrderId == orderDetail.OrderId)
                    {
                        Product pro = db.Products.Find(dbItem.AppId);
                        temp.Apps.Add(pro);
                    }
                }
            }

            if (temp == null)
                return HttpNotFound();

            return View(temp);
        }

        // POST: OrderDetailsAdmin/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,OrderId,Firstname,Lastname,Address,PostalCode,City,PhoneNumber")] OrderDetail orderDetail)
        {
            if (ModelState.IsValid)
            {
                db.OrderDetails.Add(orderDetail);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(orderDetail);
        }

        // GET: OrderDetailsAdmin/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OrderDetail orderDetail = db.OrderDetails.Find(id);
            if (orderDetail == null)
            {
                return HttpNotFound();
            }
            return View(orderDetail);
        }

        // POST: OrderDetailsAdmin/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,OrderId,Firstname,Lastname,Address,PostalCode,City,PhoneNumber")] OrderDetail orderDetail)
        {
            if (ModelState.IsValid)
            {
                db.Entry(orderDetail).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(orderDetail);
        }

        // GET: OrderDetailsAdmin/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OrderDetail orderDetail = db.OrderDetails.Find(id);
            if (orderDetail == null)
            {
                return HttpNotFound();
            }
            return View(orderDetail);
        }

        // POST: OrderDetailsAdmin/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            OrderDetail orderDetail = db.OrderDetails.Find(id);
            db.OrderDetails.Remove(orderDetail);
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
