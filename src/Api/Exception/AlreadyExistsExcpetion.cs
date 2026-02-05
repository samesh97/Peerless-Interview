namespace PeerlessInterview.src.Api.Exception;

public class AlreadyExistsException : System.Exception
{
    public AlreadyExistsException(string message) : base(message)
    {
    }
}