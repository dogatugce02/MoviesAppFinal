using BLL.DAL;
using BLL.Models;
using BLL.Services.Bases;
using Microsoft.EntityFrameworkCore;

namespace BLL.Services
{
    public interface IRoleService
    {
        public IQueryable<RoleModel> Query();
        public ServiceBase Create(Role record);
        public ServiceBase Update(Role record);
        public ServiceBase Delete(int id);
    }

    public class RoleService : ServiceBase, IRoleService
    {
        public RoleService(Db db) : base(db)
        {
        }

        public IQueryable<RoleModel> Query()
        {
            return _db.Roles.OrderBy(s => s.Name).Select(s => new RoleModel() { Record = s });
        }

        public ServiceBase Create(Role record)
        {
            if (_db.Roles.Any(s => s.Name.ToUpper() == record.Name.ToUpper().Trim()))
                return Error("Role with the same name exists!");
            record.Name = record.Name?.Trim();
            _db.Roles.Add(record);
            _db.SaveChanges(); // commit to the database
            return Success("Role created successfully.");
        }

        public ServiceBase Update(Role record)
        {
            if (_db.Roles.Any(s => s.Id != record.Id && s.Name.ToUpper() == record.Name.ToUpper().Trim()))
                return Error("Role with the same name exists!");
            // Way 1:
            //var entity = _db.Role.Find(record.Id);
            // Way 2:
            var entity = _db.Roles.SingleOrDefault(s => s.Id == record.Id);
            if (entity is null)
                return Error("Role can't be found!");
            entity.Name = record.Name?.Trim();
            _db.Roles.Update(entity);
            _db.SaveChanges(); // commit to the database
            return Success("Role updated successfully.");
        }

        public ServiceBase Delete(int id)
        {
            var entity = _db.Roles.Include(s => s.Users).SingleOrDefault(s => s.Id == id);
            if (entity is null)
                return Error("Role can't be found!");
            if (entity.Users.Any()) // Count > 0
                return Error("Role has relational users!");
            _db.Roles.Remove(entity);
            _db.SaveChanges(); // commit to the database
            return Success("Role deleted successfully.");
        }
    }
}
