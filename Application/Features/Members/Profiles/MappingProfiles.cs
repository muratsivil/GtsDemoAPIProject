using Application.Features.Members.Commands.Create;
using Application.Features.Members.Commands.Update;
using Application.Features.Members.Queries.GetById;
using AutoMapper;
using Domain.Entities;

namespace Application;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<Member, CreatedMemberResponse>().ReverseMap();
        CreateMap<Member, CreateMemberCommand>().ReverseMap();
        
        CreateMap<Member, UpdateMemberCommand>().ReverseMap();
        CreateMap<Member, UpdatedMemberResponse>().ReverseMap();

        CreateMap<Member, GetByIdMemberResponse>().ReverseMap();
        CreateMap<Member, GetByIdMemberQuery>().ReverseMap();
    }

}