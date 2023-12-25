using AutoMapper;
using BootcampResult.Api.Models.Dtos;
using BootcampResult.Domain.Entities;

namespace BootcampResult.Api.Mapper;

public class UserMapper : Profile
{
    public UserMapper()
    {
        CreateMap<User, UserDto>().ReverseMap();
    }
}