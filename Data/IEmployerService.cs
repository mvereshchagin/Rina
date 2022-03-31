namespace Data;

public interface IEmployerService
{
    public Employer? FindById(int id);

    public Employer? FindByName(string name);

    public bool Add(Employer employer);

    public bool Delete(Employer employer);
}