using System;

namespace MinhaVida.CodeChallege.VaccinationManagement.API.Domain.Entities
{
    public class Vaccine
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public DateTime AppliedAt { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public DateTime? UpdatedAt { get; set; }
    }
}