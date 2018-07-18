using Microsoft.AspNetCore.Mvc;
using MinhaVida.CodeChallege.VaccinationManagement.API.Domain.Commands.People;
using MinhaVida.CodeChallege.VaccinationManagement.API.Domain.Entities;
using MinhaVida.CodeChallege.VaccinationManagement.API.Domain.Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MinhaVida.CodeChallege.VaccinationManagement.API.Controllers
{
    [Route("api/v1/[controller]")]
    public class PeopleController : Controller
    {
        private readonly PeopleRepository peopleRepository;
        private readonly BlobRepository blobRepository;

        public PeopleController(PeopleRepository peopleRepository,
                                BlobRepository blobRepository)
        {
            this.peopleRepository = peopleRepository;
            this.blobRepository = blobRepository;
        }

        [HttpGet]
        public async Task<IEnumerable<Person>> Get()
        {
            var people = await peopleRepository.GetAsync();
            return people;
        }

        [HttpGet("{id}")]
        public async Task<Person> Get(string id)
        {
            Person person = await peopleRepository.GetAsync(id);
            return person;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody]PeopleCommand command)
        {
            string defaultUrlImage = "";

            var person = new Person(command);
            person.ChangePhoto(defaultUrlImage);
            await peopleRepository.AddAsync(person);

            var location = $"{Request.Path}/api/v1/people/{person.Id}";
            return Created(location, person);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(string id, [FromBody]PeopleCommand command)
        {
            Person person = await peopleRepository.GetAsync(id);
            if (person == null)
                return new NotFoundResult();

            person.Apply(command);
            await peopleRepository.UpdateAsync(person);

            return new OkObjectResult(person);
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> Patch(string id, [FromBody]ChangePhotoCommand command)
        {
            Person person = await peopleRepository.GetAsync(id);
            if (person == null)
                return new NotFoundResult();

            string urlPhoto = await blobRepository.UploadAsync("", "");
            person.ChangePhoto(urlPhoto);

            await peopleRepository.UpdateAsync(person);

            return new OkObjectResult(person);
        }

        [HttpDelete("{id}")]
        public async Task Delete(string id)
        {
            await peopleRepository.DeleteAsync(id);
        }

        [HttpPost("{id}/vaccines")]
        public async Task<IActionResult> Post(string id, [FromBody]AddVaccineCommand command)
        {
            Person person = await peopleRepository.GetAsync(id);
            if (person == null)
                return new NotFoundResult();

            person.Apply(command);

            await peopleRepository.UpdateAsync(person);

            return new OkObjectResult(person);
        }

        [HttpPut("{id}/vaccines")]
        public async Task<IActionResult> Put(string id, [FromBody]UpdateVaccineCommand command)
        {
            Person person = await peopleRepository.GetAsync(id);
            if (person == null)
                return new NotFoundResult();

            person.Apply(command);

            await peopleRepository.UpdateAsync(person);

            return new OkObjectResult(person);
        }

        [HttpDelete("{id}/vaccines/{vaccineId}")]
        public async Task<IActionResult> DeleteVaccine(string id, string vaccineId)
        {
            Person person = await peopleRepository.GetAsync(id);
            if (person == null)
                return new NotFoundResult();

            person.RemoveVaccine(vaccineId);

            await peopleRepository.UpdateAsync(person);

            return new OkObjectResult(person);
        }
    }
}