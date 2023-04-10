// ---------------------------------------------------------------
// Copyright (c) Coalition of the Good-Hearted Engineers
// FREE TO USE AS LONG AS SOFTWARE FUNDS ARE DONATED TO THE POOR
// ---------------------------------------------------------------

using System;

namespace Server.Models.Doctor.Exceptions
{
    public class InvalidException : Exception
    {
        public InvalidException(string parameterName, object parameterValue, string actor)
            : base(message: $"Invalid {actor}, " +
                  $"parameter name: {parameterName}, " +
                  $"parameter value: {parameterValue}.")
        { }
    }
}
