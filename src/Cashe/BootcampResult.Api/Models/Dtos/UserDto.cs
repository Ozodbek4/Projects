using AutoMapper;

namespace BootcampResult.Api.Models.Dtos;

public class UserDto
{
    public Guid Id { get; set; }
    
    public string FirstName { get; set; } = default!;

    public string LastName { get; set; } = default!;
    
    public byte Age { get; set; } 

    public string EmailAddress { get; set; } = default!;
}