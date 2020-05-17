namespace CarBookingAPI.Core.Contracts.Utilities
{
    /// <summary>
    /// Object for hashing password.
    /// </summary>
    public interface IHasher
    {
        /// <summary>
        /// Create salt for password hashing.
        /// </summary>
        /// <returns>Byte array of created salt.</returns>
        byte[] CreateSalt();

        /// <summary>
        /// Create hashed password based on plain text password and salt.
        /// </summary>
        /// <param name="password">Plain text password.</param>
        /// <param name="salt">Byte array of salt.</param>
        /// <returns>Hashed password</returns>
        string HashPassword(string password, byte[] salt);
    }
}
