namespace Content.API.Settings
{
    public class ContentDatabaseSettings : IContentDatabaseSettings
    {
        public string CollectionName { get; set; }
        public string CollectionAuditName { get; set; }
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
    }
}

