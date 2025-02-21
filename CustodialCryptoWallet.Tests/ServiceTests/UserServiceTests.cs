using AutoFixture;
using AutoMapper;
using CustodialCryptoWallet.Bll.Models;
using CustodialCryptoWallet.Bll.Services;
using CustodialCryptoWallet.Dal.DataModels;
using CustodialCryptoWallet.Dal.Repositories.Interfaces;
using FluentAssertions;
using Microsoft.EntityFrameworkCore.Storage;
using Moq;

namespace CustodialCryptoWallet.Tests.ServiceTests
{
    public class UserServiceTests
    {
        private readonly Mock<IUserRepository> _userRepositoryMock;
        private readonly Mock<IUnitOfWork> _unitOfWorkMock;
        private readonly Mock<IMapper> _mapperMock;
        private readonly UserService _userService;
        private readonly Fixture _fixture;
        private readonly Mock<IDbContextTransaction> _transaction;

        public UserServiceTests()
        {
            _userRepositoryMock = new Mock<IUserRepository>();
            _unitOfWorkMock = new Mock<IUnitOfWork>();
            _mapperMock = new Mock<IMapper>();
            _userService = new UserService(_mapperMock.Object, _unitOfWorkMock.Object, _userRepositoryMock.Object);
            _fixture = new Fixture();
            _transaction = new Mock<IDbContextTransaction>();
        }

        [Fact]
        public async Task CreateUserAsync_ShouldReturnUserModel_WhenUserIsCreated()
        {
            var userModel = _fixture.Create<UserModel>();
            var userDataModel = _fixture.Create<UserDataModel>();

            _userRepositoryMock.Setup(r => r.GetUserByEmailAsync(userModel.Email)).ReturnsAsync((UserDataModel)null);
            _mapperMock.Setup(m => m.Map<UserDataModel>(userModel)).Returns(userDataModel);
            _userRepositoryMock.Setup(r => r.CreateUserAsync(userDataModel)).ReturnsAsync(userDataModel);
            _mapperMock.Setup(m => m.Map<UserModel>(userDataModel)).Returns(userModel);

            var result = await _userService.CreateUserAsync(userModel);

            result.Should().BeEquivalentTo(userModel);
        }

        [Fact]
        public async Task GetUserByIdAsync_ShouldReturnUserModel_WhenUserExists()
        {
            var userId = _fixture.Create<Guid>();
            var userDataModel = _fixture.Create<UserDataModel>();
            var userModel = _fixture.Create<UserModel>();

            _userRepositoryMock.Setup(r => r.GetUserByIdAsync(userId)).ReturnsAsync(userDataModel);
            _mapperMock.Setup(m => m.Map<UserModel>(userDataModel)).Returns(userModel);

            var result = await _userService.GetUserByIdAsync(userId);

            result.Should().BeEquivalentTo(userModel);
        }

        [Fact]
        public async Task DepositMoneyToCurrencyAccountAsync_ShouldIncreaseBalance()
        {
            var userId = _fixture.Create<Guid>();
            var amount = _fixture.Create<decimal>();
            var userDataModel = _fixture.Build<UserDataModel>().With(u => u.Balance, 100m).Create();
            var updatedUserModel = _fixture.Create<UserModel>();

            _unitOfWorkMock.Setup(u => u.BeginTransaction()).Returns(_transaction.Object);
            _userRepositoryMock.Setup(r => r.GetUserByIdAsync(userId)).ReturnsAsync(userDataModel);
            _userRepositoryMock.Setup(r => r.UpdateUserAsync(userDataModel)).Returns(Task.CompletedTask);
            _mapperMock.Setup(m => m.Map<UserModel>(userDataModel)).Returns(updatedUserModel);

            var result = await _userService.DepositMoneyToCurrencyAccountAsync(userId, amount);

            result.Should().BeEquivalentTo(updatedUserModel);
            userDataModel.Balance.Should().Be(100m + amount);
        }

        [Fact]
        public async Task WithdrawMoneyFromCurrencyAccountAsync_ShouldDecreaseBalance()
        {
            var userId = _fixture.Create<Guid>();
            var amount = 50m;
            var userDataModel = _fixture.Build<UserDataModel>().With(u => u.Balance, 100m).Create();
            var updatedUserModel = _fixture.Build<UserModel>().With(u => u.Balance, 100m).Create();

            _unitOfWorkMock.Setup(u => u.BeginTransaction()).Returns(_transaction.Object);
            _userRepositoryMock.Setup(r => r.GetUserByIdAsync(userId)).ReturnsAsync(userDataModel);
            _userRepositoryMock.Setup(r => r.UpdateUserAsync(It.IsAny<UserDataModel>()))
                .Returns(Task.CompletedTask);
            _mapperMock.Setup(m => m.Map<UserModel>(userDataModel)).Returns(updatedUserModel);

            var result = await _userService.WithdrawMoneyFromCurrencyAccountAsync(userId, amount);

            result.Should().BeEquivalentTo(updatedUserModel);
            userDataModel.Balance.Should().Be(50m);
        }

        [Fact]
        public async Task WithdrawMoneyFromCurrencyAccountAsync_ShouldThrowException_WhenInsufficientFunds()
        {
            var userId = _fixture.Create<Guid>();
            var amount = _fixture.Create<decimal>();
            var userDataModel = _fixture.Build<UserDataModel>().With(u => u.Balance, 0).Create();
            _unitOfWorkMock.Setup(u => u.BeginTransaction()).Returns(_transaction.Object);
            _userRepositoryMock.Setup(r => r.GetUserByIdAsync(userId)).ReturnsAsync(userDataModel);

            Func<Task> act = async () => await _userService.WithdrawMoneyFromCurrencyAccountAsync(userId, amount);

            await act.Should().ThrowAsync<Exception>().WithMessage("Insufficient funds");
        }
    }

}