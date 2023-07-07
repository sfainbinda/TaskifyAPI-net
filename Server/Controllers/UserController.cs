using Microsoft.AspNetCore.Mvc;
using Server.Data;
using Server.Models;
using Server.Services;

namespace Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly UserService _service;
        private readonly ApplicationDbContext _context;
        public ILogger Logger { get; }

        public UsersController(ApplicationDbContext context, ILogger<UsersController> logger)
        {
            _context = context;
            _service = new UserService(context);
            Logger = logger;
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                var user = await _service.GetById(id);
                if(user == null)
                    return NotFound();

                await _service.Delete(user);
                return Ok();
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, "Error al eliminar usuario.");
                throw;
            }
        }

        [HttpGet(Name = "GetAll")]
        [ProducesResponseType(200)]
        public async Task<ActionResult<List<UserDto>>> Get()
        {
            try
            {
                var userDtos = await _service.GetAll();
                return Ok(userDtos);
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, "Error al obtener usuarios.");
                throw;
            }
        }

        [HttpGet("{id}", Name = "GetById")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<UserDto>> Get(int id)
        {
            try
            {
                var userDto = await _service.GetById(id);
                if(userDto == null)
                    return NotFound();
            
                return Ok(userDto);
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, "Error al obtener usuario por Id.");
                throw;
            }
        }

        [HttpPost]
        [ProducesResponseType(201)]
        [ProducesResponseType(401)]
        public async Task<ActionResult<UserDto>> Post([FromBody] UserDto entity)
        {
            if(!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                await _service.Save(entity);

                if (!String.IsNullOrEmpty(entity.Password))
                {
                    entity.Password = "";
                    entity.RepeatPassword = "";
                    entity.NewPassword = "";
                }

                return CreatedAtRoute("GetById", new { entity.Id }, entity);
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, "Error al guardar usuario");
                throw;
            }
        }

        [HttpPut("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public async Task<ActionResult<UserDto>> Put(int id, [FromBody] UserDto entity)
        {
            if(!ModelState.IsValid)
                return BadRequest(ModelState);
            
            try
            {
                entity.Id = id;
                await _service.Save(entity);

                if(!String.IsNullOrEmpty(entity.Password))
                {
                    entity.Password = "";
                    entity.RepeatPassword = "";
                    entity.NewPassword = "";
                }

                return Ok(entity);
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, "Error al actualizar usuario.");
                throw;
            }
        }
    }
}
