using System.Runtime.Serialization;

namespace Projects.Exceptions;

[Serializable]
public class InvalidProjectException : Exception
{
    public InvalidProjectException()
    {
    }

    protected InvalidProjectException(SerializationInfo info, StreamingContext context) : base(info, context)
    {
    }

    public InvalidProjectException(string? message) : base(message)
    {
    }

    public InvalidProjectException(string? message, Exception? innerException) : base(message, innerException)
    {
    }
}
