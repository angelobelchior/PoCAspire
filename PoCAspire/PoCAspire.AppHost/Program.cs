var builder = DistributedApplication.CreateBuilder(args);

var redis = builder.AddRedis("cache", 6379)
                   .WithOtlpExporter();

var project = builder.AddProject<Projects.PoCAspire_Products>("products")
                     .WithReplicas(3)
                     .WithReference(redis);

var api = builder.AddProject<Projects.PocAspire_Api>("api")
                 .WithReference(project);

builder.Build().Run();