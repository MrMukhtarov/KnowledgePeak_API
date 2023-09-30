using KnowledgePeak_API.Core.Entities.Commons;

namespace KnowledgePeak_API.Core.Entities;

public class University : BaseEntity
{
    public string Name { get; set; }
    public string Description { get; set; }
    //public int DirectorId { get; set; }
}
