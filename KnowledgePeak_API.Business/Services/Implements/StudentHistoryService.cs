using AutoMapper;
using KnowledgePeak_API.Business.Dtos.StudentHistoryDtos;
using KnowledgePeak_API.Business.Exceptions.Commons;
using KnowledgePeak_API.Business.Services.Interfaces;
using KnowledgePeak_API.Core.Entities;
using KnowledgePeak_API.DAL.Repositories.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace KnowledgePeak_API.Business.Services.Implements;

public class StudentHistoryService : IStudentHistoryService
{
    readonly IStudentHistoryRepository _repo;
    readonly IMapper _mapper;
    readonly UserManager<Student> _userManager;
    readonly IGradeRepository _gradeRepository;

    public StudentHistoryService(IStudentHistoryRepository repo, IMapper mapper, UserManager<Student> userManager,
        IGradeRepository gradeRepository)
    {
        _repo = repo;
        _mapper = mapper;
        _userManager = userManager;
        _gradeRepository = gradeRepository;
    }

    public async Task CreateAsync(StudentHistoryCreateDto dto)
    {
        //if (dto.GradeId <= 0) throw new IdIsNegativeException<Grade>();
        //var grade = await _gradeRepository.FIndByIdAsync(dto.GradeId);
        //if (grade == null) throw new NotFoundException<Grade>();

        if (string.IsNullOrEmpty(dto.Studentid)) throw new ArgumentNullException();
        var stu = await _userManager.FindByIdAsync(dto.Studentid);
        if (stu == null) throw new UserNotFoundException<Student>();

        var map = _mapper.Map<StudentHistory>(dto);
        await _repo.CreateAsync(map);
        await _repo.SaveAsync();
    }

    public async Task<ICollection<StudentHistoryListItemDto>> GetAllAsync()
    {
        var data = _repo.GetAll("Grade");
        return _mapper.Map<ICollection<StudentHistoryListItemDto>>(data);
    }

    public async Task<StudentHistoryDetailDto> GetByIdAsync(int id)
    {
        if (id <= 0) throw new IdIsNegativeException<StudentHistory>();
        var history = await _repo.FIndByIdAsync(id, "Grade", "Student");
        if (history == null) throw new NotFoundException<StudentHistory>();
        return _mapper.Map<StudentHistoryDetailDto>(history);
    }
}
