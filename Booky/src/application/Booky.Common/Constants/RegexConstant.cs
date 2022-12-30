namespace Booky.Common.Constants;

public class RegexConstant
{
    public const string PhoneRegex = @"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$";
    public const string EmailRegex = @"^[^@\s]+@[^@\s]+\.(com|net|org|gov)$";

}