using AutoMapper;
using CustodialCryptoWallet.Dal.DataModels;
using CustodialCryptoWallet.Dal.Models;

namespace CustodialCryptoWallet.Dal.Infrastructure.DalMapperProfiles
{
    public class DalUserMapperProfile : Profile
    {
        public DalUserMapperProfile()
        {
            CreateMap<User, UserDataModel>().ReverseMap();
        }
    }
}
