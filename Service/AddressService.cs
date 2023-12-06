using Microsoft.Extensions.Options;
using MongoDB.Driver;
using AddressAPI.Data;
namespace AddressAPI.Service
{
    public class AddressesService
    {
        private readonly IMongoCollection<Address> _addresses;

        public AddressesService(IOptions<AddressesDatabaseSettings> options)
        {
            var mongoClient = new MongoClient(options.Value.ConnectionString);

            _addresses = mongoClient.GetDatabase(options.Value.DatabaseName)
                .GetCollection<Address>(options.Value.AddressesCollectionName);
        }

        public async Task<List<Address>> Get() =>
            await _addresses.Find(_ => true).ToListAsync();

        public async Task<Address> Get(string id) =>
            await _addresses.Find(a => a.Id == id).FirstOrDefaultAsync();

        public async Task Create(Address newAddress) =>
            await _addresses.InsertOneAsync(newAddress);

        public async Task Update(string id, Address updatedAddress) =>
            await _addresses.ReplaceOneAsync(a => a.Id == id, updatedAddress);

        public async Task Remove(string id) =>
            await _addresses.DeleteOneAsync(a => a.Id == id);
    }
}
