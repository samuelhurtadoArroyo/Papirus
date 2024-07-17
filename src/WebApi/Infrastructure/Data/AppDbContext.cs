namespace Papirus.WebApi.Infrastructure.Data;

[ExcludeFromCodeCoverage]
public class AppDbContext : DbContext
{
    public AppDbContext()
    {
    }

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public virtual DbSet<Actor> Actors { get; set; }

    public virtual DbSet<ActorType> ActorTypes { get; set; }

    public virtual DbSet<Assignment> Assignments { get; set; }

    public virtual DbSet<Case> Cases { get; set; }

    public virtual DbSet<CaseDocumentFieldValue> CaseDocumentFieldValues { get; set; }

    public virtual DbSet<CaseProcessDocument> CaseProcessDocuments { get; set; }

    public virtual DbSet<DocumentType> DocumentTypes { get; set; }

    public virtual DbSet<Firm> Firms { get; set; }

    public virtual DbSet<Holiday> Holidays { get; set; }

    public virtual DbSet<IdentificationType> IdentificationTypes { get; set; }

    public virtual DbSet<Permission> Permissions { get; set; }

    public virtual DbSet<Person> People { get; set; }

    public virtual DbSet<PersonType> PersonTypes { get; set; }

    public virtual DbSet<Process> Processes { get; set; }

    public virtual DbSet<ProcessDocumentType> ProcessDocumentTypes { get; set; }

    public virtual DbSet<ProcessTemplate> ProcessTemplates { get; set; }

    public virtual DbSet<ProcessType> ProcessTypes { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<RolePermission> RolePermissions { get; set; }

    public virtual DbSet<Status> Statuses { get; set; }

    public virtual DbSet<SubProcess> SubProcesses { get; set; }

    public virtual DbSet<Team> Teams { get; set; }

    public virtual DbSet<TeamMember> TeamMembers { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<BusinessLine> BusinessLines { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            var connectionString = Environment.GetEnvironmentVariable("ConnectionStrings:DefaultConnection");
            optionsBuilder.UseSqlServer(connectionString);
        }
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(assembly: Assembly.GetExecutingAssembly());
    }
}