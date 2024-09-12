

namespace OARCapiRestTools.Services
{
    public interface IEmailService
    {
        public Task<ServiceResponse<string>> SendEmailFromOARCDeveloperGmailCom(Email email);
    }
}
