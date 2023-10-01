using KnowledgePeak_API.Core.Entities.Commons;

namespace KnowledgePeak_API.Core.Entities;

public class Faculty : BaseEntity
{
    public string Name { get; set; }
    public string ShortName { get; set; }
    public DateTime CreateTime { get; set; }
}
