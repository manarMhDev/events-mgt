

using Events.Application.Helpers;
using Events.Domain.BaseEntities;
using Events.Domain.Entities;
using Events.Infrastructure.Abstractions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace Events.Infrastructure.Contexts
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public IHttpContextAccessor HttpContextAccessor { get; }
        public ApplicationDbContext(DbContextOptions options, IHttpContextAccessor httpContextAccessor) : base(options)
        {
            HttpContextAccessor = httpContextAccessor;
        }
        public override int SaveChanges()
        {
            HandleSaveChanges();
            return base.SaveChanges();
        }
        public  override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            HandleSaveChanges();
            return  base.SaveChangesAsync(cancellationToken);
        }
        private void HandleSaveChanges()
        {
            string userId = HttpContextAccessor.HttpContext.User.GetLoggedInUserId();
            DateTime dateTime = DateTime.UtcNow;
            var modifiedEntries = ChangeTracker.Entries()
                .Where(x => x.Entity is IBaseEntity
                    && (x.State == EntityState.Added
                    || x.State == EntityState.Modified
                    || x.State == EntityState.Deleted));

            foreach (var entry in modifiedEntries)
            {
                IBaseEntity entity = entry.Entity as IBaseEntity;
                if (entity != null)
                {

                    switch (entry.State)
                    {
                        case (EntityState.Added):
                            {
                                entity.EntityCreated(userId, dateTime);
                                break;
                            }
                        case (EntityState.Modified):
                            {
                                entity.EntityUpdated(userId, dateTime, true);
                                break;
                            }
                        default: break;
                    }
                }
                var validationContext = new ValidationContext(entity);
                Validator.ValidateObject(entity, validationContext);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            RegisterConfigurations(modelBuilder);
        }

        protected virtual void RegisterConfigurations(ModelBuilder modelBuilder)
        {
            var ConfigurationAssemblies = AppDomain.CurrentDomain.GetAssemblies()
                                                                    .Where(x =>
                                                                                x.ManifestModule != null &&
                                                                                x.ManifestModule.Name.StartsWith("Events.", StringComparison.OrdinalIgnoreCase) &&
                                                                                x.ManifestModule.Name.EndsWith("Infrastructure.dll", StringComparison.OrdinalIgnoreCase))
                                                                    .ToList();
            var ConfigurationTypes = PickConfigurationTypes(ConfigurationAssemblies);
            foreach (var type in ConfigurationTypes)
            {
                modelBuilder.ApplyConfiguration((dynamic)Activator.CreateInstance(type));
            }
        }

        private IEnumerable<Type> PickConfigurationTypes(IEnumerable<Assembly> assemblies)
        {
            return assemblies.SelectMany(x => x.GetTypes()).Where(x => x.IsClass && !x.IsAbstract && typeof(IBaseEntityConfiguration).IsAssignableFrom(x));
        }
    }
}
