using Booky.Domain.Models;

namespace Booky.Repository.User;

public interface IUserRepository
{
    Task CreateToken(RefreshToken token);
}