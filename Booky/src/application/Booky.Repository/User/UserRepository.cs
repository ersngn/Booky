using Booky.Domain.Models;
using Booky.Infrastructure;

namespace Booky.Repository.User;

public class UserRepository:IUserRepository
{
    private readonly ApplicationDbContext _context;

    public UserRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task CreateToken(RefreshToken token)
    {
        await _context.RefreshTokens.AddAsync(token);
        await _context.SaveChangesAsync();
    }
}