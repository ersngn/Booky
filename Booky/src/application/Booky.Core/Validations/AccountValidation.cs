using System.Text.RegularExpressions;
using Booky.Common.Constants;

namespace Booky.Core.Validations;

public static class AccountValidation
{
    public static bool IsEmailValid(string email)
    {
        var regex = RegexConstant.EmailRegex;

        return Regex.IsMatch(email, regex, RegexOptions.IgnoreCase);
    }

    public static bool IsPhoneNumber(string? number)
    {
        var regex = RegexConstant.PhoneRegex;
        if (number != null) return Regex.IsMatch(number, regex);
        else return false;
    }
}