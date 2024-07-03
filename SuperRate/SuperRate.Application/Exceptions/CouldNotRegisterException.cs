﻿namespace SuperRate.Application.Exceptions;

public class CouldNotRegisterException : Exception
{
    public readonly string Code = "CouldNotRegister";

    public CouldNotRegisterException(string message = "An error occurred while trying to register.") : base(message)
    {
    }
}