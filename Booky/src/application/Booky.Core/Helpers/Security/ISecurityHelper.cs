using Booky.Domain.Dtos.Security;

namespace Booky.Core.Helpers.Security;

public interface ISecurityHelper
{
    EncyrptTextDto Encrypt(string sourceText);
    DecryptTextDto Decrypt(string sourceText);
    HashedTextDto CreateHash(string value, string salt);
    HashedTextDto CreateHash();

    ValidateHashDto ValidateHash(string value, string salt, string hash);
    //GenerateTextDto GenerateKey(int size);
}