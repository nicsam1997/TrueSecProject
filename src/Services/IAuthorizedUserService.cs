using TrueSecProject.Models;

namespace TrueSecProject.Services
{
    public interface IAuthorizedUserService
    {
        /// <summary>
        /// Retrieves an authorized user by their username.
        /// </summary>
        /// <param name="username">The username of the user trying to authenticate.</param>
        /// <param name="password">The password of the user trying to authenticate.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the authorized user if found; otherwise, null.</returns>

        Task<AuthorizedUser?> GetAuthorizedUser(string username, string password);
    }
}