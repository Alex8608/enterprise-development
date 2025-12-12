var builder = DistributedApplication.CreateBuilder(args);

var db = builder.AddSqlServer("sqlserver")
    .WithDataVolume()
    .AddDatabase("AirlineCompanyDb");

builder.AddProject<Projects.AirlineCompany_Api>("airline-api")
    .WithReference(db)
    .WithExternalHttpEndpoints();

builder.Build().Run();