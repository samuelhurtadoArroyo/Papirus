namespace Papirus.Tests.Common.Mothers;

[ExcludeFromCodeCoverage]
public static class ConfigurationMother
{
    private static readonly Dictionary<string, string> inMemorySettings = new()
    {
        {"ConnectionStrings:DefaultConnection", "Server=localhost;Database=PapirusDb;User ID=Admin;Password=P4ssw0rd*01;Persist Security Info=False;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=True;Connection Timeout=30;" },
        {"ApplicationInsights:ConnectionString", "InstrumentationKey=2ee071ad-3dde-459d-80a0-033d8f6f09b7;IngestionEndpoint=https://eastus-8.in.applicationinsights.azure.com/;LiveEndpoint=https://eastus.livediagnostics.monitor.azure.com/" },
        {"JwtBearer:SecretKey", "TXlTM2NyM3RLM3l4QXV0aDNudDFjNHQxMG4="},
        {"JwtBearer:Issuer", "localhost"},
        {"JwtBearer:Audience", "localhost/api/v1.0" }
    };

    public static IConfiguration Default()
    {
        return new ConfigurationBuilder()
            .AddInMemoryCollection(initialData: inMemorySettings!)
            .Build();
    }
}