using System;
using System.Collections.Generic;
using System.Linq;
using MinhaVida.CodeChallege.VaccinationManagement.API.Domain.Commands.People;
using MinhaVida.CodeChallege.VaccinationManagement.API.Domain.Exceptions;

namespace MinhaVida.CodeChallege.VaccinationManagement.API.Domain.Entities
{
    public class Person
    {
        public Person() { }

        public Person(PeopleCommand command) =>
            Apply(command);

        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string Name { get; set; }
        public int Age { get; set; }
        public string Photo { get; set; }
        public ICollection<Vaccine> Vaccines { get; set; } = new List<Vaccine>();

        public void Apply(PeopleCommand command)
        {
            Name = command.Name;
            Age = command.Age;
        }

        public void ChangePhoto(string photo)
        {
            if (string.IsNullOrWhiteSpace(photo))
                throw new PhotoEmptyException("Photo is empty!");

            Photo = photo;
        }

        public void Apply(UpdateVaccineCommand command)
        {
            EnsureNotExistVaccine(command.Name, command.Id);

            var vaccine = Vaccines.FirstOrDefault(v => v.Id == command.Id);
            if (vaccine == null) throw new VaccineNotFoundException("Vaccine not found!");

            vaccine.Name = command.Name;
            vaccine.AppliedAt = command.AppliedAt;
            vaccine.UpdatedAt = DateTime.Now.Date;
        }

        public void Apply(AddVaccineCommand command)
        {
            EnsureNotExistVaccine(command.Name);

            Vaccines.Add(new Vaccine
            {
                Id = Guid.NewGuid().ToString(),
                Name = command.Name,
                AppliedAt = command.AppliedAt,
                CreatedAt = DateTime.Now.Date
            });
        }

        public void RemoveVaccine(string id)
        {
            var vaccine = Vaccines.FirstOrDefault(v => v.Id == id);
            if (vaccine == null) throw new VaccineNotFoundException("Vaccine not found!");

            Vaccines.Remove(vaccine);
        }
        
        private void EnsureNotExistVaccine(string name)
        {
            var exists = Vaccines.Any(vaccine => vaccine.Name == name);
            if (exists) throw new VaccineAlreadyExistsException($"{name} vaccine already exists.");
        }

        private void EnsureNotExistVaccine(string name, string id)
        {
            var exists = Vaccines.Any(vaccine => vaccine.Name == name
                                                 && vaccine.Id != id);
            if (exists) throw new VaccineAlreadyExistsException($"{name} vaccine already exists.");
        }        
    }
}