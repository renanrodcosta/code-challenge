using System;
using System.ComponentModel.DataAnnotations;

namespace MinhaVida.CodeChallege.VaccinationManagement.API.Domain.Commands.People
{
    public class AddVaccineCommand
    {
        [MinLength(2, ErrorMessage = "The minimum name size is {1}")]
        [MaxLength(150, ErrorMessage = "The maximum name size is {1}")]
        public string Name { get; set; }

        [Required]
        public DateTime AppliedAt { get; set; }
    }
}
