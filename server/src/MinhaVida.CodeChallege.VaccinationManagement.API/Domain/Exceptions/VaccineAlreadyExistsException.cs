namespace MinhaVida.CodeChallege.VaccinationManagement.API.Domain.Exceptions
{
    public class VaccineAlreadyExistsException : BusinessException
    {
        public VaccineAlreadyExistsException(string message) : base(message)
        {
        }
    }
}
