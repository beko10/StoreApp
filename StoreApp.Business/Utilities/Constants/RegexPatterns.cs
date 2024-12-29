namespace StoreApp.Business.Utilities.Constants;

public static class RegexPatterns
{
    public const string OnlyLettersAndNumbers = @"^[a-zA-ZğüşıöçĞÜŞİÖÇ0-9 ]*$";
    public const string OnlyLetters = @"^[a-zA-ZğüşıöçĞÜŞİÖÇ ]*$";
    public const string Email = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";
    public const string Phone = @"^[0-9]{10}$";
    public const string Password = @"^(?=.*[A-Za-z])(?=.*\d)[A-Za-z\d]{8,}$";
}