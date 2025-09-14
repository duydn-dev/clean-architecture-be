var builder = DistributedApplication.CreateBuilder(args);
builder.AddProject<Projects.PublicApi>("ManagerApi");

builder.Build().Run();