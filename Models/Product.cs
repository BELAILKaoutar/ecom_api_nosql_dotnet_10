using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace ecom_api_nosql_.Models;

/// <summary>
/// Product entity class representing a product in the MongoDB collection
/// </summary>
///    
[BsonIgnoreExtraElements] // ← ça ignore les champs supplémentaires

public class Product
{
    /// <summary>
    /// Gets or sets the unique identifier for the product
    /// </summary>
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? Id { get; set; }

    /// <summary>
    /// Gets or sets the product name
    /// </summary>
    [BsonElement("nom")]
    public string Nom { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the product category
    /// </summary>
    [BsonElement("categorie")]
    public string Categorie { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the product price
    /// </summary>
    [BsonElement("prix")]
    public decimal Prix { get; set; }

    /// <summary>
    /// Gets or sets the stock quantity
    /// </summary>
    [BsonElement("stock")]
    public int Stock { get; set; }

    /// <summary>
    /// Gets or sets the product specifications
    /// </summary>
    [BsonElement("specifications")]
    public ProductSpecifications? Specifications { get; set; }

    /// <summary>
    /// Gets or sets the product tags
    /// </summary>
    [BsonElement("tags")]
    public List<string> Tags { get; set; } = new();

    /// <summary>
    /// Gets or sets the date the product was added
    /// </summary>
    [BsonElement("date_ajout")]
    public DateTime DateAjout { get; set; }


}

/// <summary>
/// Product specifications class
/// </summary>
[BsonIgnoreExtraElements]   
public class ProductSpecifications
{
    /// <summary>
    /// Gets or sets the processor
    /// </summary>
    [BsonElement("processeur")]
    public string? Processeur { get; set; }

    /// <summary>
    /// Gets or sets the RAM
    /// </summary>
    [BsonElement("ram")]
    public string? Ram { get; set; }

    /// <summary>
    /// Gets or sets the storage
    /// </summary>
    [BsonElement("stockage")]
    public string? Stockage { get; set; }


}
