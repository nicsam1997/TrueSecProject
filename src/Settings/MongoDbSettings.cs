namespace TrueSecProject.Settings
{
    public class MongoDbSettings
    {
        public string ConnectionString { get; set; } = null!;
        public string DatabaseName { get; set; } = null!;
        public string VulnerabilitiesCollectionName { get; set; } = null!;
        public string AuthorizedUsersCollectionName { get; set; } = null!;
    }
}