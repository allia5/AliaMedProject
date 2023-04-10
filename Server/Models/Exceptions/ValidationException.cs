// ---------------------------------------------------------------
// Copyright (c) Coalition of the Good-Hearted Engineers
// FREE TO USE AS LONG AS SOFTWARE FUNDS ARE DONATED TO THE POOR
// ---------------------------------------------------------------

using System;

namespace Server.Models.Doctor.Exceptions
{
    public class ValidationException : Exception
    {
        public ValidationException(Exception innerException)
            : base(message: "Invalid input, contact support.", innerException) { }
    }
}
