using Server.Data;
using Server.Interfaces;
using Server.Models.Enums;
using Microsoft.EntityFrameworkCore;

namespace Server.Models
{
    public abstract class BaseRepository : IRepository<BaseEntity>
    {
        private readonly ApplicationDbContext _context;
        
        /// <summary>
        /// Id of the currently logged-in user.
        /// </summary>
        private int _currentId = 1;

        public BaseRepository(ApplicationDbContext context)
        {
            _context = context;
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
