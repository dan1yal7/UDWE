﻿using Microsoft.EntityFrameworkCore;
using UniDwe.Models;
using UniDwe.Session;

namespace UniDwe.Infrastructure
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {

        }
        public DbSet<User> users { get; set; }
        public DbSet<Event> events { get; set; }
        public DbSet<Registration> registrations { get; set; }
        public DbSet<DbSession> sessions { get; set; }
        public DbSet<UserToken> usertokens { get; set; }
        public DbSet<Profile> profiles { get; set; }

    }
}
