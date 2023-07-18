using Server.Data;
using Server.Models;

namespace Server.Services
{
    public class TaskItemService
    {
        private readonly ApplicationDbContext _context;
        private readonly TaskItemRepository _repository;

        public TaskItemService(ApplicationDbContext context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _repository = new TaskItemRepository(context, httpContextAccessor);
        }

        public async Task<bool> Delete(TaskItem entity)
        {
            await _repository.Delete(entity);
            return true;
        }

        public async Task<List<TaskItemDto>> GetAll()
        { 
            return await _repository.GetAll();
        }

        public async Task<TaskItem> GetById(int id)
        {
            return await _repository.GetById(id);
        }

        public async Task<bool> Save(TaskItem entity)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                var isTaskTimeConflict = await _repository.IsTaskTimeConflict(entity);
                if (isTaskTimeConflict)
                    throw new Exception("La tarea no puede ser creada debido a un conflicto de horario.");

                if (entity.Id == 0)
                    await _repository.Create(entity);
                else
                    await _repository.Update(entity);

                await transaction.CommitAsync();
                return true;
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                throw new Exception("", ex);
            }
        }
    }
}
