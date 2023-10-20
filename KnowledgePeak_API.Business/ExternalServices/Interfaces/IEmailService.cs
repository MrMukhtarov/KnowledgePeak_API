using KnowledgePeak_API.Core.Entities;

namespace KnowledgePeak_API.Business.ExternalServices.Interfaces;

public interface IEmailService
{
    void SendEail(Message message);
}
