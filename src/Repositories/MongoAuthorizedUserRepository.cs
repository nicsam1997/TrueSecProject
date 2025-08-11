using Microsoft.Extensions.Options;
using MongoDB.Driver;
using TrueSecProject.Models;
using TrueSecProject.Settings;

namespace TrueSecProject.Repositories
{
    /// <summary>
    /// A repository for managing authorized users in a MongoDB database.
    /// </summary>
    public class MongoAuthorizedUserRepository : IAuthorizedUserRepository
    {
        private readonly IMongoCollection<AuthorizedUserEntity> _authorizedUsers;

        public MongoAuthorizedUserRepository(IMongoClient mongoClient, IOptions<MongoDbSettings> mongoDbSettings)
        {
            var database = mongoClient.GetDatabase(mongoDbSettings.Value.DatabaseName);
            _authorizedUsers = database.GetCollection<AuthorizedUserEntity>(mongoDbSettings.Value.AuthorizedUsersCollectionName);
        }

        public async Task<AuthorizedUserEntity> GetByNameAsync(string name)
        {
            return await _authorizedUsers.Find(user => user.Username == name).FirstOrDefaultAsync();
        }
    }
}