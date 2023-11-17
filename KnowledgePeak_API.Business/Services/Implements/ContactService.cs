using AutoMapper;
using KnowledgePeak_API.Business.Dtos.ContactDtos;
using KnowledgePeak_API.Business.Exceptions.Commons;
using KnowledgePeak_API.Business.Services.Interfaces;
using KnowledgePeak_API.Core.Entities;
using KnowledgePeak_API.DAL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace KnowledgePeak_API.Business.Services.Implements;

public class ContactService : IContactService
{
    readonly IMapper _mapper;
    readonly IContactRepository _repo;

    public ContactService(IMapper mapper, IContactRepository repo)
    {
        _mapper = mapper;
        _repo = repo;
    }

    public async Task CreateAsync(ContactCreateDto dto)
    {
        var map = _mapper.Map<Contact>(dto);
        await _repo.CreateAsync(map);
        await _repo.SaveAsync();
    }

    public async Task<ICollection<ContactListItemDto>> GetAllAsync()
    {
        var data = await _repo.GetAll().ToListAsync();
        foreach (var item in data)
        {
            item.IsRead = true;
        }
        await _repo.SaveAsync();
        var map = _mapper.Map<ICollection<ContactListItemDto>>(data);
        return map;
    }

    public async Task<ContactDetailDto> GetByIdAsync(int id)
    {
        if (id <= 0) throw new IdIsNegativeException<Contact>();
        var contact = await _repo.FIndByIdAsync(id);
        if (contact == null) throw new NotFoundException<Contact>();
        return _mapper.Map<ContactDetailDto>(contact);
    }

    public async Task<int> Count()
    {
        var data = await _repo.GetAll().ToListAsync();
        return data.Count();
    }

    public async Task<ICollection<ContactListItemDto>> GetAllAsyncForNotification()
    {
        return _mapper.Map<ICollection<ContactListItemDto>>(await _repo.GetAll().ToListAsync());
    }
}
