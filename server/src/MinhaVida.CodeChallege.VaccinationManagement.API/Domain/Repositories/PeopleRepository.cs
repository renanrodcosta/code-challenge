using MinhaVida.CodeChallege.VaccinationManagement.API.Domain.Entities;
using MongoDB.Driver;
using System.Threading.Tasks;

namespace MinhaVida.CodeChallege.VaccinationManagement.API.Domain.Repositories
{
    public class PeopleRepository
    {
        private IMongoCollection<Person> people;

        public PeopleRepository(MongoClient client)
        {
            var database = client.GetDatabase("mongodbdotnetcore");
            people = database.GetCollection<Person>(nameof(people));
        }

        public async Task AddAsync(Person person) =>
            await people.InsertOneAsync(person);

        public async Task UpdateAsync(Person person)
        {
            var x = await people.FindOneAndUpdateAsync(p => p.Id == person.Id, person);
        }

        public async Task DeleteAsync(string id) =>
            await people.FindOneAndDeleteAsync(p => p.Id == id);
    }
}