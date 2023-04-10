// ---------------------------------------------------------------
// Copyright (c) Coalition of the Good-Hearted Engineers
// FREE TO USE AS LONG AS SOFTWARE FUNDS ARE DONATED TO THE POOR
// ---------------------------------------------------------------

using System;

namespace Server.Models.Doctor.Exceptions
{
    public class ServiceException : Exception
    {
        public ServiceException(Exception innerException)
            : base(message: "Service error occurred, contact support.", innerException) { }
    }
}
