namespace Ping.Core.Utils;

public static class PasswordHasher
{
    public static string HashPassword(string plainTextPassword)
        => BCrypt.Net.BCrypt.HashPassword(plainTextPassword);
}