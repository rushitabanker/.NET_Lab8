using Ass8;
using Ass8.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Demo.Controllers
{
    public class StudentController : Controller
    {
        private readonly ApplicationDBContext _db;

        public StudentController(ApplicationDBContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            IEnumerable<Student> listofStudent = _db.Student;
            return View(listofStudent);
        }

        [HttpGet]
        public IActionResult Edit(int StudentId)
        {
            var studentObj = _db.Student.Find(StudentId);
            return View(studentObj);            
        }
        [HttpPost]
        public IActionResult Edit(Student newStudent)
        {
            _db.Student.Update(newStudent);
            _db.SaveChanges();
            return RedirectToAction("Index");

        }

        public IActionResult Delete(int StudentId)
        {
            var studentObj = _db.Student.Find(StudentId);
            _db.Student.Remove(studentObj);
            _db.SaveChanges();

            IEnumerable<Student> newListOfStudent = _db.Student;
            return RedirectToAction("Index", newListOfStudent);

        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create([Bind("Name, Class, SubjectId")] Student studentObj)
        {
            if(ModelState.IsValid)
            {
                _db.Student.Add(studentObj);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(studentObj);
        }

    }
}