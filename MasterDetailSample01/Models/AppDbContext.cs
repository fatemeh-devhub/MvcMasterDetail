using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using MasterDetailSample01.Models.Frameworks;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using System.Reflection;



//public class AppDbContext : DbContext
public class AppDbContext : IdentityDbContext<IdentityUser>
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {

    }

    #region [- OnModelCreating(ModelBuilder modelBuilder) -]
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // modelBuilder.HasDefaultSchema(DatabaseConstants.Schemas.UserManagement);

        #region [- ApplyConfigurationsFromAssembly() -]
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        #endregion

        #region [- RegisterAllEntities() -]
        modelBuilder.RegisterAllEntities<IDbSetEntity>(typeof(IDbSetEntity).Assembly);
        #endregion

        base.OnModelCreating(modelBuilder);
    }
    #endregion
}