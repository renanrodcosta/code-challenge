using System.ComponentModel.DataAnnotations;

namespace MinhaVida.CodeChallege.VaccinationManagement.API.Domain.Commands.People
{
    public class PeopleCommand
    {
        [MinLength(2, ErrorMessage = "The minimum name size is {1}")]
        [MaxLength(150, ErrorMessage = "The maximum name size is {1}")]
        public string Name { get; set; }

        [Range(0, 120, ErrorMessage = "The range of age between {1} and {2}")]
        public int Age { get; set; }
    }
}
