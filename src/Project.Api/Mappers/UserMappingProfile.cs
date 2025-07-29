using AutoMapper;
using Project.Application.Dtos.Users;
using Project.Domain.Entities;

namespace Project.Api.Mappers;

public class UserMappingProfile : Profile
{
    public UserMappingProfile()
    {
        CreateMap<UserDto, User>();
        CreateMap<User, UserGetDto>();
    }
}