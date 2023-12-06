
using MongoDB.Bson.Serialization.Attributes;
using System.Collections.Generic;
using AddressAPI.Data;
namespace AddressAPI.Data
{
    public class User
    {
        [BsonId]
        [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
        public string Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public List<Address> Addresses { get; set; }
    }
}
