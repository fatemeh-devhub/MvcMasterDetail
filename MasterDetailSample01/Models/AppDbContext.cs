using System.Reflection;
using Microsoft.EntityFrameworkCore;
using MasterDetailSample01.Models.Frameworks;



public class AppDbContext : DbContext
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