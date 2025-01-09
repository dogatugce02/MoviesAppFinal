using BLL.DAL;
using BLL.Models;
using BLL.Services.Bases;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace BLL.Services
{
    public interface IMoviesService
    {
        public IQueryable<MoviesModel> Query();
        public ServiceBase Create(Movies record);
        public ServiceBase Update(Movies record);
        public ServiceBase Delete(int id);
    }

    public class Movieservice : ServiceBase, IMoviesService
    {
        public Movieservice(Db db) : base(db)
        {
        }

        public IQueryable<MoviesModel> Query()
        {
            //return _db.Movies.OrderBy(s => s.Name).Select(s => new MoviesModel() { Record = s });
            try
            {
                return _db.Movies
                    .Include(p => p.Directors)
                    .Include(p => p.MovieGenres)
                    .ThenInclude(po => po.Genre)
                   .OrderByDescending(p => p.ReleaseDate)
                   .ThenByDescending(p => p.TotalRevenue)
                   .ThenBy(p => p.Name)
                .Select(p => new MoviesModel() { Record = p });

                //List<MoviesModel> moviesModel = new List<MoviesModel>();
                //moviesModel = result;
                //return (IQueryable<MoviesModel>)moviesModel;
                //return (IQueryable<MoviesModel>)result;

            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public ServiceBase Create(Movies record)
        {
            if (_db.Movies.Any(s => s.Name.ToUpper() == record.Name.ToUpper().Trim()))
                return Error("Movies with the same name exists!");
            record.Name = record.Name?.Trim();
            _db.Movies.Add(record);
            _db.SaveChanges(); // commit to the database

            //moviegenreye de kayıt yazılmalı
            //MovieGenre _movieGenre = new MovieGenre();
            //_movieGenre.MovieId = record.MovieGenres.
            //_db.MovieGenres.Add()

            return Success("Movies created successfully.");
        }

        public ServiceBase Update(Movies record)
        {
            if (_db.Movies.Any(s => s.Id != record.Id && s.Name.ToUpper() == record.Name.ToUpper().Trim()))
                return Error("Movies with the same name exists!");
            // Way 1:
            //var entity = _db.Movies.Find(record.Id);
            // Way 2:
            var entity = _db.Movies.SingleOrDefault(s => s.Id == record.Id);
            if (entity is null)
                return Error("Movies can't be found!");
            entity.Name = record.Name?.Trim();
            _db.Movies.Update(entity);
            _db.SaveChanges(); // commit to the database
            return Success("Movies updated successfully.");
        }

        public ServiceBase Delete(int id)
        {
            var entity = _db.Movies.Include(s => s.Directors).SingleOrDefault(s => s.Id == id);
            if (entity is null)
                return Error("Movies can't be found!");
            //if (entity.DirectorId > 0) // Count > 0
            //    return Error("Movies has relational Director!");
            _db.Movies.Remove(entity);
            _db.SaveChanges(); // commit to the database
            return Success("Movies deleted successfully.");
        }
    }
}
