using Server.Data;
using Server.Models;

namespace Server.Services
{
    public class UserService
    {
        private readonly UserRepository _repository;
        private readonly ApplicationDbContext _context;

        public UserService(ApplicationDbContext context)
        {
            _context = context;
            _repository = new UserRepository(context);
        }

        public async Task<List<UserDto>> GetAll()
        {
            try
            {
                return await _repository.GetAll();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<UserDto> GetById(int id)
        {
            try
            {
                return await _repository.GetById(id);
            }
            catch(Exception)
            {
                throw;
            }
        }

        public async Task<bool> Save(User entity)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
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
                throw new Exception("Error al guardar usuario.", ex);
            }
        }
    }
}
