using AgriSmartSierra.Application.Interfaces;
using AgriSmartSierra.Application.Services;
using AgriSmartSierra.Domain.Interfaces;
using AgriSmartSierra.Infrastructure.Data;
using AgriSmartSierra.Infrastructure.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Serilog;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog((context, configuration) =>
    configuration.ReadFrom.Configuration(context.Configuration));

builder.Services.AddDbContext<AgriSmartDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["Jwt:Issuer"] ?? "AgriSmartSierra",
            ValidAudience = builder.Configuration["Jwt:Audience"] ?? "AgriSmartSierra",
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"] ?? "YourSecretKeyHere12345678901234567890"))
        };
    });

builder.Services.AddAuthorization();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "AgriSmart Sierra API", Version = "v1" });
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "JWT Authorization header using the Bearer scheme. Enter 'Bearer' [space] and then your token.",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer"
    });
    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = "Bearer" }
            },
            Array.Empty<string>()
        }
    });
});

builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IFarmerProfileRepository, FarmerProfileRepository>();
builder.Services.AddScoped<IBuyerProfileRepository, BuyerProfileRepository>();
builder.Services.AddScoped<IAgronomistProfileRepository, AgronomistProfileRepository>();
builder.Services.AddScoped<ICropRepository, CropRepository>();
builder.Services.AddScoped<IFarmRepository, FarmRepository>();
builder.Services.AddScoped<ICropActivityRepository, CropActivityRepository>();
builder.Services.AddScoped<IPestReportRepository, PestReportRepository>();
builder.Services.AddScoped<IWeatherLogRepository, WeatherLogRepository>();
builder.Services.AddScoped<IMarketplaceListingRepository, MarketplaceListingRepository>();
builder.Services.AddScoped<IOrderRepository, OrderRepository>();
builder.Services.AddScoped<IForumPostRepository, ForumPostRepository>();
builder.Services.AddScoped<IForumCommentRepository, ForumCommentRepository>();
builder.Services.AddScoped<ILoanApplicationRepository, LoanApplicationRepository>();
builder.Services.AddScoped<IInsuranceInfoRepository, InsuranceInfoRepository>();

builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IFarmerProfileService, FarmerProfileService>();
builder.Services.AddScoped<IBuyerProfileService, BuyerProfileService>();
builder.Services.AddScoped<IAgronomistProfileService, AgronomistProfileService>();
builder.Services.AddScoped<ICropService, CropService>();
builder.Services.AddScoped<IFarmService, FarmService>();
builder.Services.AddScoped<ICropActivityService, CropActivityService>();
builder.Services.AddScoped<IWeatherService, WeatherService>();
builder.Services.AddScoped<IResourceCalculatorService, ResourceCalculatorService>();
builder.Services.AddScoped<IPestReportService, PestReportService>();
builder.Services.AddScoped<IMLPredictionService, MLPredictionService>();
builder.Services.AddScoped<IMarketplaceService, MarketplaceService>();
builder.Services.AddScoped<IOrderService, OrderService>();
builder.Services.AddScoped<ICropPriceService, CropPriceService>();
builder.Services.AddScoped<IForumService, ForumService>();
builder.Services.AddScoped<ILoanService, LoanService>();
builder.Services.AddScoped<IInsuranceService, InsuranceService>();
builder.Services.AddScoped<IAnalyticsService, AnalyticsService>();

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
    });
});

var app = builder.Build();

app.UseSerilogRequestLogging();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();