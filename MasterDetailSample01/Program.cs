using MasterDetailSample01.ApplicationServices.services.Contracts;
using MasterDetailSample01.ApplicationServices.services;
using MasterDetailSample01.Models.Services.Contracts;
using MasterDetailSample01.Models.Services.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi;

using System.Text;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AppDbContext>();
builder.Services.AddScoped<IOrderHeaderRepository, OrderHeaderRepository>();
builder.Services.AddScoped<IOrderHeaderApplicationService, OrderHeaderApplicationService>();
builder.Services.AddScoped<ICustomerRepository, CustomerRepository>();
builder.Services.AddScoped<ICustomerApplicationService, CustomerApplicationService>();
builder.Services.AddScoped<ISellerRepository, SellerRepository>();
builder.Services.AddScoped<ISellerApplicationService, SellerApplicationService>();
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IProductApplicationService, ProductApplicationService>();




// =====================================================
// 1. Database
// =====================================================

#region [- Config EF -]

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection")));

#endregion


// =====================================================
// 2. Identity
// =====================================================

#region [- Config Identity -]

builder.Services
    .AddIdentity<IdentityUser, IdentityRole>()
    .AddEntityFrameworkStores<AppDbContext>()
    .AddDefaultTokenProviders();

#endregion


// =====================================================
// 3. Authentication
// =====================================================

#region [- Config Authentication -]

builder.Services
    .AddAuthentication(options =>
    {
        options.DefaultAuthenticateScheme =
            JwtBearerDefaults.AuthenticationScheme;

        options.DefaultChallengeScheme =
            JwtBearerDefaults.AuthenticationScheme;

        options.DefaultScheme =
            JwtBearerDefaults.AuthenticationScheme;
    })

    .AddJwtBearer(options =>
    {
        options.SaveToken = true;

        options.RequireHttpsMetadata = false;

        options.TokenValidationParameters = new TokenValidationParameters
        {
            // ????? Issuer
            ValidateIssuer = true,

            // ????? Audience
            ValidateAudience = true,

            // ????? Expiration
            ValidateLifetime = true,

            // ????? Signature
            ValidateIssuerSigningKey = true,

            ValidIssuer =
                builder.Configuration["JWT:ValidIssuer"],

            ValidAudience =
                builder.Configuration["JWT:ValidAudience"],

            IssuerSigningKey =
                new SymmetricSecurityKey(
                    Encoding.UTF8.GetBytes(
                        builder.Configuration["JWT:Secret"]!))
        };
    });

#endregion


// =====================================================
// 4. Authorization
// =====================================================

builder.Services.AddAuthorization();


// =====================================================
// 5. Controllers
// =====================================================

builder.Services.AddControllers();


// =====================================================
// 6. Swagger
// =====================================================

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(options =>
{
    // ---------------------------------------------
    // JWT Bearer Definition
    // ---------------------------------------------

    options.AddSecurityDefinition(
        "Bearer",
        new OpenApiSecurityScheme
        {
            Name = "Authorization",

            Type = SecuritySchemeType.Http,

            Scheme = "bearer",

            BearerFormat = "JWT",

            In = ParameterLocation.Header,

            Description =
                "Enter your JWT token."
        });


    // ---------------------------------------------
    // JWT Security Requirement
    // ---------------------------------------------

    options.AddSecurityRequirement(document =>
        new OpenApiSecurityRequirement
        {
            [
                new OpenApiSecuritySchemeReference(
                    "Bearer",
                    document)
            ] = []
        });
});


// =====================================================
// Build
// =====================================================

var app = builder.Build();


// =====================================================
// 7. Swagger Middleware
// =====================================================

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();

    app.UseSwaggerUI();
}


// =====================================================
// 8. HTTPS
// =====================================================

app.UseHttpsRedirection();



// =====================================================
// 9. Authentication
// =====================================================

app.UseAuthentication();


// =====================================================
// 10. Authorization
// =====================================================

app.UseAuthorization();


// =====================================================
// 11. Controllers
// =====================================================

app.MapControllers();


// =====================================================
// Run
// =====================================================

app.Run();

builder.Services.AddControllersWithViews();



