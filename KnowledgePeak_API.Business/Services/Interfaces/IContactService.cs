using KnowledgePeak_API.Business.Dtos.ContactDtos;

namespace KnowledgePeak_API.Business.Services.Interfaces;

public interface IContactService
{
    Task CreateAsync(ContactCreateDto dto);
    Task<ICollection<ContactListItemDto>> GetAllAsync();
    Task<ContactDetailDto> GetByIdAsync(int id);
    Task<int> Count();
}
