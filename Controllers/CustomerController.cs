using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcCRUD.Models;

namespace MvcCRUD.Controllers
{
    public class CustomerController : Controller
    {
        // GET: /Customer/Index
        public ActionResult Index()
        {
            using (MvcCRUDEntities _MvcCRUD = new MvcCRUDEntities())
            {
                return View(_MvcCRUD.Customer.ToList());
            }

        }

        // GET: Customer/Details/5
        public ActionResult Details(int id)
        {
            using (MvcCRUDEntities _MvcCRUD = new MvcCRUDEntities())
            {
                return View(_MvcCRUD.Customer.Where(x => x.Id == id).FirstOrDefault());
            }
           
        }

        // GET: Customer/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Customer/Create
        [HttpPost]
        public ActionResult Create(Customer customer)
        {
            try
            {
                // TODO: Add insert logic here
                using (MvcCRUDEntities _MvcCRUD = new MvcCRUDEntities())
                {
                    _MvcCRUD.Customer.Add(customer);
                    _MvcCRUD.SaveChanges();
                }

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Customer/Edit/5
        public ActionResult Edit(int id)
        {
            using (MvcCRUDEntities _MvcCRUD = new MvcCRUDEntities())
            {
                return View(_MvcCRUD.Customer.Where(x => x.Id == id).FirstOrDefault());
            }
        }

        // POST: Customer/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, Customer customer)
        {
            try
            {
                using (MvcCRUDEntities _MvcCRUD = new MvcCRUDEntities())
                {
                    _MvcCRUD.Entry(customer).State = EntityState.Modified;
                    _MvcCRUD.SaveChanges();
                }
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Customer/Delete/5
        public ActionResult Delete(int id)
        {
            using (MvcCRUDEntities _MvcCRUD = new MvcCRUDEntities())
            {
                return View(_MvcCRUD.Customer.Where(x => x.Id == id).FirstOrDefault());
            }
        }

        // POST: Customer/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                using (MvcCRUDEntities _MvcCRUD = new MvcCRUDEntities())
                {
                    Customer customer = _MvcCRUD.Customer.Where(x => x.Id == id).FirstOrDefault();
                    _MvcCRUD.Customer.Remove(customer);
                    _MvcCRUD.SaveChanges();
                }
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
