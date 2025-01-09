using BLL.DAL;
using System.ComponentModel;

namespace BLL.Models
{
    public class MoviesModel
    {
        public Movies Record { get; set; }

        public int Id => Record.Id;

        public string Name => Record.Name;
        public string ReleaseDate => !Record.ReleaseDate.HasValue ? string.Empty : Record.ReleaseDate.Value.ToString("MM/dd/yyyy");
        //public DateTime? ReleaseDate => Record.ReleaseDate;

        public decimal TotalRevenue => Record.TotalRevenue;
        // DirectorId'yi direkt alabiliriz
        public int DirectorId => Record.DirectorId;
        public Director Directors { get; set; }
        public List<Director> DirectorList { get; set; } = new List<Director>();

        public string Director => Record.Directors?.Name;
       

        public string Genres => string.Join("-", Record.MovieGenres?.Select(po => po.Genre?.Name));

        [DisplayName("Genres")]
        public List<int> GenresIds
        {
            get => Record.MovieGenres?.Select(po => po.GenreId).ToList();
            set => Record.MovieGenres = value.Select(v => new MovieGenre() { GenreId = v }).ToList();
        }
    }



}

