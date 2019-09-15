using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using FIT5032_AssignmentProject.Models;

namespace FIT5032_AssignmentProject.Controllers
{
    public class OrderMenusController : Controller
    {
        private Entities1 db = new Entities1();

        // GET: OrderMenus
        public ActionResult Index()
        {
            var orderMenus = db.OrderMenus.Include(o => o.Menu).Include(o => o.Order);
            return View(orderMenus.ToList());
        }

        // GET: OrderMenus/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OrderMenu orderMenu = db.OrderMenus.Find(id);
            if (orderMenu == null)
            {
                return HttpNotFound();
            }
            return View(orderMenu);
        }

        // GET: OrderMenus/Create
        public ActionResult Create()
        {
            ViewBag.DishId = new SelectList(db.Menus, "DishId", "Name");
            ViewBag.OrderId = new SelectList(db.Orders, "OrderId", "Category");
            return View();
        }

        // POST: OrderMenus/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "OrderMenuId,DishId,OrderId")] OrderMenu orderMenu)
        {
            if (ModelState.IsValid)
            {
                db.OrderMenus.Add(orderMenu);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.DishId = new SelectList(db.Menus, "DishId", "Name", orderMenu.DishId);
            ViewBag.OrderId = new SelectList(db.Orders, "OrderId", "Category", orderMenu.OrderId);
            return View(orderMenu);
        }

        // GET: OrderMenus/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OrderMenu orderMenu = db.OrderMenus.Find(id);
            if (orderMenu == null)
            {
                return HttpNotFound();
            }
            ViewBag.DishId = new SelectList(db.Menus, "DishId", "Name", orderMenu.DishId);
            ViewBag.OrderId = new SelectList(db.Orders, "OrderId", "Category", orderMenu.OrderId);
            return View(orderMenu);
        }

        // POST: OrderMenus/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "OrderMenuId,DishId,OrderId")] OrderMenu orderMenu)
        {
            if (ModelState.IsValid)
            {
                db.Entry(orderMenu).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.DishId = new SelectList(db.Menus, "DishId", "Name", orderMenu.DishId);
            ViewBag.OrderId = new SelectList(db.Orders, "OrderId", "Category", orderMenu.OrderId);
            return View(orderMenu);
        }

        // GET: OrderMenus/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OrderMenu orderMenu = db.OrderMenus.Find(id);
            if (orderMenu == null)
            {
                return HttpNotFound();
            }
            return View(orderMenu);
        }

        // POST: OrderMenus/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            OrderMenu orderMenu = db.OrderMenus.Find(id);
            db.OrderMenus.Remove(orderMenu);
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
