namespace MinhaVida.CodeChallege.VaccinationManagement.API.Domain.Exceptions
{
    public class PhotoEmptyException : BusinessException
    {
        public PhotoEmptyException(string message) : base(message)
        {
        }
    }
}
