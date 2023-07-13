using Server.Models.Enums;

namespace Server.Models
{
    public class TaskItemDto
    {
        public int Id { get; set; }

        public EnState State { get; set; }

        public string Title { get; set; }

        public string? Description { get; set; }

        public DateTime StartDateTime { get; set; }

        public DateTime EndDateTime { get; set; }

        public bool IsAllDay { get; set; }

        public TaskItemDto(TaskItem entity)
        {
            Id = entity.Id;
            State = entity.State;
            Title = entity.Title;
            Description = entity.Description;
            StartDateTime = entity.StartDateTime;
            EndDateTime = entity.EndDateTime;
            IsAllDay = entity.IsAllDay;
        }

        public TaskItemDto()
        {
            
        }
    }
}
