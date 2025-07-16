using StudentApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentApp.Repository.Interface
{
    public interface IStudentRepo
    {
        IEnumerable<StudentModel> GetAllStudents();
        bool AddStudent(StudentModel student);
        StudentModel GetStudent(int id);
        int UpdateStudentRecord(StudentModel student);
    }
}
