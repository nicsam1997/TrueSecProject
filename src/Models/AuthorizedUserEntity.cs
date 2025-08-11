using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace TrueSecProject.Models
{
    public class AuthorizedUserEntity
    {
        [BsonId]
        [BsonElement("username")]
        public string Username { get; set; } = null!;

        [BsonElement("password")]
        public string Password { get; set; } = null!;

        [BsonElement("role")]
        public string Role { get; set; } = null!;

        public AuthorizedUser toModel()
        {
            return new AuthorizedUser
            {
                Username = Username,
                Password = Password,
                Role = Role
            };
        }
    }
}