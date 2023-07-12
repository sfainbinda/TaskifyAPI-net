using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Server.Data;
using Server.Models;
using Server.Services;

namespace Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaskItemsController : ControllerBase
    {
        private readonly TaskItemService _service;
        private readonly ApplicationDbContext _context;
        public ILogger Logger { get; set; }

        public TaskItemsController(ApplicationDbContext context, ILogger<UsersController> logger)
        {
            _service = new TaskItemService(context);
            _context = context;
            Logger = logger;
        }

        [HttpGet("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<TaskItemDto>> Get(int id)
        {
            try
            {
                var taskItem = await _service.GetById(id);
                if (taskItem == null)
                    return NotFound();

                return Ok(new TaskItemDto(taskItem));
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, "Error al obtener la tarea por Id.");
                throw;
            }
        }

        [HttpPost]
        public async Task<ActionResult<TaskItemDto>> Post([FromBody] TaskItemDto entity)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var taskItem = new TaskItem(entity);
                await _service.Save(taskItem);

                var newtaskItem = await _service.GetById(entity.Id);

                return CreatedAtAction(nameof(Get), new { id = entity.Id }, new TaskItemDto(newtaskItem));
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, "Error al crear tarea.");
                throw;
            }
        }
    }
}
