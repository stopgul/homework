namespace Content.API.Settings
{
    public interface IContentDatabaseSettings
    {
        string CollectionName { get; set; }
        string CollectionAuditName { get; set; }
        string ConnectionString { get; set; }
        string DatabaseName { get; set; }
    }
}