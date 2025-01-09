using BLL.DAL;
using BLL.Models;
using BLL.Services.Bases;
using Microsoft.EntityFrameworkCore;

namespace BLL.Services
{
    public interface IGenreService
    {
        public IQueryable<GenreModel> Query();
        public ServiceBase Create(Genre record);
        public ServiceBase Update(Genre record);
        public ServiceBase Delete(int id);
    }

    public class GenreService : ServiceBase, IGenreService
    {
        public GenreService(Db db) : base(db)
        {
        }

        public IQueryable<GenreModel> Query()
        {
            return _db.Genres.OrderBy(s => s.Name).Select(s => new GenreModel() { Record = s });
        }

        public ServiceBase Create(Genre record)
        {
            if (_db.Genres.Any(s => s.Name.ToUpper() == record.Name.ToUpper().Trim()))
                return Error("Genre with the same name exists!");
            record.Name = record.Name?.Trim();
            _db.Genres.Add(record);
            _db.SaveChanges(); // commit to the database
            return Success("Genre created successfully.");
        }

        public ServiceBase Update(Genre record)
        {
            if (_db.Genres.Any(s => s.Id != record.Id && s.Name.ToUpper() == record.Name.ToUpper().Trim()))
                return Error("Genre with the same name exists!");
            // Way 1:
            //var entity = _db.Genre.Find(record.Id);
            // Way 2:
            var entity = _db.Genres.SingleOrDefault(s => s.Id == record.Id);
            if (entity is null)
                return Error("Genre can't be found!");
            entity.Name = record.Name?.Trim();
            _db.Genres.Update(entity);
            _db.SaveChanges(); // commit to the database
            return Success("Genre updated successfully.");
        }

        public ServiceBase Delete(int id)
        {
            var entity = _db.Genres.Include(s => s.MovieGenres).SingleOrDefault(s => s.Id == id);
            if (entity is null)
                return Error("Genre can't be found!");
            if (entity.MovieGenres.Any() )// Count > 0
                return Error("Genre has relational Genre!");
            _db.Genres.Remove(entity);
            _db.SaveChanges(); // commit to the database
            return Success("Genre deleted successfully.");
        }
    }
}
