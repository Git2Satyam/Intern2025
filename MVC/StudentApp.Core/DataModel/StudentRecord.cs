using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentApp.Core.DataModel
{
    public class StudentRecord
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public int? RollNo { get; set; }
        public string? Class { get; set; }
        public string? Email { get; set; }
        public string? Address { get; set; }
        public Boolean? Deleted { get; set; }
    }
}
