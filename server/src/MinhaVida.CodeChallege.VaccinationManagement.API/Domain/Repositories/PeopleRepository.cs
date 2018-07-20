using MinhaVida.CodeChallege.VaccinationManagement.API.Domain.Entities;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MinhaVida.CodeChallege.VaccinationManagement.API.Domain.Repositories
{
    public class PeopleRepository
    {
        private readonly IMongoCollection<Person> people;

        public PeopleRepository(MongoClient client)
        {
            var opts = new MongoDatabaseSettings { WriteConcern = WriteConcern.Acknowledged };
            var database = client.GetDatabase("db", opts);            
            people = database.GetCollection<Person>("people");
        }

        public async Task<IEnumerable<Person>> GetAsync()
        {
            var allPeopleCursor = await people.FindAsync(FilterDefinition<Person>.Empty);
            var allPeople = await allPeopleCursor.ToListAsync();
            return allPeople;
        }

        public async Task<Person> GetAsync(string id)
        {
            var personCursor = 
                await people.FindAsync(Builders<Person>.Filter.Eq(p => p.Id, id));
            var person = await personCursor.FirstOrDefaultAsync();
            return person;
        }

        public async Task AddAsync(Person person) =>
            await people.InsertOneAsync(person);

        public async Task<Person> UpdateAsync(Person person) =>       
            await people.FindOneAndReplaceAsync(p => p.Id == person.Id, person);
        
        public async Task<Person> DeleteAsync(string id) =>
            await people.FindOneAndDeleteAsync(p => p.Id == id);
    }
}