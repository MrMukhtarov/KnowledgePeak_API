using AutoMapper;
using KnowledgePeak_API.Business.Dtos.RoomDtos;
using KnowledgePeak_API.Business.Exceptions.Commons;
using KnowledgePeak_API.Business.Exceptions.Room;
using KnowledgePeak_API.Business.Services.Interfaces;
using KnowledgePeak_API.Core.Entities;
using KnowledgePeak_API.DAL.Repositories.Interfaces;

namespace KnowledgePeak_API.Business.Services.Implements;

public class RoomService : IRoomService
{
    readonly IRoomRepository _repo;
    readonly IMapper _mapper;
    readonly IFacultyRepository _faculty;

    public RoomService(IRoomRepository repo, IMapper mapper, IFacultyRepository faculty)
    {
        _repo = repo;
        _mapper = mapper;
        _faculty = faculty;
    }

    public async Task CreateAsync(RoomCreateDto dto)
    {
        if (await _repo.IsExistAsync(r => r.RoomNumber == dto.RoomNumber)) throw new RoomNameIsExistException();
        var faculty = await _faculty.GetSingleAsync(r => r.Id == dto.FacultyId && r.IsDeleted == false);
        if (faculty == null) throw new NotFoundException<Faculty>();
        var map = _mapper.Map<Room>(dto);
        map.FacultyId = dto.FacultyId;
        await _repo.CreateAsync(map);
        await _repo.SaveAsync();
    }

    public async Task DeleteAsync(int id)
    {
        if (id <= 0) throw new IdIsNegativeException<Room>();
        var room = await _repo.FIndByIdAsync(id);
        if (room == null) throw new NotFoundException<Room>();
        if (room.IsEmpty == false) throw new RoomNotEmptyException();
        await _repo.DeleteAsync(id);
        await _repo.SaveAsync();
    }

    public async Task<ICollection<RoomListItemDto>> GetAllAsync(bool takeAll)
    {
        if (takeAll)
        {
            var data = _repo.GetAll("Faculty");
            return _mapper.Map<ICollection<RoomListItemDto>>(data);
        }
        else
        {
            var data = _repo.FindAll(r => r.IsDeleted == false, "Faculty");
            return _mapper.Map<ICollection<RoomListItemDto>>(data);
        }
    }

    public async Task<RoomDetailItemDto> GetByIdAsync(int id, bool takeAll)
    {
        if (id <= 0) throw new IdIsNegativeException<Room>();
        if (takeAll)
        {
            var data = await _repo.GetSingleAsync(r => r.Id == id, "Faculty");
            if (data == null) throw new NotFoundException<Room>();
            return _mapper.Map<RoomDetailItemDto>(data);
        }
        else
        {
            var data = await _repo.GetSingleAsync(r => r.Id == id && r.IsDeleted == false, "Faculty");
            if (data == null) throw new NotFoundException<Room>();
            return _mapper.Map<RoomDetailItemDto>(data);
        }
    }

    public async Task RevertSoftDeleteAsync(int id)
    {
        if (id <= 0) throw new IdIsNegativeException<Room>();
        var room = await _repo.FIndByIdAsync(id);
        if (room == null) throw new NotFoundException<Room>();
        room.IsDeleted = false;
        await _repo.SaveAsync();
    }

    public async Task SoftDeleteAsync(int id)
    {
        if (id <= 0) throw new IdIsNegativeException<Room>();
        var room = await _repo.FIndByIdAsync(id);
        if (room == null) throw new NotFoundException<Room>();
        if (room.IsEmpty == false) throw new RoomNotEmptyException();
        room.IsDeleted = true;
        await _repo.SaveAsync();
    }

    public async Task UpdateAsync(int id, RoomUpdateDto dto)
    {
        if (id <= 0) throw new IdIsNegativeException<Room>();
        var room = await _repo.GetSingleAsync(r => r.Id == id, "Faculty");
        if (room == null) throw new NotFoundException<Room>();

        var exist = await _repo.IsExistAsync(r => r.RoomNumber == dto.RoomNumber && r.Id != id);
        if (exist) throw new RoomNameIsExistException();

        if (dto.FacultyId != null)
        {
            var faculty = await _faculty.GetSingleAsync(f => f.Id == dto.FacultyId && f.IsDeleted == false);
            if (faculty == null) throw new NotFoundException<Faculty>();
            room.FacultyId = faculty.Id;
        }
        _mapper.Map(dto, room);
        await _repo.SaveAsync();
    }
}
