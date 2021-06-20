using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using TEST_CommonCalendar.Models;

namespace TEST_CommonCalendar.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<TEST_CommonCalendar.Models.mEVENT> mEVENT { get; set; }
        public DbSet<TEST_CommonCalendar.Models.mErrorHandling> mErrorHandling { get; set; }
    }
}
