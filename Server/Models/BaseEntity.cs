using Server.Models.Enums;
using System.ComponentModel.DataAnnotations;

namespace Server.Models
{
	/// <summary>
	/// Base class for system entities.
	/// </summary>
	public class BaseEntity
	{
		#region Public properties

		[Key]
		public int Id { get; set; }

		public EnState State { get; set; }

		#endregion

		#region Audit properties

		[Required]
		public DateTime Created { get; set; }

		public DateTime? Updated { get; set; }

		public DateTime? Deleted { get; set; }

		[Required]
		public int CreatedUserId { get; set; }

		public int? UpdatedUserId { get; set; }

		public int? DeletedUserId { get; set; }

		#endregion
	}
}
