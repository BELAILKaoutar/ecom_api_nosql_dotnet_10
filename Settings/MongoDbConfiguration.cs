namespace ecom_api_nosql_.Settings;
/// <summary>
/// MongoDbConfiguration class is used to store the configuration settings for MongoDB connection, database and collections.
/// </summary>
public class MongoDbConfiguration
{
    /// gets or sets the connection string 
    /// </summary>
    public string? ConnectionString { get; set; }
    /// <summary>
    /// gets or sets the Database
    /// </summary>
    public string? Database { get; set; }
    /// <summary>
    /// gets or sets the Collections
    /// </summary>
    public IReadOnlyDictionary<string,string>? Collections { get; set; }
}