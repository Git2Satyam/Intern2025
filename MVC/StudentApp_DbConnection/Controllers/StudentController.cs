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
    }
}
