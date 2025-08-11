using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodApp.Core.Entities
{
    public class User
    {
        public int Id { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Email { get; set; }
        public bool? IsDeleted { get; set; }
        public string? Address { get; set; }
        public int? PhoneNumber { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? PasswordExpiryDate { get; set; }
        public string? Password { get; set; }


    }
}
