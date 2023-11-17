using AutoMapper;
using KnowledgePeak_API.Business.Dtos.ContactDtos;
using KnowledgePeak_API.Core.Entities;

namespace KnowledgePeak_API.Business.Profiles;

public class ContactMappingProfile : Profile
{
    public ContactMappingProfile()
    {
        CreateMap<ContactCreateDto, Contact>();
        CreateMap<Contact, ContactListItemDto>();
        CreateMap<Contact, ContactDetailDto>();
    }
}
