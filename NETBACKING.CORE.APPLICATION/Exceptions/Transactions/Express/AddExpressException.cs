namespace NETBACKING.CORE.APPLICATION.Exceptions.Transactions.Express;

public class AddExpressException : Exception
{
    public AddExpressException(string message, Exception? exception) : base(message, exception){}
}