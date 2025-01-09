using BLL.DAL;

namespace BLL.Models
{
    public class DirectorModel
    {
        public Director Record { get; set; }

        public int Id => Record.Id;

        public string Name => Record.Name;

        public string Surname => Record.Surname;

        public string isRetired => Record.isRetired ? "Retired" : "Not Retired";
         
        public List<Movies> Movies => Record.Movies;
    }
}
