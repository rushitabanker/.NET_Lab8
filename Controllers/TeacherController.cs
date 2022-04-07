using Ass8;
using Ass8.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Demo.Controllers
{
    public class TeacherController : Controller
    {
        private readonly ApplicationDBContext _db;

        public TeacherController(ApplicationDBContext db)
        {
            _db = db;
        }
        // public IActionResult Index()
        // {
        //     IEnumerable<Teacher> listofTeacher = _db.Teacher;
        //     return View(listofTeacher);
        // }

        [HttpGet]
        public IActionResult Edit(int TeacherId)
        {
            var teacherObj = _db.Teacher.Find(TeacherId);
            return View(teacherObj);            
        }
        [HttpPost]
        public IActionResult Edit(Teacher newTeacher)
        {
            _db.Teacher.Update(newTeacher);
            _db.SaveChanges();
            return RedirectToAction("Index");

        }

        public IActionResult Delete(int TeacherId)
        {
            var teacherObj = _db.Teacher.Find(TeacherId);
            _db.Teacher.Remove(teacherObj);
            _db.SaveChanges();

            IEnumerable<Teacher> newListOfTeacher = _db.Teacher;
            return RedirectToAction("Index", newListOfTeacher);

        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create([Bind("Name, Class")] Teacher teacherObj)
        {
            if(ModelState.IsValid)
            {
                _db.Teacher.Add(teacherObj);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(teacherObj);
        }

        public async Task<IActionResult> Index(string searchString)
        {
            var teachers = from t in _db.Teacher
                           select t;
            if(!String.IsNullOrEmpty(searchString))
            {
                teachers = teachers.Where(s => s.Name!.Contains(searchString));
            }

            return View(await teachers.ToListAsync());

        }

    }
}