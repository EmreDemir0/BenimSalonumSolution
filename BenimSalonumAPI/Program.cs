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

Console.WriteLine("🚀 API Başlatılıyor...");

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

Console.WriteLine("✅ CORS ayarlandı!");

// 🔹 **Swagger Servislerini Ekleyelim**
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.PropertyNameCaseInsensitive = true;
    });

builder.Services.AddScoped(typeof(IRepository<>), typeof(DataAccess<>));
builder.Services.AddScoped<JwtTokenService>();
builder.Services.AddScoped<TokenService>(); // **Eksik olan TokenService eklendi**
builder.Services.AddScoped<RefreshTokenRepository>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "BenimSalonum API", Version = "v1" });

    // ✅ Swagger’a JWT Authentication ekleyelim
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "JWT Token'ınızı 'Bearer {token}' formatında girin",
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer"
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement
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
            new string[] {}
        }
    });
});

Console.WriteLine("✅ Swagger yapılandırıldı!");

// 🔹 **JWT Authentication Middleware'i Ekle**
builder.Services.AddJwtAuthentication(builder.Configuration);
builder.Services.AddAuthorization();

// 🔹 **Kestrel Yapılandırması**
builder.WebHost.ConfigureKestrel((context, options) =>
{
    options.Configure(context.Configuration.GetSection("Kestrel"));
});

// 🔹 **1. Şifreyi Oku & Çöz**
var encryptedPassword = builder.Configuration["DatabaseSettings:Password"]
    ?? throw new InvalidOperationException("🔴 Şifre bulunamadı! Lütfen SetDatabasePassword ile şifre belirleyin.");

var decryptedPassword = AesEncryption.Decrypt(encryptedPassword)
    ?? throw new InvalidOperationException("🔴 Şifre çözülemedi! Lütfen SetDatabasePassword ile şifreyi tekrar belirleyin.");

Console.WriteLine("✅ Şifre başarıyla çözüldü!");

// 🔹 **2. Connection String'i Oku & Güncelle**
var connectionStringTemplate = builder.Configuration.GetConnectionString("DefaultConnection")
    ?? throw new InvalidOperationException("🔴 ConnectionStrings:DefaultConnection eksik!");

string finalConnectionString = connectionStringTemplate.Replace("ENC(YOUR_ENCRYPTED_PASSWORD_HERE)", decryptedPassword);

// 🔹 **3. DbContext Konfigurasyonu**
builder.Services.AddDbContext<BenimSalonumContext>(options =>
    options.UseSqlServer(finalConnectionString));

Console.WriteLine("✅ Veritabanı bağlantısı için DbContext ayarlandı!");

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

// 🔹 **Veritabanı Migrasyonu Çalıştır**
try
{
    app.MigrateDatabase();
    Console.WriteLine("✅ Veritabanı migrasyonu başarıyla tamamlandı.");
}
catch (Exception ex)
{
    Console.WriteLine($"❌ Veritabanı migrasyonu başarısız: {ex.Message}");
}

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
        c.InjectJavascript("/swagger-ui/custom.js"); // ✅ Swagger'da 'Authorize' butonunu vurgulamak için
    });
}

// 🔹 **Middleware'ler**
app.UseRouting();

// 🔹 **CORS'u aktif et**
Console.WriteLine("✅ CORS aktif ediliyor...");
app.UseCors("AllowAll");

app.UseAuthentication();
app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});

// 🔹 **API'yi Başlat**
Console.WriteLine("🚀 API çalışmaya başladı...");
app.Run();
