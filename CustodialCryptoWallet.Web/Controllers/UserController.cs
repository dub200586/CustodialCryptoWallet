using AutoMapper;
using CustodialCryptoWallet.Bll.Models;
using CustodialCryptoWallet.Bll.Services.Interfaces;
using CustodialCryptoWallet.Web.Models.Dto;
using CustodialCryptoWallet.Web.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace CustodialCryptoWallet.Web.Controllers
{
    [ApiController]
    [Route("api/users")]
    public class UserController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly ILogger<UserController> _logger;
        private readonly IUserService _userService;

        public UserController(IMapper mapper, ILogger<UserController> logger, IUserService userService)
        {
            _mapper = mapper;
            _logger = logger;
            _userService = userService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateUserAsync(UserDto user)
        {
            try
            {
                var userModel = _mapper.Map<UserModel>(user);
                var createdUserModel = await _userService.CreateUserAsync(userModel);
                var createdUserViewModel = _mapper.Map<UserViewModel>(createdUserModel);

                return Ok(createdUserViewModel);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return BadRequest(ex);
            }
        }

        [HttpGet, Route("{userId}/balance")]
        public async Task<IActionResult> GetBalanceByUserIdAsync(Guid userId)
        {
            try
            {
                var userBalance = await _userService.GetUserByIdAsync(userId);
                var userBalanceViewModel = _mapper.Map<BalanceViewModel>(userBalance);

                return Ok(userBalanceViewModel);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return BadRequest(ex);
            }
        }

        [HttpPost, Route("{userId}/deposit")]
        public async Task<IActionResult> DepositMoneyToCurrencyAccountAsync(Guid userId, decimal amount)
        {
            try
            {
                var updatedBalance = _userService.DepositMoneyToCurrencyAccountAsync(userId, amount);
                var updatedBalanceViewModel = _mapper.Map<NewBalanceViewModel>(updatedBalance);

                return Ok(updatedBalanceViewModel);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return BadRequest(ex);
            }
        }

        [HttpPost, Route("{userId}/withdraw")]
        public async Task<IActionResult> WithdrawMoneyFromCurrencyAccountAsync(Guid userId, decimal amount)
        {
            try
            {
                var updatedBalance = _userService.WithdrawMoneyFromCurrencyAccountAsync(userId, amount);
                var updatedBalanceViewModel = _mapper.Map<NewBalanceViewModel>(updatedBalance);

                return Ok(updatedBalanceViewModel);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return BadRequest(ex);
            }
        }
    }
}
