var builder = DistributedApplication.CreateBuilder(args);

var sql = builder.AddSqlServer("sqlserver")
    .WithDataVolume()
    .AddDatabase("AirlineCompanyDb");

builder.AddProject<Projects.AirlineCompany_API>("airline-api")
    .WithReference(sql)
    .WithExternalHttpEndpoints();

builder.Build().Run();