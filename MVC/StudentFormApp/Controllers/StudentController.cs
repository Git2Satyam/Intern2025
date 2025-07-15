using Microsoft.AspNetCore.Mvc;
using StudentFormApp.Models;

namespace StudentFormApp.Controllers
{
    public class StudentController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly static List<StudentML> student = new List<StudentML>();

        public StudentController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }
        public IActionResult Index()
        {
            return View(student);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(StudentML model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                student.Add(model);
                return RedirectToAction("Index");
                //return View(student);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return BadRequest();
            }
        }

        public IActionResult EditStudent(int rollNo)
        {
            var studentExist = student.FirstOrDefault(x => x.RollNo == rollNo);
            if (studentExist == null) return NotFound();
                     
            return View(studentExist);
        }

        [HttpPost]
        public IActionResult EditStudent(StudentML model)
        {
            var exist = student.FirstOrDefault(st => st.RollNo == model.RollNo);
            if (exist == null) return NotFound();
            exist.Name = model.Name;
            exist.Address = model.Address;
            exist.Email = model.Email;
            return RedirectToAction("Index");
        }

        public IActionResult DeleteStudent(int rollNo)
        {
            var studentExist = student.FirstOrDefault(x => x.RollNo == rollNo);
            if(studentExist == null) return NotFound();
            student.Remove(studentExist);
            return RedirectToAction("Index");
        }
    }
}
