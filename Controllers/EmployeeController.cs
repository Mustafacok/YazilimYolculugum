using MvcCRUD.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcCRUD.Controllers
{
    public class EmployeeController : Controller
    {
        // GET: Employee
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult ViewAll()
        {
            return View(GetAllEmployee());
        }

        IEnumerable<Employee> GetAllEmployee()
        {
            using (MvcCRUDEntities _MvcCRUD = new MvcCRUDEntities())
            {
                return _MvcCRUD.Employee.ToList<Employee>();
            }
        }

        public ActionResult AddOrEdit(int id=0)
        {
            Employee emp = new Employee();
            if (id != 0)
            {
                using (MvcCRUDEntities _MvcCRUD = new MvcCRUDEntities())
                {
                    emp = _MvcCRUD.Employee.Where(x => x.EmployeeID == id).FirstOrDefault<Employee>();
                }
            }
            return View(emp);
        }

        [HttpPost]
        public ActionResult AddOrEdit(Employee emp)
        {
            try
            {
                if (emp.ImageUpload != null)
                {
                    string fileName = Path.GetFileNameWithoutExtension(emp.ImageUpload.FileName);
                    string extension = Path.GetExtension(emp.ImageUpload.FileName);
                    fileName = fileName + DateTime.Now.ToString("yymmssfff") + extension;
                    emp.ImagePath = "~/AppFiles/Images/" + fileName;
                    emp.ImageUpload.SaveAs(Path.Combine(Server.MapPath("~/AppFiles/Images/"), fileName));
                }
                using (MvcCRUDEntities _MvcCRUD = new MvcCRUDEntities())
                {
                    if (emp.EmployeeID ==0)
                    {
                        _MvcCRUD.Employee.Add(emp);
                        _MvcCRUD.SaveChanges();
                    }
                    else
                    {
                        _MvcCRUD.Entry(emp).State = EntityState.Modified;
                        _MvcCRUD.SaveChanges();
                    }
                    
                }
                return Json(new { success = true, html = GlobalClass.RenderRazorViewToString(this, "ViewAll", GetAllEmployee()), message = "Submitted Succesfully" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult Delete(int id)
        {
            try
            {
                using (MvcCRUDEntities _MvcCRUD = new MvcCRUDEntities())
                {
                    Employee emp = _MvcCRUD.Employee.Where(x => x.EmployeeID == id).FirstOrDefault<Employee>();
                    _MvcCRUD.Employee.Remove(emp);
                    _MvcCRUD.SaveChanges();
                }
                return Json(new { success = true, html = GlobalClass.RenderRazorViewToString(this, "ViewAll", GetAllEmployee()), message = "Deleted Succesfully" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
    }
}