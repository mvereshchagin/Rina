namespace Data;

public interface IEmployerDocumentService
{
    public EmployerDocument? FindById(int id);

    public EmployerDocument? FindByName(string name);

    public bool Add(EmployerDocument doc);

    public bool Delete(EmployerDocument doc);

    public IList<EmployerDocument> GetByEmployer(Employer doc);
}