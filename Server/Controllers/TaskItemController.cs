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

        [HttpDelete("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                var taskItem = await _service.GetById(id);
                
                if (taskItem == null)
                    return NotFound();

                await _service.Delete(taskItem);
                return Ok();
            }
            catch(Exception ex)
            {
                Logger.LogError(ex, "Error al eliminar tarea.");
                throw;
            }
        }

        [HttpGet]
        [ProducesResponseType(200)]
        public async Task<ActionResult<TaskItemDto>> Get()
        {
            try
            {
                var taskItems = await _service.GetAll();
                return Ok(taskItems);
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, "Error al obtener las tareas.");
                throw;
            }
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
        [ProducesResponseType(201)]
        [ProducesResponseType(401)]
        public async Task<ActionResult<TaskItemDto>> Post([FromBody] TaskItemDto entity)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var taskItem = new TaskItem(entity);
                await _service.Save(taskItem);

                var newtaskItem = await _service.GetById(taskItem.Id);

                return CreatedAtAction(nameof(Get), new { id = taskItem.Id }, new TaskItemDto(newtaskItem));
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, "Error al crear tarea.");
                throw;
            }
        }

        [HttpPut("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public async Task<ActionResult> Put(int id, [FromBody] TaskItemDto entity)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                entity.Id = id;
                var taskItem = new TaskItem(entity);
                await _service.Save(taskItem);

                var updatedTaskItem = await _service.GetById(entity.Id);

                return Ok(new TaskItemDto(updatedTaskItem));
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, "Error al actualizar tarea.");
                throw;
            }
        }
    }
}
