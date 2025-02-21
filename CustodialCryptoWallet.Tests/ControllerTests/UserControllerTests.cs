using AutoFixture;
using AutoMapper;
using FluentAssertions;
using CustodialCryptoWallet.Bll.Models;
using CustodialCryptoWallet.Bll.Services.Interfaces;
using CustodialCryptoWallet.Web.Controllers;
using CustodialCryptoWallet.Web.Models.Dto;
using CustodialCryptoWallet.Web.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace CustodialCryptoWallet.Tests.ControllerTests
{
    public class UserControllerTests
    {
        private readonly Mock<IUserService> _userServiceMock;
        private readonly Mock<IMapper> _mapperMock;
        private readonly UserController _controller;
        private readonly Fixture _fixture;

        public UserControllerTests()
        {
            _userServiceMock = new Mock<IUserService>();
            _mapperMock = new Mock<IMapper>();
            _controller = new UserController(_mapperMock.Object, _userServiceMock.Object);
            _fixture = new Fixture();
        }

        [Fact]
        public async Task CreateUserAsync_ShouldReturnOk_WhenUserCreated()
        {
            var userDto = _fixture.Create<UserDto>();
            var userModel = _fixture.Create<UserModel>();
            var createdUserModel = _fixture.Create<UserModel>();
            var createdUserViewModel = _fixture.Create<UserViewModel>();

            _mapperMock.Setup(m => m.Map<UserModel>(userDto)).Returns(userModel);
            _userServiceMock.Setup(s => s.CreateUserAsync(userModel)).ReturnsAsync(createdUserModel);
            _mapperMock.Setup(m => m.Map<UserViewModel>(createdUserModel)).Returns(createdUserViewModel);

            var result = await _controller.CreateUserAsync(userDto);

            result.Should().BeOfType<OkObjectResult>().Which.Value.Should().BeEquivalentTo(createdUserViewModel);
        }

        [Fact]
        public async Task GetBalanceByUserIdAsync_ShouldReturnOk_WhenUserExists()
        {
            var userId = _fixture.Create<Guid>();
            var userModel = _fixture.Create<UserModel>();
            var balanceViewModel = _fixture.Create<BalanceViewModel>();

            _userServiceMock.Setup(s => s.GetUserByIdAsync(userId)).ReturnsAsync(userModel);
            _mapperMock.Setup(m => m.Map<BalanceViewModel>(userModel)).Returns(balanceViewModel);

            var result = await _controller.GetBalanceByUserIdAsync(userId);

            result.Should().BeOfType<OkObjectResult>().Which.Value.Should().BeEquivalentTo(balanceViewModel);
        }

        [Fact]
        public async Task DepositMoneyToCurrencyAccountAsync_ShouldReturnOk_WhenDepositSucceeds()
        {
            var userId = _fixture.Create<Guid>();
            var amountDto = _fixture.Create<AmountDto>();
            var updatedBalanceModel = _fixture.Create<NewBalanceViewModel>();
            var userModel = _fixture.Create<UserModel>();

            _userServiceMock.Setup(s => s.DepositMoneyToCurrencyAccountAsync(userId, amountDto.Amount))
                .ReturnsAsync(userModel);
            _mapperMock.Setup(m => m.Map<NewBalanceViewModel>(userModel)).Returns(updatedBalanceModel);

            var result = await _controller.DepositMoneyToCurrencyAccountAsync(userId, amountDto);

            result.Should().BeOfType<OkObjectResult>().Which.Value.Should().BeEquivalentTo(updatedBalanceModel);
        }

        [Fact]
        public async Task WithdrawMoneyFromCurrencyAccountAsync_ShouldReturnOk_WhenWithdrawalSucceeds()
        {
            var userId = _fixture.Create<Guid>();
            var amountDto = _fixture.Create<AmountDto>();
            var updatedBalanceModel = _fixture.Create<NewBalanceViewModel>();
            var userModel = _fixture.Create<UserModel>();

            _userServiceMock.Setup(s => s.WithdrawMoneyFromCurrencyAccountAsync(userId, amountDto.Amount))
                .ReturnsAsync(userModel);
            _mapperMock.Setup(m => m.Map<NewBalanceViewModel>(userModel)).Returns(updatedBalanceModel);

            var result = await _controller.WithdrawMoneyFromCurrencyAccountAsync(userId, amountDto);

            result.Should().BeOfType<OkObjectResult>().Which.Value.Should().BeEquivalentTo(updatedBalanceModel);
        }
    }
}