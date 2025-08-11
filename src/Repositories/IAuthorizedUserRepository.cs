using TrueSecProject.Models;

namespace TrueSecProject.Repositories;

public interface IAuthorizedUserRepository
{
    Task<AuthorizedUserEntity> GetByNameAsync(string name);
}