using System;

public class ThirdPartyApiUnreachableException : Exception
{
    public ThirdPartyApiUnreachableException()
    {
    }

    public ThirdPartyApiUnreachableException(string message)
        : base(message)
    {
    }

    public ThirdPartyApiUnreachableException(string message, Exception inner)
        : base(message, inner)
    {
    }
}