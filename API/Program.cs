using Microsoft.Extensions.Diagnostics.Metrics;
using OpenTelemetry.Metrics;

var builder = WebApplication.CreateBuilder(args);

builder.WebHost.ConfigureKestrel(
    ex => ex.ListenAnyIP(55555));

builder.Services.AddOpenTelemetry()
    .WithMetrics(
        metrics => metrics
            .AddPrometheusExporter()
            .AddRuntimeInstrumentation()
            .AddAspNetCoreInstrumentation()
    );


var app = builder.Build();


app.MapPrometheusScrapingEndpoint();

app.MapGet("/", () => "Hello World!");

Console.Write(1);

app.Run();