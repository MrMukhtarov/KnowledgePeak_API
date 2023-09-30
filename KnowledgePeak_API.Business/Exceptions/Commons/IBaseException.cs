namespace KnowledgePeak_API.Business.Exceptions.Commons;

public interface IBaseException
{
    public int StatusCode { get; }
    public string ErrorMessage { get; }
}
