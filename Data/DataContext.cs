using Microsoft.EntityFrameworkCore;

namespace Data;

public class DataContext : DbContext
{
    private readonly string _connectionString;
    
    public DbSet<Employer> Employers { get; set; } = null!;
    public DbSet<EmployerDocument> EmployerDocuments { get; set; } = null!;

    public DataContext(string connectionString)
    {
        this._connectionString = connectionString;
    }
}