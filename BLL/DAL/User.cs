using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.DAL
{
    public class User
    {
        public int Id { get; set; }
        [Required(ErrorMessage ="User Name is required!")]
        [StringLength(50,ErrorMessage ="User name must be maximum {1} characters! ")]
        public string UserName { get; set; }
        [Required(ErrorMessage = "{0} is required!")]
        [StringLength(10,ErrorMessage ="{0} must be maximum {1} characters!")]
        public string Password { get; set; }
        public bool IsActive { get; set;}
        public int RoleId { get; set; }

        // Navigation Property
        public Role Role { get; set; } // Her kullanıcı bir role sahiptir
    }
}
