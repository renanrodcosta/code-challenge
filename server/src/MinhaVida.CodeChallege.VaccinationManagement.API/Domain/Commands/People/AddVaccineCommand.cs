using System;

namespace MinhaVida.CodeChallege.VaccinationManagement.API.Domain.Commands.People
{
    public class AddVaccineCommand
    {
        public string Name { get; set; }

        public DateTime AppliedAt { get; set; }
    }
}
