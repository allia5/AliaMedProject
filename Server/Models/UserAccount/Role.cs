// ---------------------------------------------------------------
// Copyright (c) Coalition of the Good-Hearted Engineers
// FREE TO USE AS LONG AS SOFTWARE FUNDS ARE DONATED TO THE POOR
// ---------------------------------------------------------------

using System;
using Microsoft.AspNetCore.Identity;
using Newtonsoft.Json;
using Server.Models.UserRoles;

namespace Server.Models.UserAccount
{
    public class Role : IdentityRole<Guid>
    {
        [JsonIgnore]
        public IEnumerable<UserRole> usersRoles { get; set; }
    }
}
