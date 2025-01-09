using BLL.DAL;
using BLL.Models;
using BLL.Services.Bases;
using Microsoft.EntityFrameworkCore;

namespace BLL.Services
{
    public interface IDirectorService
    {
        public IQueryable<DirectorModel> Query();
        public ServiceBase Create(Director record);
        public ServiceBase Update(Director record);
        public ServiceBase Delete(int id);
    }

    public class DirectorService : ServiceBase, IDirectorService
    {
        public DirectorService(Db db) : base(db)
        {
        }

        public IQueryable<DirectorModel> Query()
        {
            return _db.Directors.OrderBy(s => s.Name).Select(s => new DirectorModel() { Record = s });
        }

        public ServiceBase Create(Director record)
        {
            if (_db.Directors.Any(s => s.Name.ToUpper() == record.Name.ToUpper().Trim()))
                return Error("Director with the same name exists!");
            record.Name = record.Name?.Trim();
            _db.Directors.Add(record);
            _db.SaveChanges(); // commit to the database
            return Success("Director created successfully.");
        }

        public ServiceBase Update(Director record)
        {
            if (_db.Directors.Any(s => s.Id != record.Id && s.Name.ToUpper() == record.Name.ToUpper().Trim()))
                return Error("Director with the same name exists!");
            // Way 1:
            //var entity = _db.Director.Find(record.Id);
            // Way 2:
            var entity = _db.Directors.SingleOrDefault(s => s.Id == record.Id);
            if (entity is null)
                return Error("Director can't be found!");
            entity.Name = record.Name?.Trim();
            _db.Directors.Update(entity);
            _db.SaveChanges(); // commit to the database
            return Success("Director updated successfully.");
        }

        public ServiceBase Delete(int id)
        {
            var entity = _db.Directors.Include(s => s.Movies).SingleOrDefault(s => s.Id == id);
            if (entity is null)
                return Error("Director can't be found!");
            if (entity.Movies.Any()) // Count > 0
                return Error("Director has relational users!");
            _db.Directors.Remove(entity);
            _db.SaveChanges(); // commit to the database
            return Success("Director deleted successfully.");
        }
    }
}
