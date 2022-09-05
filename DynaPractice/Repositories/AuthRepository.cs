using DynaPractice.Data;
using DynaPractice.Dtos;
using DynaPractice.Interfaces;
using DynaPractice.Models;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using BC = BCrypt.Net.BCrypt;

namespace DynaPractice.Repositories
{
    public class AuthRepository : IAuthRepository
    {
        private readonly MainDbContext _context;
        private readonly IConfiguration _configuration;
        public AuthRepository(MainDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        public string CreateToken(User user)
        {
            List<Claim> claims = new List<Claim>()
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.UserName.ToString()),
            };

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(
                _configuration.GetSection("JwtSettings:Key").Value));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature);

            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddHours(8),
                signingCredentials: credentials);

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);
            return "Bearer " + jwt;
        }

        public async Task<User> RegisterAsync(UserAuthenticateRequestDto userAuthenticateRequestDto)
        {
            User user = new User();

            user.UserName = userAuthenticateRequestDto.Username;
            user.PasswordHash = BC.HashPassword(userAuthenticateRequestDto.Password);

            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();

            return user;
        }
    }
}
