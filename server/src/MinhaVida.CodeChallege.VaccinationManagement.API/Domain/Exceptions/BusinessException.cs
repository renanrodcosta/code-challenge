using System;

namespace MinhaVida.CodeChallege.VaccinationManagement.API.Domain.Exceptions
{
    public abstract class BusinessException : Exception
    {
        public BusinessException(string message) : base(message) { }
    }
}
