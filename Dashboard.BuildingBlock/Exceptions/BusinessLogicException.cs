using System;
using System.Runtime.Serialization;

namespace Dashboard.BuildingBlock.Exceptions;

[Serializable]
public class BusinessLogicException : Exception
{
    public BusinessLogicException()
    {
    }

    protected BusinessLogicException(SerializationInfo info, StreamingContext context) : base(info, context)
    {
    }

    public BusinessLogicException(string? message) : base(message)
    {
    }

    public BusinessLogicException(string? message, Exception? innerException) : base(message, innerException)
    {
    }
}
