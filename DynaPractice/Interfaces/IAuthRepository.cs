using DynaPractice.Dtos;
using DynaPractice.Models;

namespace DynaPractice.Interfaces
{
    public interface IAuthRepository
    {
        Task<User> RegisterAsync(UserAuthenticateRequestDto userAuthenticateRequestDto);
        string CreateToken(User user);
    }
}
