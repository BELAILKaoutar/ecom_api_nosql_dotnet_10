using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace ecom_api_nosql_.Models
{
    public class Order
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement("client_id")]
        public string CustomerId { get; set; }

        [BsonElement("date_commande")]
        public DateTime DateCommande { get; set; }

        [BsonElement("statut")]
        public string Statut { get; set; }

        [BsonElement("articles")]
        public List<string> Articles { get; set; }

        [BsonElement("montant_total")]
        public decimal MontantTotal { get; set; }

        [BsonElement("adresse_livraison")]
        public string AdresseLivraison { get; set; }
    }
}
