using Microsoft.AspNetCore.Mvc;
using StudentApp.Models;
using StudentApp.Repository.Interface;

namespace StudentApp_DbConnection.Controllers
{
    public class StudentController : Controller
    {
        private readonly IStudentRepo _studentRepo;
        public StudentController(IStudentRepo studentRepo)
        {
            _studentRepo = studentRepo;
        }

       
        public IActionResult GetAllStudents()
        {
            var result = _studentRepo.GetAllStudents();
            if(result.Any())
            {
                return View(result);
            }
            else
            {
                return BadRequest();
            }
        }

        public IActionResult DisplayForm()
        {
            return View();
        }

        [HttpPost]
        public IActionResult DisplayForm(StudentModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            else
            {
                var result = _studentRepo.AddStudent(model);
                if (result)
                {
                    return RedirectToAction("GetAllStudents");
                }
                else
                {
                    return BadRequest();
                }
            }
        }

        public IActionResult EditStudent(int id)
        {
            var data = _studentRepo.GetStudent(id);
            return View(data);
        }

        [HttpPost]
        public IActionResult EditStudent(StudentModel model)
        {
            var result = _studentRepo.UpdateStudentRecord(model);
            if(result == 1)
            {
                return RedirectToAction("GetAllStudents");
            }
            else
            {
                return NotFound();
            }
        }

        public IActionResult RemoveStudent(int id)
        {
            var result = _studentRepo.RemoveStudenet(id);
            if (result)
            {
                return RedirectToAction("GetAllStudents");
            }
            else
            {
                return NotFound();
            }
        }
    }
}
