using StudentApp.Core.DataModel;
using StudentApp.Core.DB_Context;
using StudentApp.Models;
using StudentApp.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentApp.Repository.Implementation
{
    public class StudentRepo : IStudentRepo
    {
        private readonly StudentAppContext _context;
        public StudentRepo(StudentAppContext context)
        {
            _context = context;
        }

        public bool AddStudent(StudentModel student)
        {
            bool flag = false;
            try
            {
                var addStudent = new StudentRecord
                {
                    Name = student.Name,
                    RollNo = student.RollNo,
                    Class = student.Class,
                    Email = student.Email,
                    Address = student.Address,
                    Deleted = false,
                };
                _context.StudentRecords.Add(addStudent);
                _context.SaveChanges();
                flag = true;
                return flag;
            }
            catch(Exception ex)
            {
                throw;
            }
        }

        public IEnumerable<StudentModel> GetAllStudents()
        {
            try
            {
                var students = _context.StudentRecords.Where(x => x.Deleted == false).Select(st => new StudentModel
                {
                    Id = st.Id,
                    Name = st.Name,
                    Class = st.Class,
                    RollNo = st.RollNo,
                    Email = st.Email,
                    Address = st.Address,
                }).ToList();
                return students;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
