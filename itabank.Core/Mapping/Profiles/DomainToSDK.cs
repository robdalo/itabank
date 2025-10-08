using AutoMapper;

namespace itabank.Core.Mapping.Profiles;

public class DomainToSDK : Profile
{
    public DomainToSDK()
    {
        CreateMap<Domain.Enums.TransactionType, SDK.Enums.TransactionType>();

        CreateMap<Domain.Models.Account, SDK.Models.Account>();
        CreateMap<Domain.Models.Transaction, SDK.Models.Transaction>();
    }
}
