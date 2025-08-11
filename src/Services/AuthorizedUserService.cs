using TrueSecProject.Models;
using TrueSecProject.Repositories;

namespace TrueSecProject.Services
{
    public class AuthorizedUserService : IAuthorizedUserService
    {
        private readonly IAuthorizedUserRepository _repository;

        public AuthorizedUserService(IAuthorizedUserRepository repository)
        {
            _repository = repository;
        }

        public async Task<AuthorizedUser?> GetAuthorizedUser(string username, string password)
        {
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                return null;
            }

            // Retrieve the user by username
            var user = (await _repository.GetByNameAsync(username))?.toModel();

            // Check if the user exists and the password matches
            if (user != null && user.Password == password)
            {
                return user;
            }

            return null; // User not found or password does not match
        }
    }
}