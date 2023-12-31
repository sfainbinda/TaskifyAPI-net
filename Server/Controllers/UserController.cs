﻿using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Server.Configuration;
using Server.Data;
using Server.Models;
using Server.Services;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Server.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
	public class UsersController : ControllerBase
	{
		private readonly UserService _service;
		private readonly ApplicationDbContext _context;
		private readonly AuthenticationService _authenticationService;
		private readonly AppSettings? _appSettings;
		private readonly PasswordHasher _passwordHasher;

		public ILogger Logger { get; }

		public UsersController(ApplicationDbContext context, ILogger<UsersController> logger, IOptionsSnapshot<AppSettings> appSettings, IHttpContextAccessor httpContextAccessor)
		{
			_context = context;
			_service = new UserService(context, httpContextAccessor);
			Logger = logger;
			_authenticationService = new AuthenticationService(appSettings);
			_appSettings = appSettings.Value;
			_passwordHasher = new PasswordHasher();
		}

		[HttpDelete("{id}")]
		[ProducesResponseType(200)]
		[ProducesResponseType(404)]
		public async Task<ActionResult> Delete(int id)
		{
			try
			{
				var user = await _service.GetById(id);
				if (user == null)
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
				var user = await _service.GetById(id);
				if (user == null)
					return NotFound();

				return Ok(new UserDto(user));
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
			if (!ModelState.IsValid)
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

				var newUser = await _service.GetById(entity.Id);
				var newUserDto = new UserDto(newUser);

				return CreatedAtRoute("GetById", new { id = entity.Id }, newUserDto);
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
			if (!ModelState.IsValid)
				return BadRequest(ModelState);

			try
            {
                entity.Id = id;
				await _service.Save(entity);

                var updatedUser = await _service.GetById(entity.Id);
				var updatedUserDto = new UserDto(updatedUser);

                return Ok(updatedUserDto);
			}
			catch (Exception ex)
			{
				Logger.LogError(ex, "Error al actualizar usuario.");
				throw;
			}
		}

        [HttpPost("SignIn", Name = "SignIn")]
		[AllowAnonymous]
        public async Task<ActionResult<UserDto>> SignIn(UserSignIn userSignIn)
        {
            try
            {
                var user = await _service.GetByEmail(userSignIn.Email);

                if (user == null)
                    return NotFound();

                var verifiedPassword = _passwordHasher.VerifyPassword(userSignIn.Password, user.Password!, Convert.FromBase64String(user.Salt));

                if (verifiedPassword)
                {
                    var token = _authenticationService.GenerateToken(user);

                    var cookieOptions = new CookieOptions
                    {
                        Expires = DateTime.Now.AddDays(7),
                        Secure = true,
                        HttpOnly = true,
                        SameSite = SameSiteMode.Strict
                    };

                    Response.Cookies.Append(_appSettings?.TokenAuthentication?.CookieToken!, token, cookieOptions);
                    Response.Cookies.Append(_appSettings?.TokenAuthentication?.CookieUsername!, user.Email!, cookieOptions);

                    return Ok(token);
                }
                else
                    return BadRequest();
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, "Error al iniciar sesión. Credenciales inválidas.");
                throw;
            }
        }

        [HttpGet("LogOut", Name = "LogOut")]
        public ActionResult LogOut()
        {
            try
            {
				Response.Cookies.Delete(_appSettings?.TokenAuthentication?.CookieToken!);
                Response.Cookies.Delete(_appSettings?.TokenAuthentication?.CookieUsername!);
				return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
    }
}
