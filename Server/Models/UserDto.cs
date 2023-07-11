using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Server.Models.Enums;

namespace Server.Models
{
	public class UserDto
	{
		public int Id { get; set; }

		public EnState State { get; set; }

		[Required]
		[MaxLength(50)]
		public string? FirstName { get; set; }

		[Required]
		[MaxLength(50)]
		public string? LastName { get; set; }

		[Required]
		[MaxLength(50)]
		[DataType(DataType.EmailAddress)]
		public string? Email { get; set; }

		[MaxLength(50)]
		[DataType(DataType.Password)]
		public string? Password { get; set; }

		[MaxLength(50)]
		[DataType(DataType.Password)]
		public string? RepeatPassword { get; set; }

		[MaxLength(50)]
		[DataType(DataType.Password)]
		public string? NewPassword { get; set; }

		public UserDto()
		{

		}

		public UserDto(User entity)
		{
			Id = entity.Id;
			State = entity.State;
			FirstName = entity.FirstName;
			LastName = entity.LastName;
			Email = entity.Email;
		}
	}
}
