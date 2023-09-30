namespace KnowledgePeak_API.Business.Dtos.SettingDtos;

public record SettingDetailDto
{
    public int Id { get; set; }
    public string Email { get; set; }
    public string Phone { get; set; }
    public string Location { get; set; }
    public string HeaderLogo { get; set; }
    public string FooterLogo { get; set; }
}
