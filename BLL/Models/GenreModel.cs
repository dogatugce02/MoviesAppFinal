using BLL.DAL;

namespace BLL.Models
{
    public class GenreModel
    {
        public Genre Record { get; set; }

        public int Id => Record.Id;

        public string Name => Record.Name;

    }
}
