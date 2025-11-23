using APIGateway.API.ServiceA;
using APIGateway.Filter;
using APIGateway.Services;
using CorrelationId.DependencyInjection;
using CorrelationId.HttpClient;
using DAO.Repository;
using DAO.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Data;
using System.Text;



var builder = WebApplication.CreateBuilder(args);

string dbtype = builder.Configuration["DBType"];
string connectionstring = builder.Configuration["MSSqlConnectionString"];

builder.Services.AddDatabase(dbtype, connectionstring);


builder.Services.AddCorrelationId();

var configuration = builder.Configuration;

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
        policy => policy
            .AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader());
});


builder.Services.AddTransient<ICommonService, CommonService>();

builder.Services.AddTransient<ILoginService, LoginService>();

builder.Services.AddTransient<IRepository, Repository>();

builder.Services.AddHttpClient<IServiceA_API, ServiceA_API>(httpclient=>
{
    httpclient.BaseAddress = new Uri(configuration["ServiceABaseUrl"]);
    httpclient.DefaultRequestHeaders.Add("internal_API","true");
    httpclient.DefaultRequestHeaders.Add("ServiceA_Token", "123");

}).AddCorrelationIdForwarding();

builder.Services.AddSingleton<IDatabaseFactory>(sp =>
    DatabaseFactory.Create(
        builder.Configuration["Database:Type"],
        sp
    ));

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
//builder.Services.AddSwaggerGen(c =>
//{
//    c.OperationFilter<InternalTokenHeaderFilter>();
//});

builder.Services.AddSwaggerGen(c =>
{
    c.OperationFilter<InternalTokenHeaderFilter>();
    c.SwaggerDoc("v1", new() { Title = "Your API", Version = "v1" });
    c.AddSecurityDefinition("Bearer", new Microsoft.OpenApi.Models.OpenApiSecurityScheme
    {
        Description = "JWT Authorization header using the Bearer scheme. Example: \"Bearer {token}\"",
        Name = "Authorization",
        In = Microsoft.OpenApi.Models.ParameterLocation.Header,
        Type = Microsoft.OpenApi.Models.SecuritySchemeType.Http,
        Scheme = "bearer"
    });
    c.AddSecurityRequirement(new Microsoft.OpenApi.Models.OpenApiSecurityRequirement
    {
        {
            new Microsoft.OpenApi.Models.OpenApiSecurityScheme
            {
                Reference = new Microsoft.OpenApi.Models.OpenApiReference
                {
                    Type = Microsoft.OpenApi.Models.ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[] {}
        }
    });
});

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        var jwtIssuer = builder.Configuration["Jwt:Issuer"];
        var jwtAudience = builder.Configuration["Jwt:Audience"];
        var jwtSecret = builder.Configuration["Jwt:Secret"];

        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = jwtIssuer,
            ValidAudience = jwtAudience,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSecret))
        };
    });

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();


app.UseCors("AllowAll");

app.Run();
