using KnowledgePeak_API.Core.Entities.Commons;

namespace KnowledgePeak_API.Core.Entities;

public class StudentHistory : BaseEntity
{
    public DateTime HistoryDate { get; set; }
    public Grade Grade { get; set; }
    public int GradeId { get; set; }
    public Student Student { get; set; }
    public string Studentid { get; set; }
}
