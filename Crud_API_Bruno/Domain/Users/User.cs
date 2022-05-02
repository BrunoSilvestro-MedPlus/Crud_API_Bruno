using System;
using Microsoft.AspNetCore.Identity;

namespace Crud_API_Bruno.Domain.Users {
    public class User : IdentityUser<Guid> {
        public Role Role { get; set; }
        public string Name { get; set; }
    }
}