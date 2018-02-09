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
    public class OrderDetailsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: OrderDetails
        public ActionResult Index()
        {
            List<OrderDetailViewModels> OrderDetailsViews = new List<OrderDetailViewModels>();

            foreach (var item in db.OrderDetails.ToList())
            {
                if (item != null)
                {
                    //så vi bara listar rätt persons ordrar, vi kollar samma email från session + ordermodellen ;)
                    if (User.Identity.Name == db.Orders.Where(x => x.OrderId == item.OrderId).FirstOrDefault().UserIdentity)
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
            }

            return View(OrderDetailsViews.ToList());
        }

        // GET: OrderDetails/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

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

            //ingen orderdetail, kunde ej hitta ordern så vi skickar errorpage
            if (temp == null)
                return HttpNotFound();

            return View(temp);
        }

        // GET: OrderDetails/Create
        public ActionResult Create()
        {
            OrderDetailViewModels orderDetailViews = new OrderDetailViewModels();
            Product product;

            if (Session[User.Identity.Name] == null)
            {
                return RedirectToAction("Index", "Carts");
            }
            else
            {
                Sessions temp = (Sessions)Session[User.Identity.Name];

                //Vi har noll appar, varför skapa order då? :O
                if (temp.TotalPrice == 0)
                    return RedirectToAction("Index", "Carts");

                orderDetailViews.TotalPrice = temp.TotalPrice;
                orderDetailViews.Apps = new List<Product>();

                //Loopar igenom apparna vi har från session -> appkorg
                foreach (var item in temp.Apps.Keys.ToList())
                {
                    //sparar appen i ett temp-objekt
                    product = db.Products.Find(item);

                    //extra kontroll
                    if (product != null)
                    {
                        //denna är onödig egentligen då jag inte kodat cache som lever under programmets gång men saksamma, redan kodat detta så behåller det
                        //nu lägger vi in appen i vårt orderobjekt så vi kan spara ordern i databasen, extrakontroll bara innan vi lägger till
                        if (!orderDetailViews.Apps.Contains(product))
                        {
                            //lägger till appen i cache samt orderItem objekt som sparas vidare i databasen sen ifall cachen försvinner
                            orderDetailViews.Apps.Add(product);
                        }
                    }
                }
            }

            return View(orderDetailViews);
        }


        // GET: OrderDetails/Create
        public ActionResult Checkout()
        {
            OrderDetailViewModels orderDetailViews = new OrderDetailViewModels();
            Product product;

            if (Session[User.Identity.Name] == null)
            {
                return RedirectToAction("Index", "Carts");
            }
            else
            {
                Sessions temp = (Sessions)Session[User.Identity.Name];

                //Vi har noll appar, varför skapa order då? :O
                if (temp.TotalPrice == 0)
                    return RedirectToAction("Index", "Carts");

                orderDetailViews.TotalPrice = temp.TotalPrice;
                orderDetailViews.Apps = new List<Product>();

                //Loopar igenom apparna vi har från session -> appkorg
                foreach (var item in temp.Apps.Keys.ToList())
                {
                    //sparar appen i ett temp-objekt
                    product = db.Products.Find(item);

                    //extra kontroll
                    if (product != null)
                    {
                        //denna är onödig egentligen då jag inte kodat cache som lever under programmets gång men saksamma, redan kodat detta så behåller det
                        //nu lägger vi in appen i vårt orderobjekt så vi kan spara ordern i databasen, extrakontroll bara innan vi lägger till
                        if (!orderDetailViews.Apps.Contains(product))
                        {
                            //lägger till appen i cache samt orderItem objekt som sparas vidare i databasen sen ifall cachen försvinner
                            orderDetailViews.Apps.Add(product);
                        }
                    }
                }
            }

            return View(orderDetailViews);
        }

        // POST: OrderDetails/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,OrderId,Firstname,Lastname,Address,PostalCode,City,PhoneNumber")] OrderDetail orderDetail)
        {
            if (ModelState.IsValid)
            {
                if (Session[User.Identity.Name] != null)
                {
                    OrderDetailViewModels orderDetailViews = new OrderDetailViewModels();
                    Sessions temp = (Sessions)Session[User.Identity.Name];

                    //Generera vår unika id, inte bästa sättet men orkade ej byta alla int till sträng när jag såg guid generator
                    int Id = new Random().Next();
                    Product product;

                    orderDetail.Id = Id; //vet ej varför men blir error utan? :S
                    orderDetail.OrderId = Id;

                    //jobbigt men vi måste göra en kopia av vyn för kvitto som innehåller applista
                    orderDetailViews.Id = Id;
                    orderDetailViews.OrderId = Id;
                    orderDetailViews.Firstname = orderDetail.Firstname;
                    orderDetailViews.Lastname = orderDetail.Lastname;
                    orderDetailViews.Address = orderDetail.Address;
                    orderDetailViews.PostalCode = orderDetail.PostalCode;
                    orderDetailViews.City = orderDetail.City;
                    orderDetailViews.PhoneNumber = orderDetail.PhoneNumber;
                    orderDetailViews.Apps = new List<Product>();

                    //Vi måste spara varje app i databasen, går ju inte att spara lista i ett fält i databasen :O
                    OrderItem orderItem = new OrderItem
                    {
                        OrderId = Id
                    };

                    //Ny order, här sparar vi info om själva ordern, datum, totalpris m.m, denna behövs inte heller bara om vi skulle cacha allt i en lista som går igenom hela programmet
                    OrderViewModels orderViews = new OrderViewModels
                    {
                        OrderTotalPrice = temp.TotalPrice,
                        UserIdentity = User.Identity.Name,
                        OrderId = Id,
                        Apps = new List<int>()
                    };

                    //Samma som uppe fast för databasen utan applistan
                    Order order = new Order
                    {
                        OrderTotalPrice = temp.TotalPrice,
                        UserIdentity = User.Identity.Name,
                        OrderId = Id,
                    };

                    //Loopar igenom apparna vi har från session -> appkorg
                    foreach (var item in temp.Apps.Keys.ToList())
                    {
                        //sparar appen i ett temp-objekt
                        product = db.Products.Find(item);

                        //extra kontroll
                        if (product != null)
                        {
                            //spara appen så vi kan visa kvittot med appen sen
                            if (!orderDetailViews.Apps.Contains(product))
                                orderDetailViews.Apps.Add(product);

                            orderItem.AppId = product.Id;

                            //Sparar appen som tillhör denna orderId i databasen
                            db.OrderItems.Add(orderItem);

                            //vi måste spara varje gång i loopen annars ersätts ju objektet varje gång utan att det sparas :P
                            db.SaveChanges();
                        }
                    }

                    db.Orders.Add(order);


                    /* vi måste ju rensa varukorgen också :o*/
                    temp.Apps.Clear();
                    temp.TotalPrice = 0;

                    //Ersätter gamla med nya uppdaterade session
                    Session.Add(User.Identity.Name, temp);

                    db.OrderDetails.Add(orderDetail);
                    db.SaveChanges();

                    return View("Checkout", orderDetailViews);
                }

            }

            return View(orderDetail);
        }

        // GET: OrderDetails/Edit/5
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

        // POST: OrderDetails/Edit/5
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

        // GET: OrderDetails/Delete/5
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

        // POST: OrderDetails/Delete/5
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
