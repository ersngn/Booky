using System.Security.Cryptography;
using System.Text;
using Booky.Common.Constants;
using Booky.Domain.Dtos.Security;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.AspNetCore.DataProtection;

namespace Booky.Core.Helpers.Security;

public class SecurityHelper : ISecurityHelper
{
    private readonly IDataProtectionProvider _dataProtectionProvider;

    public SecurityHelper(IDataProtectionProvider dataProtectionProvider)
    {
        _dataProtectionProvider = dataProtectionProvider;
    }

    public EncyrptTextDto Encrypt(string sourceText)
    {
        var protector = _dataProtectionProvider.CreateProtector(SecurityConstant.Key);
        return new EncyrptTextDto
        {
            EncryptedText = protector.Protect(sourceText)
        };
    }

    public DecryptTextDto Decrypt(string sourceText)
    {
        var protector = _dataProtectionProvider.CreateProtector(SecurityConstant.Key);
        return new DecryptTextDto
        {
            DecryptedText = protector.Unprotect(sourceText)
        };
    }

    public HashedTextDto CreateHash(string value, string salt)
    {
        var valueBytes = KeyDerivation.Pbkdf2(
            value,
            Encoding.UTF8.GetBytes(salt),
            KeyDerivationPrf.HMACSHA512,
            10000,
            256 / 8);

        return new HashedTextDto
        {
            HashedText = Convert.ToBase64String(valueBytes) + "æ" + salt
        };
    }

    public HashedTextDto CreateHash()
    {
        var randomBytes = new byte[128 / 8];
        using var generator = RandomNumberGenerator.Create();
        generator.GetBytes(randomBytes);
        return new HashedTextDto
        {
            HashedText = Convert.ToBase64String(randomBytes)
        };
    }

    public ValidateHashDto ValidateHash(string value, string salt, string hash)
    {
        return new ValidateHashDto
        {
            isValidated = CreateHash(value, salt).HashedText?.Split('æ')[0] == hash
        };
    }
}