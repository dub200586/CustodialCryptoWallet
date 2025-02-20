using AutoMapper;
using CustodialCryptoWallet.Bll.Models;
using CustodialCryptoWallet.Dal.DataModels;

namespace CustodialCryptoWallet.Bll.Infrastructure.BllMapperProfiles
{
    public class BllUserMapperProfile : Profile
    {
        public BllUserMapperProfile()
        {
            CreateMap<UserModel, UserDataModel>().ReverseMap();
        }
    }
}
