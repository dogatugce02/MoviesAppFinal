using BLL.DAL;
using BLL.Models;
using BLL.Services.Bases;
using Microsoft.EntityFrameworkCore;

namespace BLL.Services
{
    public interface IMovieGenreService
    {
        public IQueryable<MovieGenreModel> Query();
        public ServiceBase Create(MovieGenre record);
        public ServiceBase Update(MovieGenre record);
        public ServiceBase Delete(int id);
    }

    public class MovieGenreService : ServiceBase, IMovieGenreService
    {
        public MovieGenreService(Db db) : base(db)
        {
        }

        public IQueryable<MovieGenreModel> Query()
        {
            return _db.MovieGenres.OrderBy(s => s.Genre.Name).Select(s => new MovieGenreModel() { Record = s });
        }

        public ServiceBase Create(MovieGenre record)
        {
            if (record != null && record.Genre != null && !string.IsNullOrEmpty(record.Genre.Name)
                && _db.MovieGenres.Any(s => s.Genre.Name.ToUpper() == record.Genre.Name.ToUpper().Trim())
                ) {
                
                return Error("MovieGenre with the same name exists!");
            }
            if (record.Genre != null)
                record.Genre.Name = record.Genre.Name?.Trim();
            _db.MovieGenres.Add(record);
            _db.SaveChanges(); // commit to the database
            return Success("MovieGenre created successfully.");
        }

        public ServiceBase Update(MovieGenre record)
        {
            if (_db.MovieGenres.Any(s => s.Id != record.Id && s.Genre.Name.ToUpper() == record.Genre.Name.ToUpper().Trim()))
                return Error("MovieGenre with the same name exists!");
            // Way 1:
            //var entity = _db.MovieGenre.Find(record.Id);
            // Way 2:
            var entity = _db.MovieGenres.SingleOrDefault(s => s.Id == record.Id);
            if (entity is null)
                return Error("MovieGenre can't be found!");
            entity.Genre.Name = record.Genre.Name?.Trim();
            _db.MovieGenres.Update(entity);
            _db.SaveChanges(); // commit to the database
            return Success("MovieGenre updated successfully.");
        }

        public ServiceBase Delete(int id)
        {
            var entity = _db.MovieGenres.Include(s => s.Movie).SingleOrDefault(s => s.Id == id);
            if (entity is null)
                return Error("MovieGenre can't be found!");
            if (entity.Movie.Id > 0) // Count > 0
                return Error("MovieGenre has relational MovieGenre!");
            _db.MovieGenres.Remove(entity);
            _db.SaveChanges(); // commit to the database
            return Success("MovieGenre deleted successfully.");
        }
    }
}
