using Server.Data;
using Server.Models;
using System.Text.RegularExpressions;

namespace Server.Services
{
	public class UserService
	{
		private readonly UserRepository _repository;
		private readonly ApplicationDbContext _context;
		private readonly PasswordHasher _passwordHasher;

		public UserService(ApplicationDbContext context)
		{
			_context = context;
			_repository = new UserRepository(context);
			_passwordHasher = new PasswordHasher();
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
				{
					var salt = _passwordHasher.GenerateSalt();
					var hashedPassword = _passwordHasher.HashPassword(entity.Password!, salt);
					
					user.Password = hashedPassword;
					user.Salt = Convert.ToBase64String(salt);

					await _repository.Create(user);
				}
				else
				{
					if(!String.IsNullOrWhiteSpace(entity.Password))
					{
						user = await _repository.GetById(entity.Id);
						var verifiedPassword = _passwordHasher.VerifyPassword(entity.Password, user.Password!, Convert.FromBase64String(user.Salt));
						var passwordsMatch = entity.NewPassword == entity.RepeatPassword;

						if (verifiedPassword && passwordsMatch)
						{
							var salt = _passwordHasher.GenerateSalt();
							var hashedPassword = _passwordHasher.HashPassword(entity.NewPassword!, salt);

							user.Password = hashedPassword;
							user.Salt = Convert.ToBase64String(salt);
						}
						else
							throw new Exception("Error, las contraseñas no coinciden.");
					}
					await _repository.Update(user);
				}
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
