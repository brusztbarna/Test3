using DynaPractice.Data;
using DynaPractice.Dtos;
using DynaPractice.Interfaces;
using DynaPractice.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BC = BCrypt.Net.BCrypt;

namespace DynaPractice.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly MainDbContext _context;
        private readonly IAuthRepository _repository;

        public AuthController(MainDbContext context,IAuthRepository repository)
        {
            _context = context;
            _repository = repository;
        }

        [HttpPost]
        [Route("Register")]
        public async Task<ActionResult<User>> Register(UserAuthenticateRequestDto userAuthenticateRequestDto) 
        {
            if (userAuthenticateRequestDto == null) 
            {
                return BadRequest();
            }

            if (_context.Users.Any(u => u.UserName == userAuthenticateRequestDto.Username)) 
            {
                return BadRequest("Username already exist!");
            }

            User user = await _repository.RegisterAsync(userAuthenticateRequestDto);

            return Ok(user);
        }

        [HttpPost]
        [Route("Login")]
        public async Task<ActionResult> Login(UserAuthenticateRequestDto userAuthenticateRequestDto) 
        {
            if (userAuthenticateRequestDto == null)
            {
                return BadRequest();
            }

            var user =  await _context.Users.FirstOrDefaultAsync(u => u.UserName == userAuthenticateRequestDto.Username);

            if (user == null || !BC.Verify(userAuthenticateRequestDto.Password, user.PasswordHash)) 
            {
                return BadRequest("Incorrect username or password!");
            }

            string token = _repository.CreateToken(user);

            return Ok(token);
        }
    }
}
