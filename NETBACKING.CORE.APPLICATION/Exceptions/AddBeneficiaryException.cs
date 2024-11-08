namespace NETBACKING.CORE.APPLICATION.Exceptions;

public class AddBeneficiaryException : Exception
{
    public AddBeneficiaryException(string message, Exception? innerException) : base(message, innerException){}
}