namespace Data;

public class EmployerService : IEmployerService
{
    private readonly string _connectionString;
    
    public EmployerService(string connectionString)
    {
        this._connectionString = connectionString;
    }
    
    public Employer? FindById(int id)
    {
        using var db = new DataContext(this._connectionString);
        return (
            from employer in db.Employers
            where employer.Id == id
            select employer
            ).SingleOrDefault();
    }
    
    public Employer? FindByName(string name)
    {
        using var db = new DataContext(this._connectionString);
        return (
            from employer in db.Employers
            where String.Equals(employer.Name, name, StringComparison.InvariantCultureIgnoreCase)
            select employer
        ).SingleOrDefault();
    }

    public bool Add(Employer employer)
    {
        using var db = new DataContext(this._connectionString);
        db.Employers.Add(employer);
        try
        {
            db.SaveChanges();
            return true;
        }
        catch (Exception e)
        {
            return false;
        }
    }
    
    public bool Delete(Employer employer)
    {
        using var db = new DataContext(this._connectionString);
        db.Employers.Remove(employer);
        try
        {
            db.SaveChanges();
            return true;
        }
        catch (Exception e)
        {
            return false;
        }
    }
    
}