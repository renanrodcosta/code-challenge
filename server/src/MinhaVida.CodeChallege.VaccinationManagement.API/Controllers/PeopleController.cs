using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using MinhaVida.CodeChallege.VaccinationManagement.API.Attributes;
using MinhaVida.CodeChallege.VaccinationManagement.API.Domain.Commands.People;
using MinhaVida.CodeChallege.VaccinationManagement.API.Domain.Entities;
using MinhaVida.CodeChallege.VaccinationManagement.API.Domain.Repositories;
using MinhaVida.CodeChallege.VaccinationManagement.API.Infrastructure;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MinhaVida.CodeChallege.VaccinationManagement.API.Controllers
{
    [Route("api/v1/[controller]")]
    public class PeopleController : Controller
    {
        private readonly PeopleRepository peopleRepository;
        private readonly BlobRepository blobRepository;
        private readonly IOptions<PeopleOptions> peopleOpts;

        public PeopleController(PeopleRepository peopleRepository,
                                BlobRepository blobRepository,
                                IOptions<PeopleOptions> peopleOpts)
        {
            this.peopleRepository = peopleRepository;
            this.blobRepository = blobRepository;
            this.peopleOpts = peopleOpts;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var people = await peopleRepository.GetAsync();
            return new ObjectResult(people);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(string id)
        {
            Person person = await peopleRepository.GetAsync(id);
            if (person == null)
                return new NotFoundResult();

            return new ObjectResult(person);
        }

        [HttpPost]
        [ValidateModelState]
        public async Task<IActionResult> Post([FromBody]PeopleCommand command)
        {
            var person = new Person(command);
            person.ChangePhoto(peopleOpts.Value.DefaultUrlImage);

            await peopleRepository.AddAsync(person);

            var location = $"{Request.Path}/api/v1/people/{person.Id}";
            return Created(location, person);
        }

        [HttpPut("{id}")]
        [ValidateModelState]
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
        public async Task<IActionResult> Patch(string id, IFormFile file)
        {
            Person person = await peopleRepository.GetAsync(id);
            if (person == null)
                return new NotFoundResult();

            using (var stream = file.OpenReadStream())
            {   
                string urlPhoto = await blobRepository.SaveAsync(stream, "people", file.FileName);
                person.ChangePhoto(urlPhoto);

                await peopleRepository.UpdateAsync(person);

                return new OkObjectResult(person); 
            }
        }

        [HttpDelete("{id}")]
        [ValidateModelState]
        public async Task<IActionResult> Delete(string id)
        {
            Person person = await peopleRepository.GetAsync(id);
            if (person == null)
                return new NotFoundResult();

            await peopleRepository.DeleteAsync(id);
            return new OkResult();
        }

        [HttpPost("{id}/vaccines")]
        [ValidateModelState]
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
        [ValidateModelState]
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
        [ValidateModelState]
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