using AutoMapper;
using MeuGuia.Application.Commands.Revenue.Create;
using MeuGuia.Application.Commands.Revenue.Delete;
using MeuGuia.Application.Commands.Revenue.Update;
using MeuGuia.Application.Commands.User.Create;
using MeuGuia.Application.Queries.Revenue.GetAll;
using MeuGuia.Application.Queries.Revenue.GetById;
using MeuGuia.Domain.Entitie;

namespace MeuGuia.Application.Mapping;

public class AutoMapperConfiguration : Profile
{
    public AutoMapperConfiguration()
    {
        #region Account

        CreateMap<IdentityUserCustom, CreateUserCommandRequest>().ReverseMap();

        #endregion

        #region Revenue

        CreateMap<Revenue, CreateRevenueCommandRequest>().ReverseMap();
        CreateMap<Revenue, UpdateRevenueCommandRequest>().ReverseMap();
        CreateMap<Revenue, DeleteRevenueCommandRequest>().ReverseMap();

        CreateMap<Revenue, QueryRevenueGetAllResponseItems>().ReverseMap();
        CreateMap<Revenue, QueryRevenueGetByIdResponseItem>().ReverseMap();

        #endregion
    }
}
