using BLL.DAL;

namespace BLL.Models
{
    public class MovieGenreModel
    {
        public MovieGenre Record { get; set; }

        public int Id => Record.Id;

        public int MovieId => Record.MovieId;

        public int GenreId => Record.GenreId;

    }
}
