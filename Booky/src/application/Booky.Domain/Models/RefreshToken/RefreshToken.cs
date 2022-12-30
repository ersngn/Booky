using System.ComponentModel.DataAnnotations.Schema;

namespace Booky.Domain.Models;

public class RefreshToken
{
    public int Id { get; set; }

    public string UserId { get; set; }
    public string Token { get; set; }
    public string JwtId { get; set; }

    public bool IsRevoked { get; set; }
    public DateTime DateAdded { get; set; }
    public DateTime DateExpire { get; set; }

    [ForeignKey(nameof(UserId))]
    public User.User User { get; set; }
}