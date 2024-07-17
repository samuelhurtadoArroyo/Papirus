var builder = WebApplication.CreateBuilder(args);

// Custom service configurations
builder.Services.AddEmailServices(builder.Configuration);
builder.Services.AddEmailApiVersioningAndSwagger();
builder.Services.AddHttpServices(builder.Configuration);
builder.Services.AddMapping();

// ASP.NET Core essentials
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddHealthChecks();

var app = builder.Build();

app.MapHealthChecks("api/v1/healthz");

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        var provider = app.Services.GetRequiredService<IApiVersionDescriptionProvider>();
        var endpoints = provider.ApiVersionDescriptions
            .Select(description => new
            {
                Url = $"/swagger/{description.GroupName}/swagger.json",
                Name = description.GroupName.ToUpperInvariant()
            });

        foreach (var endpoint in endpoints)
        {
            options.SwaggerEndpoint(endpoint.Url, endpoint.Name);
        }
    });
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();