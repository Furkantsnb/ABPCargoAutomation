
using CargoAutomation.Agentas;
using CargoAutomation.Lines;
using CargoAutomation.Stations;
using CargoAutomation.TransferCenters;
using CargoAutomation.Units;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using Volo.Abp.AuditLogging.EntityFrameworkCore;
using Volo.Abp.BackgroundJobs.EntityFrameworkCore;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore.Modeling;
using Volo.Abp.FeatureManagement.EntityFrameworkCore;
using Volo.Abp.Identity;
using Volo.Abp.Identity.EntityFrameworkCore;
using Volo.Abp.OpenIddict.EntityFrameworkCore;
using Volo.Abp.PermissionManagement.EntityFrameworkCore;
using Volo.Abp.SettingManagement.EntityFrameworkCore;
using Volo.Abp.TenantManagement;
using Volo.Abp.TenantManagement.EntityFrameworkCore;

namespace CargoAutomation.EntityFrameworkCore;

[ReplaceDbContext(typeof(IIdentityDbContext))]
[ReplaceDbContext(typeof(ITenantManagementDbContext))]
[ConnectionStringName("Default")]
public class CargoAutomationDbContext :
    AbpDbContext<CargoAutomationDbContext>,
    IIdentityDbContext,
    ITenantManagementDbContext
{
    /* Add DbSet properties for your Aggregate Roots / Entities here. */

    #region Entities from the modules

    /* Notice: We only implemented IIdentityDbContext and ITenantManagementDbContext
     * and replaced them for this DbContext. This allows you to perform JOIN
     * queries for the entities of these modules over the repositories easily. You
     * typically don't need that for other modules. But, if you need, you can
     * implement the DbContext interface of the needed module and use ReplaceDbContext
     * attribute just like IIdentityDbContext and ITenantManagementDbContext.
     *
     * More info: Replacing a DbContext of a module ensures that the related module
     * uses this DbContext on runtime. Otherwise, it will use its own DbContext class.
     */

    //Identity
    public DbSet<IdentityUser> Users { get; set; }
    public DbSet<IdentityRole> Roles { get; set; }
    public DbSet<IdentityClaimType> ClaimTypes { get; set; }
    public DbSet<OrganizationUnit> OrganizationUnits { get; set; }
    public DbSet<IdentitySecurityLog> SecurityLogs { get; set; }
    public DbSet<IdentityLinkUser> LinkUsers { get; set; }
    public DbSet<IdentityUserDelegation> UserDelegations { get; set; }

    // Tenant Management
    public DbSet<Tenant> Tenants { get; set; }
    public DbSet<TenantConnectionString> TenantConnectionStrings { get; set; }

    public DbSet<Unit> Units { get; set; }
    public DbSet<Agenta> Agentas { get; set; }
    public DbSet<CargoAutomation.TransferCenters.TransferCenter> TransferCenters { get; set; }
    public DbSet<Station> Stations { get; set; }
    public DbSet<Line> Lines { get; set; }
    //public DbSet<Book> Books { get; set; }
    //public DbSet<Kategory> kategories { get; set; }
    #endregion

    public CargoAutomationDbContext(DbContextOptions<CargoAutomationDbContext> options)
        : base(options)
    {

    }
    public void Configure(EntityTypeBuilder<Line> builder)
    {
        // LineType alanını int olarak belirtme
        builder.Property(l => l.LineType)
               .HasConversion<int>(); // Enum'i int'e dönüştür

        // Diğer konfigürasyonlar buraya eklenebilir
    }
    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        /* Include modules to your migration db context */

        builder.ConfigurePermissionManagement();
        builder.ConfigureSettingManagement();
        builder.ConfigureBackgroundJobs();
        builder.ConfigureAuditLogging();
        builder.ConfigureIdentity();
        builder.ConfigureOpenIddict();
        builder.ConfigureFeatureManagement();
        builder.ConfigureTenantManagement();

        /* Configure your own tables/entities inside here */

        //builder.Entity<YourEntity>(b =>
        //{
        //    b.ToTable(CargoAutomationConsts.DbTablePrefix + "YourEntities", CargoAutomationConsts.DbSchema);
        //    b.ConfigureByConvention(); //auto configure for the base class props
        //    //...
        //});

        builder.Entity<Unit>(b =>
        {
            b.ToTable(CargoAutomationConsts.DbTablePrefix + "Units", CargoAutomationConsts.DbSchema);
            b.ConfigureByConvention();

     
        });

        // Agentas
        builder.Entity<Agenta>(b =>
        {
            b.ToTable(CargoAutomationConsts.DbTablePrefix + "Agentas", CargoAutomationConsts.DbSchema);
            b.ConfigureByConvention();

          
        });

        // TransferCenters
        builder.Entity<CargoAutomation.TransferCenters.TransferCenter>(b =>
        {
            b.ToTable(CargoAutomationConsts.DbTablePrefix + "TransferCenters", CargoAutomationConsts.DbSchema);
            b.ConfigureByConvention();


            // TransferCenter'dan Agenta'ya one-to-many ilişkisi belirle
            b.HasMany(tc => tc.Agentas)
                .WithOne(a => a.TransferCenter)
                .HasForeignKey(a => a.TransferCenterId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);
        });

        //unit ile line arasında çoka çok ilişki kuruldu
        builder.Entity<Station>().HasKey(sc => new { sc.UnitId, sc.LineId });

        builder.Entity<Station>().HasOne(sc => sc.Unit)
            .WithMany(sc => sc.Stations)
            .HasForeignKey(sc => sc.UnitId);

        builder.Entity<Station>().HasOne(sc => sc.Line)
           .WithMany(sc => sc.Stations)
           .HasForeignKey(sc => sc.LineId);

        // Stations
        builder.Entity<Station>(b =>
        {
            b.ToTable(CargoAutomationConsts.DbTablePrefix + "Stations", CargoAutomationConsts.DbSchema);
            b.ConfigureByConvention();

           

            //// Station'dan Line'a many-to-one ilişkisi belirle
            //b.HasOne(s => s.Line)
            //    .WithMany(l => l.Stations)
            //    .HasForeignKey(s => s.LineId)
            //    .IsRequired()
            //    .OnDelete(DeleteBehavior.Restrict);
        });

        // Lines
        builder.Entity<Line>(b =>
        {
            b.ToTable(CargoAutomationConsts.DbTablePrefix + "Lines", CargoAutomationConsts.DbSchema);
            b.ConfigureByConvention();


            //// Line'dan Station'a one-to-many ilişkisi belirle
            //b.HasMany(l => l.Stations)
            //    .WithOne(s => s.Line)
            //    .HasForeignKey(s => s.LineId)
            //    .IsRequired()
            //    .OnDelete(DeleteBehavior.Cascade);
        });
    }
}
