using AutoMapper;

namespace itabank.Core.Mapping.Profiles;

public class SDKToDomain : Profile
{
    public SDKToDomain()
    {
        CreateMap<SDK.Enums.TransactionType, Domain.Enums.TransactionType>();

        CreateMap<SDK.Models.Account, Domain.Models.Account>();
        CreateMap<SDK.Models.Transaction, Domain.Models.Transaction>();
    }
}