using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Trupti_Gandhi_WebMobi.Models;
using PagedList.Mvc;
using PagedList;

namespace Trupti_Gandhi_WebMobi.Controllers
{
    public class EmployeesController : Controller
    {
        private EmployeeContext db = new EmployeeContext();

        // GET: Employees
        public ActionResult Index(string Sorting_Order, string Search_Data, string Filter_Value, int? Page_No)
        {
            ViewBag.CurrentSortOrder = Sorting_Order;
            ViewBag.SortingName = String.IsNullOrEmpty(Sorting_Order) ? "Name_Description" : "";
            ViewBag.SortingDate = Sorting_Order == "Date_of_Birth" ? "Date_Description" : "Date";

            if (Search_Data != null)
            {
                Page_No = 1;
            }
            else
            {
                Search_Data = Filter_Value;
            }

            ViewBag.FilterValue = Search_Data;
            var emps = from emp in db.Employees select emp;
            if (!String.IsNullOrEmpty(Search_Data))
            {
                emps = emps.Where(emp => emp.FirstName.ToUpper().Contains(Search_Data.ToUpper())
            || emp.LastName.ToUpper().Contains(Search_Data.ToUpper()));
            }
            switch (Sorting_Order)
            {
                case "Name_Description":
                    emps = emps.OrderByDescending(emp => emp.FirstName);
                    break;
                case "Date_of_Birth":
                    emps = emps.OrderBy(emp => emp.DateOfBirth);
                    break;
                case "Date_Description":
                    emps = emps.OrderByDescending(emp => emp.DateOfBirth);
                    break;
                default:
                    emps = emps.OrderBy(emp => emp.FirstName);
                    break;
            }
            int Size_Of_Page = 4;
            int No_Of_Page = (Page_No ?? 1);
            return View(emps.ToPagedList(No_Of_Page, Size_Of_Page));
        }

        // GET: Employees/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Employee employee = db.Employees.Find(id);
            if (employee == null)
            {
                return HttpNotFound();
            }
            return View(employee);
        }

        // GET: Employees/Create
        public ActionResult Create()
        {
            var res = new SelectList(db.Departments, "DeptId", "DepartmentName").ToList();
            ViewBag.Dept = res;
            return View();
        }

        // POST: Employees/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Employee employee)
        {
            ModelState["Departments.DepartmentName"].Errors.Clear();
            if (ModelState.IsValid)
            {
                Department dept = db.Departments.Find(employee.Departments.DeptId);
                employee.Departments = dept;
                db.Employees.Add(employee);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(employee);
        }

        // GET: Employees/Edit/5
        public ActionResult Edit(int? id)
        {
            var res = new SelectList(db.Departments, "DeptId", "DepartmentName").ToList();
            ViewBag.Dept = res;
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Employee employee = db.Employees.Find(id);
            if (employee == null)
            {
                return HttpNotFound();
            }
            return View(employee);
        }

        // POST: Employees/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Employee employee)
        {
            ModelState["Departments.DepartmentName"].Errors.Clear();
            if (ModelState.IsValid)
            {
                Department dept = db.Departments.Find(employee.Departments.DeptId);
                employee.Departments = dept;
                db.Employees.Add(employee);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(employee);
            /*
            [Bind(Include = "EmployeeID,FirstName,LastName,Gender,DateOfBirth")] 
            if (ModelState.IsValid)
            {
                db.Entry(employee).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }*/
        }

        // GET: Employees/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Employee employee = db.Employees.Find(id);
            if (employee == null)
            {
                return HttpNotFound();
            }
            return View(employee);
        }

        // POST: Employees/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Employee employee = db.Employees.Find(id);
            db.Employees.Remove(employee);
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
