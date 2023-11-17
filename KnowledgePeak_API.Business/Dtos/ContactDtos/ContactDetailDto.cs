namespace KnowledgePeak_API.Business.Dtos.ContactDtos;

public record ContactDetailDto
{
    public int Id { get; set; }
    public string FullName { get; set; }
    public string Phone { get; set; }
    public string Email { get; set; }
    public string Message { get; set; }
    public DateTime CreateDate { get; set; }
}
