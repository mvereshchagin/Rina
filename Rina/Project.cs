using System.Diagnostics;
using Data;

namespace Rina;

public class Project
{
    private readonly IEmployerService _employerService;
    private readonly ISettingsService _settingsService;
    private readonly IEmployerDocumentService _employerDocumentService;

    private Employer? _curEmployer;
    
    public Project(ISettingsService settingsService, 
        IEmployerService employerService,
        IEmployerDocumentService employerDocumentService)
    {
        this._employerService = employerService;
        this._settingsService = settingsService;
        this._employerDocumentService = employerDocumentService;
    }

    public void Run()
    {
        _curEmployer = Authorize();
        if (_curEmployer is null)
        {
            // read name
            string name = "";
            _curEmployer = _employerService.FindByName(name);
            if (_curEmployer is null)
                _curEmployer = Register(name);
        }
        
        
    }

    private Employer? Authorize()
    {
        var settings = _settingsService.Read();
        int? userId = settings.UserId;
        if (userId is null)
            return null;

        var employer = _employerService.FindById(userId.Value);
        return employer;
    }

    private Employer? Register(string name)
    {
        var employer = new Employer() {Name = name};
        _employerService.Add(employer);
        
        var settings = _settingsService.Read();
        settings.UserId = employer.Id;
        _settingsService.Update(settings);

        return employer;
    }

    private void UploadFile(string path)
    {
        if (_curEmployer is null)
            return;

        if (!File.Exists(path))
            return;
        
        var name = Path.GetFileName(path);
        byte[] content;

        using var fStream = File.Open(path, FileMode.Open);
        using var br = new BinaryReader(fStream);
        content = br.ReadBytes((int) fStream.Length);

        var doc = new EmployerDocument() { Name = name, Content = content};

        _employerDocumentService.Add(doc);
    }

    private void DownloadAndOpenFile(int id)
    {
        var doc = _employerDocumentService.FindById(id);
        if (doc is null)
            return;

        var path = Path.Combine(Path.GetTempPath(), doc.Name);

        using var fStream = File.Create(path);
        using var writer = new BinaryWriter(fStream);
        writer.Write(fStream.Length);
        
        OpenFile(path);
    }

    private void OpenFile(string path)
    {
        using Process fileOpener = new Process();

        fileOpener.StartInfo.FileName = "explorer";
        fileOpener.StartInfo.Arguments = "\"" + path + "\"";
        fileOpener.Start();
    }

    private void ListDocuments()
    {
        var docs = _employerDocumentService.GetByEmployer(_curEmployer);
    }
}