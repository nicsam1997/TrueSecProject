using TrueSecProject.Models;

namespace TrueSecProject.Services
{
    public interface IAuthorizedUserService
    {
        Task<AuthorizedUser?> GetAuthorizedUser(string username, string password);
    }
}