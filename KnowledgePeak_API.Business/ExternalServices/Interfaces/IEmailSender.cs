namespace KnowledgePeak_API.Business.ExternalServices.Interfaces;

public interface IEmailSender
{
    Task SendEmailAsync(string fromAddress,  string toAddress, string subject, string message);
}
