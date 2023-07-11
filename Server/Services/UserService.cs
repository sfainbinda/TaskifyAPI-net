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

		public async Task<bool> Delete(User entity)
		{
			await _repository.Delete(entity);
			return true;
		}

		public async Task<List<UserDto>> GetAll()
		{
			return await _repository.GetAll();
		}

		public async Task<User> GetByEmail(string email)
		{
			return await _repository.GetByEmail(email);
		}

		public async Task<User> GetById(int id)
		{
			return await _repository.GetById(id);
		}

		public async Task<bool> Save(UserDto entity)
		{
			using var transaction = await _context.Database.BeginTransactionAsync();
			var user = new User(entity);
			try
			{
				if (entity.Id == 0)
					await _repository.Create(user);
				else
					await _repository.Update(user);

				await transaction.CommitAsync();
				entity.Id = user.Id;
				return true;
			}
			catch (Exception ex)
			{
				await transaction.RollbackAsync();
				throw new Exception("Error al guardar/actualizar usuario.", ex);
			}
		}
	}
}
