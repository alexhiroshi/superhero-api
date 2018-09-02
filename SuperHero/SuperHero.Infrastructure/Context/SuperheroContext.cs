using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using SuperHero.Domain.Entities;

namespace SuperHero.Infrastructure.Context
{
    public class SuperheroContext : DbContext
    {
        public virtual DbSet<Superhero> Superhero { get; set; }
        public virtual DbSet<Superpower> Superpower { get; set; }
        public virtual DbSet<ProtectionArea> ProtectionArea { get; set; }
        public virtual DbSet<SuperheroSuperpower> SuperheroSuperpower { get; set; }
        public virtual DbSet<AuditEvent> AuditEvent { get; set; }
        public virtual DbSet<User> User { get; set; }
        public virtual DbSet<Role> Role { get; set; }
        public virtual DbSet<UserRole> UserRole { get; set; }

        public SuperheroContext(DbContextOptions<SuperheroContext> options)
                    : base(options)
        { }

        public override int SaveChanges(bool acceptAllChangesOnSuccess)
        {
            var entities = ChangeTracker.Entries().Where(x => x.Entity is Base
                                                         && (x.State == EntityState.Added || x.State == EntityState.Modified));

            foreach (var entity in entities)
            {
                if (entity.State == EntityState.Added)
                    ((Base)entity.Entity).DateCreated = DateTime.Now;

                if (entity.State == EntityState.Modified)
                    ((Base)entity.Entity).DateModified = DateTime.Now;
            }

            return base.SaveChanges(acceptAllChangesOnSuccess);
        }
    }
}