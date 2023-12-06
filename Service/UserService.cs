using AddressAPI.Data;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Threading.Tasks;
using AddressAPI.Data;
using AddressAPI.Service;
namespace AddressAPI.Service
{
    public class UserService
    {
        private readonly IMongoCollection<User> _users;
        private readonly AddressesService _AddressService;

        public UserService(IOptions<UsersDatabaseSettings> options, AddressesService AddressService)
        {
            var mongoClient = new MongoClient(options.Value.ConnectionString);
            _users = mongoClient.GetDatabase(options.Value.DatabaseName)
                .GetCollection<User>(options.Value.UsersCollectionName);

            _AddressService = AddressService;
        }

        public async Task<List<User>> GetUsers() =>
            await _users.Find(_ => true).ToListAsync();

        public async Task<User> GetUser(string userId) =>
            await _users.Find(u => u.Id == userId).FirstOrDefaultAsync();

        public async Task CreateUser(User newUser, string password)
        {
            newUser.Password = password;

            foreach (var Address in newUser.Addresses)
            {
                await _AddressService.Create(Address);
            }

            await _users.InsertOneAsync(newUser);
        }

        public async Task AddAddressToUser(string userId, Address newAddress)
        {
            var userFilter = Builders<User>.Filter.Eq(u => u.Id, userId);
            var update = Builders<User>.Update.Push(u => u.Addresses, newAddress);
            await _users.UpdateOneAsync(userFilter, update);

            await _AddressService.Create(newAddress);
        }

        public async Task UpdateUser(string userId, User updatedUser) =>
            await _users.ReplaceOneAsync(u => u.Id == userId, updatedUser);

        public async Task RemoveUser(string userId) =>
            await _users.DeleteOneAsync(u => u.Id == userId);

        public async Task<User> GetUserByEmail(string email) =>
           await _users.Find(u => u.Email == email).FirstOrDefaultAsync();

        public bool VerifyPassword(User user, string password)
        {
            return user.Password == password;
        }
    }
}
