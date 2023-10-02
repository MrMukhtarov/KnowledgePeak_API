using AutoMapper;
using KnowledgePeak_API.Business.Dtos.GroupDtos;
using KnowledgePeak_API.Business.Exceptions.Commons;
using KnowledgePeak_API.Business.Exceptions.Group;
using KnowledgePeak_API.Business.Services.Interfaces;
using KnowledgePeak_API.Core.Entities;
using KnowledgePeak_API.DAL.Repositories.Interfaces;

namespace KnowledgePeak_API.Business.Services.Implements;

public class GroupService : IGroupService
{
    readonly IGroupRepository _repo;
    readonly IMapper _mapper;
    readonly ISpecialityRepository _specialityRepo;

    public GroupService(IGroupRepository repo, IMapper mapper,
        ISpecialityRepository specialityRepo)
    {
        _repo = repo;
        _mapper = mapper;
        _specialityRepo = specialityRepo;
    }

    public async Task CreateAsync(GroupCreateDto dto)
    {
        var exist = await _repo.IsExistAsync(g => g.Name == dto.Name);
        if (exist) throw new GroupNameIsExistException();

        var map = _mapper.Map<Group>(dto);
        await _repo.CreateAsync(map);
        await _repo.SaveAsync();
    }

    public async Task DeleteAsync(int id)
    {
        if (id <= 0) throw new IdIsNegativeException<Group>();
        var entity = await _repo.FIndByIdAsync(id);
        if (entity == null) throw new NotFoundException<Group>();

        await _repo.DeleteAsync(id);
        await _repo.SaveAsync();
    }

    public async Task<IEnumerable<GroupListItemDto>> GetAllAsync(bool takeAll)
    {
        if (takeAll)
        {
            var entity = _repo.GetAll();
            return _mapper.Map<IEnumerable<GroupListItemDto>>(entity);
        }
        else
        {
            var entity = _repo.FindAll(g => g.IsDeleted == false);
            return _mapper.Map<IEnumerable<GroupListItemDto>>(entity);
        }
    }

    public async Task<GroupDetailDto> GetByIdAsync(int id, bool takeAll)
    {
        if (id <= 0) throw new IdIsNegativeException<Group>();
        if (!takeAll)
        {
            var entity = await _repo.FIndByIdAsync(id);
            if (entity == null) throw new NotFoundException<Group>();
            return _mapper.Map<GroupDetailDto>(entity);
        }
        else
        {
            var entity = await _repo.GetSingleAsync(g => g.Id == id && g.IsDeleted == false);
            if (entity == null) throw new NotFoundException<Group>();
            return _mapper.Map<GroupDetailDto>(entity);
        }
    }

    public async Task GroupAddSpecialityAsync(int id, GroupAddSpecialityDto dto)
    {
        if (id <= 0) throw new IdIsNegativeException<Group>();
        var entity = await _repo.FIndByIdAsync(id);
        if (entity == null) throw new NotFoundException<Group>();

        var checkSpecialityId = await _specialityRepo.FIndByIdAsync(dto.SpecialityId);
        if (checkSpecialityId == null) throw new NotFoundException<Speciality>();

        entity.SpecialityId = dto.SpecialityId;
        await _repo.SaveAsync();
    }

    public async Task RevertSoftDeleteAsync(int id)
    {
        if (id <= 0) throw new IdIsNegativeException<Group>();
        var entity = await _repo.FIndByIdAsync(id);
        if (entity == null) throw new NotFoundException<Group>();

        _repo.RevertSoftDelete(entity);
        await _repo.SaveAsync();
    }

    public async Task SoftDeleteAsync(int id)
    {
        if (id <= 0) throw new IdIsNegativeException<Group>();
        var entity = await _repo.FIndByIdAsync(id);
        if (entity == null) throw new NotFoundException<Group>();

        _repo.SoftDelete(entity);
        await _repo.SaveAsync();
    }

    public async Task UpdateAsync(int id, GroupUpdateDto dto)
    {
        if (id <= 0) throw new IdIsNegativeException<Group>();
        var entity = await _repo.FIndByIdAsync(id);
        if (entity == null) throw new NotFoundException<Group>();

        var checkSpecialityId = await _specialityRepo.FIndByIdAsync(dto.SpecialityId);
        if (checkSpecialityId == null) throw new NotFoundException<Speciality>();

        _mapper.Map(dto, entity);
        await _repo.SaveAsync();
    }
}
