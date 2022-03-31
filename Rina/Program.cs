using Rina;

Configuration configuration = new Configuration();
Project project = new Project(
    configuration.SettingsService, 
    configuration.EmployerService,
    configuration.EmployerDocumentService);
project.Run();
