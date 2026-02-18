using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace ecom_api_nosql_.Models
{
    public class Order
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }   // ✅ nullable (enlève le warning)

        [BsonElement("client_id")]
        public string CustomerId { get; set; } = string.Empty;

        [BsonElement("date_commande")]
        public DateTime DateCommande { get; set; } = DateTime.UtcNow;  // ✅ DateTime

        [BsonElement("statut")]
        public string Statut { get; set; } = string.Empty;

        [BsonElement("articles")]
        public List<string> Articles { get; set; } = new();            // ✅ List<string>

        [BsonElement("montant_total")]
        public decimal MontantTotal { get; set; } = 0m;                // ✅ decimal

        [BsonElement("adresse_livraison")]
        public string AdresseLivraison { get; set; } = string.Empty;
    }
}
