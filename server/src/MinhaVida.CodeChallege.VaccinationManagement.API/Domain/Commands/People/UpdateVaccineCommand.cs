using System;

namespace MinhaVida.CodeChallege.VaccinationManagement.API.Domain.Commands.People
{
    public class UpdateVaccineCommand
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public DateTime AppliedAt { get; set; }
    }
}
