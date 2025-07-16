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

        public StudentModel GetStudent(int id)
        {
            var model = new StudentModel();
            try
            {
                var studentExist = _context.StudentRecords.FirstOrDefault(x => x.Id == id);
                if(studentExist != null)
                {
                    model.Id = studentExist.Id;
                    model.Name = studentExist.Name;
                    model.Class = studentExist.Class;
                    model.RollNo = studentExist.RollNo;
                    model.Email = studentExist.Email;
                    model.Address = studentExist.Address;
                }
                else
                {
                    return new StudentModel();
                }
                return model;
            }
            catch(Exception ex)
            {
                throw;
            }
        }

        public int UpdateStudentRecord(StudentModel student)
        {
            int result = 0;
            try
            {
                var exist = _context.StudentRecords.FirstOrDefault(y => y.Id == student.Id);
                if(exist != null)
                {
                    exist.Name = student.Name;
                    exist.Class = student.Class;
                    exist.RollNo = student.RollNo;
                    exist.Email = student.Email;
                    exist.Address = student.Address;

                    _context.StudentRecords.Update(exist);
                    _context.SaveChanges();
                    result = 1;
                }
                return result;
            }
            catch(Exception ex)
            {
                throw;
            }
        }
    }
}
