// ---------------------------------------------------------------
// Copyright (c) Coalition of the Good-Hearted Engineers
// FREE TO USE AS LONG AS SOFTWARE FUNDS ARE DONATED TO THE POOR
// ---------------------------------------------------------------

using System;

namespace Server.Models.Doctor.Exceptions
{
    public class AlreadyExistsException : Exception
    {
        public AlreadyExistsException(string actor)
            : base(message: $"{actor} with the same id already exists.") { }
    }
}
