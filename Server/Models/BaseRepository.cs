using Server.Data;
using Server.Interfaces;
using Server.Models.Enums;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace Server.Models
{
    public abstract class BaseRepository : IRepository<BaseEntity>
    {
        private readonly ApplicationDbContext _context;
        private readonly IHttpContextAccessor _contextAccessor;
        
        /// <summary>
        /// Id of the currently logged-in user.
        /// </summary>
        protected int _currentId;

        public int CurrentId => _currentId;

        protected BaseRepository(ApplicationDbContext context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _contextAccessor = httpContextAccessor;

            int id = 0;
            if (httpContextAccessor.HttpContext!.User.HasClaim(x => x.Type == "Id")
                && int.TryParse(httpContextAccessor.HttpContext.User.Claims.First(x => x.Type == "Id").Value, out id))
            {
                _currentId = id;
            }
            
        }

        public async Task<bool> Create(BaseEntity entity)
        {
            entity.State = EnState.Active;
            entity.Created = DateTime.Now;
            entity.CreatedUserId = _currentId;
            
            return true;
        }

        public async Task<bool> Delete(BaseEntity entity)
        {
            var attachEntity = _context.Attach(entity);

            attachEntity.State = EntityState.Modified;
            attachEntity.Property(x => x.Created).IsModified = false;
            attachEntity.Property(x => x.CreatedUserId).IsModified = false;
            attachEntity.Property(x => x.Updated).IsModified = false;
            attachEntity.Property(x => x.UpdatedUserId).IsModified = false;

            entity.State = EnState.Deleted;
            entity.Deleted = DateTime.Now;
            entity.DeletedUserId = _currentId;

            return true;
        }

        public async Task<bool> Update(BaseEntity entity)
        {
            var attachEntity = _context.Attach(entity);

            attachEntity.State = EntityState.Modified;
            attachEntity.Property(x => x.State).IsModified = false;
            attachEntity.Property(x => x.Created).IsModified = false;
            attachEntity.Property(x => x.CreatedUserId).IsModified = false;
            attachEntity.Property(x => x.Deleted).IsModified = false;
            attachEntity.Property(x => x.DeletedUserId).IsModified = false;

            entity.Updated = DateTime.Now;
            entity.UpdatedUserId = _currentId;

            return true;
        }
    }
}
