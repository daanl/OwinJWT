using System.Security.Cryptography;

namespace WebApplication1
{
    /// <summary>
    /// These setting should be retrieved from config file
    /// </summary>
    public static class JwtSettings
    {
        static JwtSettings()
        {
            Issuer = "issuer";
            Audience = "test";

            Secret = new byte[64];
            new RNGCryptoServiceProvider().GetNonZeroBytes(Secret); // this generates a new secret everytime your application restarts
        }

        public static string Issuer { get; set; }
        public static string Audience { get; set; }
        public static byte[] Secret { get; set; }
    }
}