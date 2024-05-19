using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace LapTopShop.Services
{
    public class AppIdentityErrorDescriber : IdentityErrorDescriber
    {
        // public override IdentityError DplicateEmail(string email)
        // {
        //     var er =  base.DuplicateEmail(email);
        //     return new IdentityError(
        //         Code: er.Code,
        //         Description: $"Email {email} already exists"
        //     )
        // }
    }
}