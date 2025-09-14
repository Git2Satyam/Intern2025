using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.DB_Context;
using WebApplication1.Entities;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly StudentContext _context;
        public StudentController(StudentContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult GetAllStudents()
        {
            try
            {
                var students = _context.StudentTable.Select(x => new StudentModel
                {
                    Id = x.Id,  
                    Name = x.Name,
                    Email = x.Email,
                    Address = x.Address,
                    RollNumber = x.RollNumber,
                });
                return Ok(students);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public IActionResult AddOrUpdateStudent(StudentModel model)
        {
            try
            {
                if(!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var studentExist = _context.StudentTable.FirstOrDefault(x => x.Id == model.Id);
                if(studentExist != null)
                {
                    studentExist.Name = model.Name;
                    studentExist.Email = model.Email;
                    studentExist.Address = model.Address;
                }
                else
                {
                    var student = new Student
                    {
                        Name = model.Name,
                        RollNumber = model.RollNumber,
                        Email = model.Email,
                        Address = model.Address,
                        isDeleted = false
                    };
                    _context.StudentTable.Add(student);
                }
                _context.SaveChanges();

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete]
        public IActionResult DeleteStudent(int id)
        {
            try
            {
                var student = _context.StudentTable.FirstOrDefault( x => x.Id == id);
                if(student != null)
                {
                    _context.StudentTable.Remove(student);
                    _context.SaveChanges();
                }
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
