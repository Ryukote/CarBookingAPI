using Bogus;
using CarBookingAPI.Infrastructure.Handlers;
using CarBookingAPI.Infrastructure.Utilities.Mappers;
using CarBookingAPI.Infrastructure.ViewModels;
using CarBookingAPI.Tests.Shared;
using Microsoft.Extensions.Options;
using Moq;
using System;
using System.Threading.Tasks;
using Xunit;

namespace CarBookingAPI.Tests.IntegrationTests.Handlers
{
    public class UsersHandlerTests
    {
        IOptions<AppSettings> _options;

        public UsersHandlerTests()
        {
            var mock = new Mock<IOptions<AppSettings>>();
            mock.Setup(x => x.Value).Returns(new AppSettings());
            mock.Object.Value.HashSecret = "jagdkjashdashdasjdlkasdklas";
            _options = mock.Object;
        }

        [Fact]
        public async Task WillAddUser()
        {
            var user = new FakeUser().CreateSingle();
            var context = new InMemoryContext().Create(Guid.NewGuid().ToString());
            var userMapper = new UsersMapper();
            var credentialsMapper = new CredentialsMapper();

            var handler = new UsersHandler(context, _options.Value.HashSecret, credentialsMapper, userMapper);

            var result = await handler.Add(user);

            Assert.NotEqual(default, result.Id);
            Assert.NotEqual(default, result.Username);
        }

        [Fact]
        public async Task WillGetUser()
        {
            var user = new FakeUser().CreateSingle();
            var context = new InMemoryContext().Create(Guid.NewGuid().ToString());

            var userMapper = new UsersMapper();
            var credentialsMapper = new CredentialsMapper();

            var handler = new UsersHandler(context, _options.Value.HashSecret, credentialsMapper, userMapper);
            var result = await handler.Add(user);

            var finded = await handler.GetById(result.Id);

            Assert.NotNull(finded);
            Assert.NotEqual(default, finded.Id);
            Assert.NotEqual(default, finded.Username);
        }

        [Fact]
        public async Task WillNotGetUser()
        {
            var user = new FakeUser().CreateSingle();
            var context = new InMemoryContext().Create(Guid.NewGuid().ToString());

            var userMapper = new UsersMapper();
            var credentialsMapper = new CredentialsMapper();

            var handler = new UsersHandler(context, _options.Value.HashSecret, credentialsMapper, userMapper);

            var finded = await handler.GetById(default);

            Assert.Null(finded);
        }

        [Fact]
        public async Task WillUpdateUserPassword()
        {
            var user = new FakeUser().CreateSingle();
            var context = new InMemoryContext().Create(Guid.NewGuid().ToString());

            var userMapper = new UsersMapper();
            var credentialsMapper = new CredentialsMapper();

            var handler = new UsersHandler(context, _options.Value.HashSecret, credentialsMapper, userMapper);
            var result = await handler.Add(user);

            CredentialsViewModel updatedUser = new CredentialsViewModel();
            updatedUser.Id = result.Id;
            updatedUser.Username = result.Username;
            updatedUser.Password = new Faker<CredentialsViewModel>()
                .RuleFor(x => x.Password, y => y.Random.AlphaNumeric(10))
                .Generate()
                .Password;

            var updated = await handler.Update(updatedUser);

            Assert.NotNull(updated);
            Assert.NotEqual(default, updated.Id);
            Assert.NotEqual(default, updated.Username);
        }

        [Fact]
        public async Task WillDeleteUser()
        {
            var user = new FakeUser().CreateSingle();
            var context = new InMemoryContext().Create(Guid.NewGuid().ToString());

            var userMapper = new UsersMapper();
            var credentialsMapper = new CredentialsMapper();

            var handler = new UsersHandler(context, _options.Value.HashSecret, credentialsMapper, userMapper);
            var result = await handler.Add(user);

            var deleted = await handler.Delete(result.Id);

            Assert.True(deleted > 0);
        }

        [Fact]
        public async Task WillGetSpecificUsers()
        {
            var firstUser = new FakeUser().CreateSingle();
            firstUser.Username = "Username1";

            var secondUser = new FakeUser().CreateSingle();
            secondUser.Username = "Username2";

            var thirdUser = new FakeUser().CreateSingle();
            thirdUser.Username = "OtherUser";

            var context = new InMemoryContext().Create(Guid.NewGuid().ToString());

            var userMapper = new UsersMapper();
            var credentialsMapper = new CredentialsMapper();

            var handler = new UsersHandler(context, _options.Value.HashSecret, credentialsMapper, userMapper);

            var firstAddedUser = await handler.Add(firstUser);
            var secondAddedUser = await handler.Add(secondUser);
            var thirdAddedUser = await handler.Add(thirdUser);

            var userSearch = new QueryViewModel<UsersViewModel>()
            {
                QueryData = new UsersViewModel()
                {
                    Username = "username"
                },
                Skip = 0,
                Take = 10
            };

            var selected = await handler.GetPaginated(userSearch);

            Assert.NotNull(selected);
            Assert.Equal(2, selected.Count);
        }

        [Fact]
        public async Task WillNotGetSpecificUsers()
        {
            var firstUser = new FakeUser().CreateSingle();
            firstUser.Username = "Username1";

            var secondUser = new FakeUser().CreateSingle();
            secondUser.Username = "Username2";

            var thirdUser = new FakeUser().CreateSingle();
            thirdUser.Username = "OtherUser";

            var context = new InMemoryContext().Create(Guid.NewGuid().ToString());

            var userMapper = new UsersMapper();
            var credentialsMapper = new CredentialsMapper();

            var handler = new UsersHandler(context, _options.Value.HashSecret, credentialsMapper, userMapper);

            var firstAddedUser = await handler.Add(firstUser);
            var secondAddedUser = await handler.Add(secondUser);
            var thirdAddedUser = await handler.Add(thirdUser);

            var userSearch = new QueryViewModel<UsersViewModel>()
            {
                QueryData = new UsersViewModel()
                {
                    Username = "cow"
                },
                Skip = 0,
                Take = 10
            };

            var selected = await handler.GetPaginated(userSearch);

            Assert.Null(selected);
        }
    }
}
