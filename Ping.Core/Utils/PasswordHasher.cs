namespace Ping.Core.Utils;

public static class PasswordHasher
{
    public static string HashPassword(string plainTextPassword)
        => BCrypt.Net.BCrypt.HashPassword(plainTextPassword);

    public static bool VerifyPassword(string plainTextPassword, string hashedPassword)
        => BCrypt.Net.BCrypt.Verify(plainTextPassword, hashedPassword);
}