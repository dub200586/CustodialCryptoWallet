using AutoFixture;
using AutoMapper;
using CustodialCryptoWallet.Dal;
using CustodialCryptoWallet.Dal.DataModels;
using CustodialCryptoWallet.Dal.Models;
using CustodialCryptoWallet.Dal.Repositories;
using CustodialCryptoWallet.Dal.Repositories.Interfaces;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;

namespace CustodialCryptoWallet.Tests.RepositoryTests
{
    public class UserRepositoryTests : IDisposable
    {
        private const string EmailExample = "user@example.com";

        private readonly CustodialCryptoWalletContext _context;
        private readonly IMapper _mapper;
        private readonly IUserRepository _userRepository;
        private readonly Fixture _fixture;

        public UserRepositoryTests()
        {
            var options = new DbContextOptionsBuilder<CustodialCryptoWalletContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            _context = new CustodialCryptoWalletContext(options);
            _mapper = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<User, UserDataModel>().ReverseMap();
            }).CreateMapper();

            _userRepository = new UserRepository(_mapper, _context);
            _fixture = new Fixture();
        }

        public void Dispose()
        {
            _context.Database.EnsureDeleted();
            _context.Dispose();
        }

        [Fact]
        public async Task GetUserByIdAsync_ShouldReturnUser_WhenUserExists()
        {
            var user = _fixture.Create<User>();
            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            var result = await _userRepository.GetUserByIdAsync(user.UserId);

            result.Should().NotBeNull();
            result.UserId.Should().Be(user.UserId);
        }

        [Fact]
        public async Task GetUserByIdAsync_ShouldReturnNull_WhenUserDoesNotExist()
        {
            var userId = _fixture.Create<Guid>();

            var result = await _userRepository.GetUserByIdAsync(userId);

            result.Should().BeNull();
        }

        [Fact]
        public async Task GetUserByEmailAsync_ShouldReturnUser_WhenEmailExists()
        {
            var user = _fixture.Create<User>();
            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            var result = await _userRepository.GetUserByEmailAsync(user.Email);

            result.Should().NotBeNull();
            result.Email.Should().Be(user.Email);
        }

        [Fact]
        public async Task GetUserByEmailAsync_ShouldReturnNull_WhenEmailDoesNotExist()
        {
            var result = await _userRepository.GetUserByEmailAsync(EmailExample);

            result.Should().BeNull();
        }

        [Fact]
        public async Task CreateUserAsync_ShouldAddUserToDatabase()
        {
            var userDataModel = _fixture.Create<UserDataModel>();

            var createdUser = await _userRepository.CreateUserAsync(userDataModel);
            await _context.SaveChangesAsync();

            var userInDb = await _context.Users.FindAsync(createdUser.UserId);
            userInDb.Should().NotBeNull();
            userInDb.Email.Should().Be(userDataModel.Email);
        }

        [Fact]
        public async Task UpdateUserAsync_ShouldModifyUserData()
        {
            var user = _fixture.Create<User>();
            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            _context.Entry(user).State = EntityState.Detached;

            var updatedDataModel = _mapper.Map<UserDataModel>(user);
            updatedDataModel.Email = EmailExample;

            await _userRepository.UpdateUserAsync(updatedDataModel);
            await _context.SaveChangesAsync();

            var updatedUser = await _context.Users.FindAsync(user.UserId);
            updatedUser.Email.Should().Be(EmailExample);
        }
    }
}