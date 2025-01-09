using BLL.DAL;

namespace BLL.Models
{
    public class RoleModel
    {
        public Role Record { get; set; }

        public int Id => Record.Id;

        public string Name => Record.Name;

        public List<User> Users => Record.Users; 
    }
}
