using FluentAssertions;
using MinhaVida.CodeChallege.VaccinationManagement.API.Domain.Commands.People;
using MinhaVida.CodeChallege.VaccinationManagement.API.Domain.Entities;
using MinhaVida.CodeChallege.VaccinationManagement.API.Domain.Exceptions;
using System;
using System.Linq;
using Xunit;

namespace MinhaVida.CodeChallege.VaccinationManagement.Tests.Unit.API.Domain.Entities
{
    public class PersonTests
    {
        [Fact]
        public void ShouldApplyPeople()
        {
            var command = new PeopleCommand
            {
                Name = "Teste",
                Age = 24
            };
            var person = new Person();
            person.Apply(command);

            person.Name.Should().Be(command.Name);
            person.Age.Should().Be(command.Age);
        }

        [Fact]
        public void ShouldBuildPersonByCommand()
        {
            var command = new PeopleCommand
            {
                Name = "Teste",
                Age = 24
            };
            var person = new Person(command);

            person.Name.Should().Be(command.Name);
            person.Age.Should().Be(command.Age);
        }

        [Fact]
        public void ShouldChangePhoto()
        {
            var person = new Person()
            {
                Photo = "http://avatar.com.br"
            };

            const string newPhoto = "http://newphoto";
            person.ChangePhoto(newPhoto);

            person.Photo.Should().Be(newPhoto);
        }

        [Theory]
        [InlineData("")]
        [InlineData(null)]
        public void ShouldThrowExceptionWhenPhotoIsInvalid(string newPhoto)
        {
            var person = new Person();

            Action changePhoto = () => person.ChangePhoto(newPhoto);

            changePhoto.Should().ThrowExactly<BusinessException>()
                .Which.Message.Equals("Photo is empty!");
        }

        [Fact]
        public void ShouldApplyUpdateVaccine()
        {
            var person = new Person();
            var yesterday = DateTime.Now.Date.AddDays(-1);
            var today = DateTime.Now.Date;
            person.Vaccines.Add(new Vaccine { Name = "Vaccine 1", Id = "1", AppliedAt = yesterday, CreatedAt = yesterday });
            person.Vaccines.Add(new Vaccine { Name = "Vaccine 2", Id = "2", AppliedAt = yesterday, CreatedAt = yesterday });
            person.Vaccines.Add(new Vaccine { Name = "Vaccine 3", Id = "3", AppliedAt = yesterday, CreatedAt = yesterday });

            var command = new UpdateVaccineCommand
            {
                Id = "2",
                Name = "Vaccine 2 edited",
                AppliedAt = today
            };
            person.Apply(command);

            var vaccine = person.Vaccines.ToArray()[1];
            vaccine.Name.Should().Be(command.Name);
            vaccine.AppliedAt.Should().Be(command.AppliedAt);
            vaccine.CreatedAt.ToShortDateString().Should().Be(yesterday.ToShortDateString());
            vaccine.UpdatedAt.Value.ToShortDateString().Should().Be(today.ToShortDateString());
        }

        [Fact]
        public void ShouldThrowExceptionWhenApplyUpdateVaccineIsNotFound()
        {
            var person = new Person();
            person.Vaccines.Add(new Vaccine { Name = "Vaccine 1", Id = "1", AppliedAt = DateTime.Now.Date.AddDays(-1) });

            var command = new UpdateVaccineCommand
            {
                Id = "2",
                Name = "Vaccine 2 edited",
                AppliedAt = DateTime.Now.Date
            };

            Action apply = () => person.Apply(command);

            apply.Should().ThrowExactly<BusinessException>()
                .Which.Message.Equals("Vaccine not found!");
        }

        [Fact]
        public void ShouldThrowExceptionWhenApplyUpdateAndExistsAnotherVaccineWithNameEquals()
        {
            var person = new Person();
            person.Vaccines.Add(new Vaccine { Name = "Vaccine 2 edited", Id = "1", AppliedAt = DateTime.Now.Date.AddDays(-1) });

            var command = new UpdateVaccineCommand
            {
                Id = "2",
                Name = "Vaccine 2 edited",
                AppliedAt = DateTime.Now.Date
            };

            Action apply = () => person.Apply(command);

            apply.Should().ThrowExactly<BusinessException>()
                .Which.Message.Equals("Already exists vaccine Vaccine 2 edited");
        }
    }
}
