using System;

namespace Arya.Infrastructure.CrossCutting.Enums
{
    [Flags]
    public enum Roles
    {
        None,
        User,
        Admin
    }
}
