using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodApp.Models
{
    public class LoginFormModel
    {
        [Required(ErrorMessage ="Username is required")]
        public string? Username { get; set; }

        [Required(ErrorMessage = "password is required")]
        public string? Password { get; set; }
    }
}
