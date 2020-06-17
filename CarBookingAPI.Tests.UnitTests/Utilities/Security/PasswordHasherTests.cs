using CarBookingAPI.Infrastructure.Utilities.Security;
using System;
using Xunit;

namespace CarBookingAPI.Tests.UnitTests.Utilities.Security
{
    public class PasswordHasherTests
    {
        private string _password;
        private string _secret;

        public PasswordHasherTests()
        {
            _password = "MyRandomPassword11";
            _secret = "aksdhaksjhdas";
        }

        [Fact]
        public void WillGenerateHashedPassword()
        {
            var hasher = new PasswordHasher(_secret);

            var hashed = hasher.HashPassword(_password, hasher.CreateSalt());

            Assert.NotEqual("", hashed);
            Assert.NotEqual(_password, hashed);
        }

        [Fact]
        public void WillGeneratePasswordWithEmptySecret()
        {
            var hasher = new PasswordHasher("");

            var hashed = hasher.HashPassword(_password, hasher.CreateSalt());

            Assert.NotEqual(default, hashed);
            Assert.NotEqual(_password, hashed);
        }

        [Fact]
        public void WillFailGeneratingHashDueToEmptyPassword()
        {
            var hasher = new PasswordHasher(_secret);

            Assert.Throws<ArgumentException>(() => hasher.HashPassword("", hasher.CreateSalt()));
        }
    }
}
