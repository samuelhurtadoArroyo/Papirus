using Papirus.WebApi.Api.Binders;
using Papirus.WebApi.Api.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Adding Serilog Configuration
builder.Host.AddSerilog(builder.Configuration);

// Add Culture for the Validators
ValidatorOptions.Global.LanguageManager.Culture = new CultureInfo("es");
builder.Services.AddFluentValidationAutoValidation().AddFluentValidationClientsideAdapters();

// Add Authentication
builder.Services.AddAuthentication(x =>
{
    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    options.RequireHttpsMetadata = true;
    options.SaveToken = true;
    options.TokenValidationParameters = new TokenValidationParameters()
    {
        ValidateIssuer = false,
        ValidateAudience = false,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["JwtBearer:Issuer"],
        ValidAudience = builder.Configuration["JwtBearer:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JwtBearer:SecretKey"]!))
    };
});

// Add the DB Context
builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Add services to the container.
builder.Services.AddControllers(options => options.ModelBinderProviders.Insert(0, new QueryRequestBinderProvider()))
    .AddJsonOptions(options => options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()));

// Add services to the container.
builder.Services.AddControllers().ConfigureApiBehaviorOptions(options =>
{
    options.InvalidModelStateResponseFactory = context =>
    {
        var errorDetails = context.ConstructErrorMessages();
        return new BadRequestObjectResult(errorDetails);
    };
});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(options =>
{
    options.UseInlineDefinitionsForEnums();

    options.SwaggerDoc("v1", new OpenApiInfo { Title = "Papirus.WebApi.Api", Version = "v1.0" });

    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Scheme = "Bearer",
        In = ParameterLocation.Header,
        Name = "Authorization",
        Description = "Please enter into field the word 'Bearer' following by space and JWT",
        Type = SecuritySchemeType.ApiKey
    });

    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            Array.Empty<string>()
        }
    });

    var xmlFiles = Directory.GetFiles(Path.Combine(AppContext.BaseDirectory), "*.xml");
    foreach (var filePath in xmlFiles)
    {
        options.IncludeXmlComments(filePath);
    }
});

builder.Services.AddFluentValidationRulesToSwagger();

// Add Authorization Methods
builder.Services.AddAuthorization();

// Add Modules
builder.Services.AddValidators();
builder.Services.AddRepositories();
builder.Services.AddServices(builder.Configuration);
builder.Services.AddMapping();
builder.Services.AddInfrastructureServices(builder.Configuration);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c => c.DocExpansion(DocExpansion.None));
}

app.UseSerilogRequestLogging();

// Add the Exception Middleware Handler
app.UseExceptionMiddleware();

app.UseHttpsRedirection();

// Add the Authentication to Enable the JWT Service
app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();

[ExcludeFromCodeCoverage]
public static partial class Program;