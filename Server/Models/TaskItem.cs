using Microsoft.EntityFrameworkCore.ChangeTracking;
using Server.Models;
using System.ComponentModel.DataAnnotations;

namespace Server.Models
{
    public class TaskItem : BaseEntity
    {
        [Required]
        [MaxLength(255)]
        public string Title { get; set; }

        [MaxLength(500)]
        public string? Description { get; set; }

        [Required]
        public DateTime StartDateTime { get; set; }

        public DateTime EndDateTime { get; set; }

        public bool IsAllDay { get; set; }

        public TaskItem(TaskItemDto entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));
            
            Id = entity.Id;
            State = entity.State;

            Title = entity.Title;
            Description = entity.Description;
            IsAllDay = entity.IsAllDay;

            if (entity.IsAllDay)
            {
                StartDateTime = entity.StartDateTime.Date;
                EndDateTime = entity.StartDateTime.Date.AddDays(1);
            }
            else
            {
                StartDateTime = entity.StartDateTime;
                EndDateTime = entity.EndDateTime;

                if (EndDateTime < StartDateTime)
                {
                    throw new InvalidOperationException("La fecha de fin no puede ser menor a la fecha de inicio.");
                }
            }
        }

        public TaskItem()
        {
            
        }
    }
}