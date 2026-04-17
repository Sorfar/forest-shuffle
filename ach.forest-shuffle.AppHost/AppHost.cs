var builder = DistributedApplication.CreateBuilder(args);

builder.AddProject<Projects.ach_forest_shuffle_mobile_app>("ach-forest-shuffle-mobile-app");

builder.Build().Run();
