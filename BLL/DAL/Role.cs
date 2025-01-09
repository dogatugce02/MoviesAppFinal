using System.ComponentModel.DataAnnotations;

namespace BLL.DAL
{
    public class Role
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        // Navigation Property
        public List<User> Users { get; set; } = new List<User>();// Bir rol birden fazla kullanıcıya atanabilir
    }
}