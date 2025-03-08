using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.Extensions.Configuration;
using Microsoft.OpenApi.Models;
using Microsoft.Data.SqlClient;
using BenimSalonumAPI.DataAccess.Context;
using BenimSalonum.Tools;
using BenimSalonum.API.Extensions;
using BenimSalonum.DataAccess.SeedData;
using BenimSalonumAPI.DataAccess.Services;
using System;
using BenimSalonum.Entities.Interfaces;
using BenimSalonum.Entities.Tables;
using BenimSalonumAPI.DataAccess;
using BenimSalonumAPI.DataAccess.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using BenimSalonumAPI;

var builder = WebApplication.CreateBuilder(args);

// 🔹 **CORS Yapılandırmasını Ekleyelim**
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

// 🔹 **Swagger Servislerini Ekleyelim**
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.PropertyNameCaseInsensitive = true;
    });

builder.Services.AddScoped(typeof(IRepository<>), typeof(DataAccess<>));
builder.Services.AddScoped<JwtTokenService>();
builder.Services.AddScoped<TokenService>(); // **Eksik olan TokenService eklendi**
builder.Services.AddScoped<RefreshTokenRepository>(); // **Eksik olan RefreshTokenRepository eklendi**
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "BenimSalonum API", Version = "v1" });
});

// 🔹 **Yetkilendirme & Kimlik Doğrulama**
var jwtKey = builder.Configuration["Jwt:Secret"];
if (string.IsNullOrEmpty(jwtKey))
{
    throw new InvalidOperationException("JWT Secret key tanımlanmamış! Lütfen appsettings.json içine ekleyin.");
}

var key = Encoding.UTF8.GetBytes(jwtKey);

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.RequireHttpsMetadata = false;
        options.SaveToken = true;
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(key),
            ValidateIssuer = false,
            ValidateAudience = false,
            RequireExpirationTime = true,
            ValidateLifetime = true,
            ClockSkew = TimeSpan.Zero
        };
    });

builder.Services.AddAuthorization();

// 🔹 **Kestrel Yapılandırması**
builder.WebHost.ConfigureKestrel((context, options) =>
{
    options.Configure(context.Configuration.GetSection("Kestrel"));
});

// 🔹 **1. Şifreyi Oku & Çöz**
var encryptedPassword = builder.Configuration["DatabaseSettings:Password"]
    ?? throw new InvalidOperationException("Şifre bulunamadı! Lütfen SetDatabasePassword ile şifre belirleyin.");

var decryptedPassword = AesEncryption.Decrypt(encryptedPassword)
    ?? throw new InvalidOperationException("Şifre çözülemedi! Lütfen SetDatabasePassword ile şifreyi tekrar belirleyin.");

// 🔹 **2. Connection String'i Oku & Güncelle**
var connectionStringTemplate = builder.Configuration.GetConnectionString("DefaultConnection")
    ?? throw new InvalidOperationException("ConnectionStrings:DefaultConnection eksik!");

string finalConnectionString = connectionStringTemplate.Replace("ENC(YOUR_ENCRYPTED_PASSWORD_HERE)", decryptedPassword);

// 🔹 **3. DbContext Konfigurasyonu**
builder.Services.AddDbContext<BenimSalonumContext>(options =>
    options.UseSqlServer(finalConnectionString));

// 🔹 **Bağlantıyı Test Et**
try
{
    using var connection = new SqlConnection(finalConnectionString);
    connection.Open();
    Console.WriteLine("✅ Veritabanı bağlantısı başarılı!");
}
catch (Exception ex)
{
    Console.WriteLine($"❌ Veritabanı bağlantısı başarısız: {ex.Message}");
}

// 🔹 **Uygulama Oluştur & Middleware'leri Ekle**
var app = builder.Build();
app.MigrateDatabase();

// 🔹 **✅ TrialData'yı Çağırarak Test Verilerini Yükle**
using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<BenimSalonumContext>();
    try
    {
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine("✅ TEST VERİLERİ YÜKLENİYOR..");
        Console.ResetColor();
        await TrialData.SeedAsync(dbContext);
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine("✅ TEST VERİLERİ YÜKLEDİ - BAŞARILI ");
        Console.ResetColor();
    }
    catch (Exception ex)
    {
        Console.WriteLine($"❌ Test verileri yüklenirken hata oluştu: {ex.Message}");
    }
}

// 🔹 **Swagger UI Kullanımı**
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "BenimSalonum API v1");
    });
}

// 🔹 **Middleware'ler**
//app.UseHttpsRedirection();

app.UseAuthorization();
app.UseRouting();
app.UseAuthentication();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});

// 🔹 **CORS'u aktif et**
app.UseCors("AllowAll");

// 🔹 **API'yi Başlat**
app.Run();
