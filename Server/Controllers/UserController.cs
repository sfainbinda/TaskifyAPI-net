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

        public UsersController(ApplicationDbContext context)
        {
            _context = context;
            _service = new UserService(context);
        }

        [HttpGet("{id}", Name = "GetById")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<UserDto>> GetById(int id)
        {
            var userDto = await _service.GetById(id);
            if (userDto == null)
                return NotFound();
            
            return Ok(userDto);
        }

        [HttpPost]
        [ProducesResponseType(201)]
        [ProducesResponseType(401)]
        public async Task<ActionResult<UserDto>> Post([FromBody] UserDto userDto)
        {
            //if (!ModelState.IsValid)
            //    return BadRequest(ModelState);

            var user = new User
            {
                Id = userDto.Id,
                FirstName = userDto.FirstName,
                LastName = userDto.LastName,
                Email = userDto.Email,
                Password = userDto.Password
            };

            await _service.Save(user);
            var response = new UserDto(user);

            return CreatedAtRoute("GetById", new { Id = userDto.Id }, response);
        }

        //[HttpPut]
        //public async Task<ActionResult<UserDto>> Put([FromBody] User user)
    }
}
