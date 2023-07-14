using Microsoft.EntityFrameworkCore;
using Server.Data;
using Server.Interfaces;
using Server.Models.Enums;

namespace Server.Models
{
	public class UserRepository : BaseRepository, IRepository<User>
	{
		private readonly ApplicationDbContext _context;

		public UserRepository(ApplicationDbContext context, IHttpContextAccessor httpContextAccessor) : base(context, httpContextAccessor)
		{
			_context = context;
		}

		public async Task<bool> Create(User entity)
		{
			await base.Create(entity);
			_context.Users.Add(entity);
			await _context.SaveChangesAsync();

			return true;
		}

		public async Task<bool> Delete(User entity)
		{
			await base.Delete(entity);
			var entry = _context.Entry(entity);
			entry.Property(x => x.Password).IsModified = false;
            entry.Property(x => x.Salt).IsModified = false;
            await _context.SaveChangesAsync();

			return true;
		}

		public async Task<List<UserDto>> GetAll()
		{
			var usersDto = await _context.Users
					.Where(x => x.State != EnState.Deleted)
					.Select(x => new UserDto(x))
					.ToListAsync();

			return usersDto;
		}

		public async Task<User> GetByEmail(string email)
		{
			var user = await _context.Users
					.AsNoTracking()
					.Where(x => x.Email == email)
					.FirstOrDefaultAsync();

			if (user == null)
				throw new Exception("Usuario no encontrado");

			return user;
		}

		public async Task<User> GetById(int id)
		{
			var user = await _context.Users
					.AsNoTracking()
					.Where(x => x.Id == id)
					.FirstOrDefaultAsync();

			if (user == null)
				throw new Exception("Usuario no encontrado");

			return user;
		}

		public async Task<bool> Update(User entity)
		{
			await base.Update(entity);
			if (String.IsNullOrWhiteSpace(entity.Password))
			{
				var entry = _context.Entry(entity);
				entry.Property(x => x.Password).IsModified = false;
                entry.Property(x => x.Salt).IsModified = false;
            }
			await _context.SaveChangesAsync();

			return true;
		}
	}
}
