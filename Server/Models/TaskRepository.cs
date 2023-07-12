﻿using Microsoft.EntityFrameworkCore;
using Server.Data;
using Server.Interfaces;
using Server.Models.Enums;

namespace Server.Models
{
    public class TaskItemRepository : BaseRepository, IRepository<TaskItem>
    {
        private readonly ApplicationDbContext _context;

        public TaskItemRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<bool> Create(TaskItem entity)
        {
            await base.Create(entity);
            _context.TaskItems.Add(entity);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<List<TaskItemDto>> GetAll()
        {
            var taskItemsDto = await _context.TaskItems
                .Where(x => x.State != EnState.Deleted)
                .Select(x => new TaskItemDto(x))
                .ToListAsync();

            return taskItemsDto;
        }

        public async Task<TaskItem> GetById(int id)
        {
            var taskItem = await _context.TaskItems
                .AsNoTracking()
                .Where(x => x.Id == id)
                .FirstOrDefaultAsync();

            if (taskItem == null)
            {
                throw new Exception("Tarea no encontrada.");
            }

            return taskItem;
        }

        public async Task<bool> Delete(TaskItem entity)
        {
            await base.Delete(entity);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<bool> Update(TaskItem entity)
        {
            await base.Update(entity);
            await _context.SaveChangesAsync();

            return true;
        }
    }
}
