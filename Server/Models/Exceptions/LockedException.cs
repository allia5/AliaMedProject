// ---------------------------------------------------------------
// Copyright (c) Coalition of the Good-Hearted Engineers
// FREE TO USE AS LONG AS SOFTWARE FUNDS ARE DONATED TO THE POOR
// ---------------------------------------------------------------

using System;

namespace Server.Models.Doctor.Exceptions
{
    public class LockedException : Exception
    {
        public LockedException(Exception innerException, string actor)
            : base(message: $"Locked {actor} record exception, please try again later.", innerException) { }
    }
}
