using System.Security.Cryptography;
using System.Text;
using Booky.Common.Constants;
using Booky.Domain.Dtos.Security;

namespace Booky.Core.Helpers.Generate;

public class GenerateKey
{
    public static GenerateTextDto GenerateTextKey(int size)
    {
        var data = new byte[4 * size];
        using (var crypto = RandomNumberGenerator.Create())
        {
            crypto.GetBytes(data);
        }

        var result = new StringBuilder(size);
        for (var i = 0; i < size; i++)
        {
            var rnd = BitConverter.ToUInt32(data, i * 4);
            var idx = rnd % SecurityConstant.Chars.Length;

            result.Append(SecurityConstant.Chars[idx]);
        }

        return new GenerateTextDto
        {
            GeneratedText = result.ToString()
        };
    }
}