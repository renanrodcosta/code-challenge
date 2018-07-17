using System;
using System.Collections.Generic;

namespace MinhaVida.CodeChallege.VaccinationManagement.API.Domain.Entities
{
    public class Person
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string Name { get; set; }
        public int Age { get; set; }
        public string Photo { get; set; }
        public ICollection<Vaccine> Vaccines { get; set; }
    }
}