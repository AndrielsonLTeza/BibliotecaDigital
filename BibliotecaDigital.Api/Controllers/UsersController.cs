using BibliotecaDigital.Core.DTOs;
using BibliotecaDigital.Core.Entities;
using BibliotecaDigital.Core.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BibliotecaDigital.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IPasswordHashService _passwordHashService;

        public UsersController(IUserService userService, IPasswordHashService passwordHashService)
        {
            _userService = userService;
            _passwordHashService = passwordHashService;
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Register([FromBody] UserRegistrationDto model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            // Verificar se o usuário já existe
            if (await _userService.UserExistsByEmail(model.Email))
                return BadRequest("Email já está em uso");

            // Criar o usuário com senha criptografada
            var user = new User
            {
                UserName = model.UserName,
                Email = model.Email,
                Password = _passwordHashService.HashPassword(model.Password),
                Roles = new List<string> { "User" } // Role padrão
            };

            await _userService.CreateUser(user);

            return CreatedAtAction(nameof(GetUserById), new { id = user.Id },
                new UserResponseDto { Id = user.Id, UserName = user.UserName, Email = user.Email });
        }

        [HttpGet("{id}")]
        [Authorize]
        public async Task<IActionResult> GetUserById(int id)
        {
            var user = await _userService.GetUserById(id);
            if (user == null)
                return NotFound();

            // Não retornar a senha, mesmo que esteja hasheada
            return Ok(new UserResponseDto { Id = user.Id, UserName = user.UserName, Email = user.Email });
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetAllUsers()
        {
            // Implementação para listar todos os usuários (apenas para administradores)
            // Esta é uma funcionalidade adicional que você pode implementar
            return Ok("Lista de usuários - apenas para administradores");
        }
    }
}