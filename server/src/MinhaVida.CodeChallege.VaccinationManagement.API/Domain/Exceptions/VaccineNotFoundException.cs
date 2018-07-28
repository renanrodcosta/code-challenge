namespace MinhaVida.CodeChallege.VaccinationManagement.API.Domain.Exceptions
{
    public class VaccineNotFoundException : BusinessException
    {
        public VaccineNotFoundException(string message) : base(message)
        {
        }
    }
}
