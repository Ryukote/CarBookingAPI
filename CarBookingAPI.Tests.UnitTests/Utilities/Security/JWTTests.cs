using CarBookingAPI.Infrastructure.Utilities.Security;
using CarBookingAPI.Infrastructure.ViewModels;
using Microsoft.Extensions.Options;
using Moq;
using System;
using Xunit;

namespace CarBookingAPI.Tests.UnitTests.Utilities.Security
{
    public class JWTTests
    {
        IOptions<AppSettings> _options;

        public JWTTests()
        {
            var mock = new Mock<IOptions<AppSettings>>();
            mock.Setup(x => x.Value).Returns(new AppSettings());
            mock.Object.Value.JWTSecret = "GdCn8Hau0igxs5EvAbrFDTLpQ1ZtcuQf";
            _options = mock.Object;
        }

        [Fact]
        public void WillGenerateValidToken()
        {
            var mockedUser = new Mock<UsersViewModel>().Object;
            mockedUser.Username = "User1";

            var roles = new string[] { "RegisteredUser" };

            var jwt = new JWT(_options.Value.JWTSecret).CreateAccessToken(mockedUser, roles);

            Assert.NotEmpty(jwt);
            Assert.NotNull(jwt);
        }

        [Fact]
        public void WillNotGenerateValidToken()
        {
            var mockedUser = new Mock<UsersViewModel>().Object;

            _options.Value.JWTSecret = "";
            var roles = new string[] { "RegisteredUser" };

            Assert.Throws<ArgumentNullException>(() => new JWT(_options.Value.JWTSecret).CreateAccessToken(mockedUser, roles));
        }
    }
}
