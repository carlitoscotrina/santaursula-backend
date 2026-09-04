using System;
using System.Security.Cryptography;
using System.Text;

namespace SantaUrsula.API.Services;

#pragma warning disable SYSLIB0060
public static class PasswordHasher
{
    // Format: iterations:saltBase64:hashBase64
    public static string HashPassword(string password, int iterations = 100_000)
    {
        using var rng = RandomNumberGenerator.Create();
        byte[] salt = new byte[16];
        rng.GetBytes(salt);

        using var pbkdf2 = new Rfc2898DeriveBytes(password, salt, iterations, HashAlgorithmName.SHA256);
        byte[] hash = pbkdf2.GetBytes(32);

        return $"{iterations}:{Convert.ToBase64String(salt)}:{Convert.ToBase64String(hash)}";
    }

    public static bool Verify(string hashedPassword, string providedPassword)
    {
        if (string.IsNullOrEmpty(hashedPassword)) return false;
        var parts = hashedPassword.Split(':');
        if (parts.Length != 3) return false;

        if (!int.TryParse(parts[0], out int iterations)) return false;
        byte[] salt = Convert.FromBase64String(parts[1]);
        byte[] hash = Convert.FromBase64String(parts[2]);

        using var pbkdf2 = new Rfc2898DeriveBytes(providedPassword, salt, iterations, HashAlgorithmName.SHA256);
        byte[] attempted = pbkdf2.GetBytes(hash.Length);

        return CryptographicOperations.FixedTimeEquals(attempted, hash);
    }
}
#pragma warning restore SYSLIB0060
