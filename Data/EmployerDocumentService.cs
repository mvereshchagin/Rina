namespace Data;

public class EmployerDocumentService : IEmployerDocumentService
{
    private readonly string _connectionString;

    public EmployerDocumentService(string connectionString)
    {
        this._connectionString = connectionString;
    }
    
    public EmployerDocument? FindById(int id)
    {
        using var db = new DataContext(this._connectionString);
        return (
            from doc in db.EmployerDocuments
            where doc.Id == id
            select doc
        ).SingleOrDefault();
    }

    public EmployerDocument? FindByName(string name)
    {
        using var db = new DataContext(this._connectionString);
        return (
            from doc in db.EmployerDocuments
            where String.Equals(doc.Name, name, StringComparison.InvariantCultureIgnoreCase)
            select doc
        ).SingleOrDefault();
    }

    public bool Add(EmployerDocument doc)
    {
        using var db = new DataContext(this._connectionString);
        db.EmployerDocuments.Add(doc);
        try
        {
            var res = db.SaveChanges();
            if (res > 0)
                return true;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }
        
        return false;
    }
    
    public bool Delete(EmployerDocument doc)
    {
        using var db = new DataContext(this._connectionString);
        db.EmployerDocuments.Remove(doc);
        try
        {
            var res = db.SaveChanges();
            if (res > 0)
                return true;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }

        return false;
    }

    public IList<EmployerDocument> GetByEmployer(Employer employer)
    {
        using var db = new DataContext(this._connectionString);
        return (
            from doc in db.EmployerDocuments
            where doc.Employer == employer
            select doc
        ).ToList();
    }
}