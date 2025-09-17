using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AllureStore.Models
{
    public class AdminRoleModel
    {
        //public int Id { get; set; }
        public string? RoleName { get; set; }
        public bool? View { get; set; }
        public bool? Edit { get; set; }
        
    }
}
