namespace Data;

public class EmployerDocument
{
    public int Id { get; set; }
    public string Name { get; set; }
    public DateTime DateCreate { get; set; }
    public byte[] Content { get; set; }
    
    public Employer Employer { get; set; }
}