using KnowledgePeak_API.Core.Entities.Commons;

namespace KnowledgePeak_API.Core.Entities;

public class Setting : BaseEntity
{
    public string Email { get; set; }
    public string Phone { get; set; }
    public string Location { get; set; }
    public string HeaderLogo { get; set; }
    public string FooterLogo { get; set; }
}
