﻿
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using talabat.Core.Entities.Identity;

namespace talabat.Repository.Identity
{
    public class AppIdentityDbcontext:IdentityDbContext<AppUser>
    {
        public AppIdentityDbcontext(DbContextOptions<AppIdentityDbcontext> options):base(options)
        {
            
        }
    }
}
