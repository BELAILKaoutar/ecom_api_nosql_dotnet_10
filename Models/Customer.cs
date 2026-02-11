using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace ecom_api_nosql_.Models
{
    public enum CustomerStatus
    {
        Standard,
        Premium
    }

    public class Customer
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }

        [BsonElement("nom")]
        public string Nom { get; set; } = string.Empty;

        [BsonElement("prenom")]
        public string Prenom { get; set; } = string.Empty;

        [BsonElement("email")]
        public string Email { get; set; } = string.Empty;

        [BsonElement("telephone")]
        public string Telephone { get; set; } = string.Empty;

        [BsonElement("adresse")]
        public string Adresse { get; set; } = string.Empty; 

        [BsonElement("date_inscription")]
        public DateTime DateCreation { get; set; } = DateTime.UtcNow;

        [BsonElement("statut")]
        public CustomerStatus Statut { get; set; } = CustomerStatus.Standard;

        [BsonIgnore]
        public string NomComplet => $"{Nom} {Prenom}";
    }
}
