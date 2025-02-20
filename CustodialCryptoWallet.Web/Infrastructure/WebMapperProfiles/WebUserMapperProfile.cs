using AutoMapper;
using CustodialCryptoWallet.Bll.Models;
using CustodialCryptoWallet.Web.Models.Dto;
using CustodialCryptoWallet.Web.Models.ViewModels;

namespace CustodialCryptoWallet.Web.Infrastructure.WebMapperProfiles
{
    public class WebUserMapperProfile : Profile
    {
        public WebUserMapperProfile()
        {
            CreateMap<UserDto, UserModel>();
            CreateMap<UserModel, UserViewModel>();
            CreateMap<UserModel, BalanceViewModel>();
            CreateMap<UserModel, NewBalanceViewModel>().ForMember(b => b.NewBalance, opt =>
                opt.MapFrom(u => u.Balance));
        }
    }
}
