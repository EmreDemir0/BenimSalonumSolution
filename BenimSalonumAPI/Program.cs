using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.Extensions.Configuration;
using Microsoft.OpenApi.Models;
using Microsoft.Data.SqlClient;
using BenimSalonumAPI.DataAccess.Context;
using BenimSalonum.Tools;
using BenimSalonum.API.Extensions;
using BenimSalonum.DataAccess.SeedData;
using System;
using BenimSalonum.Entities.Interfaces;
using BenimSalonum.Entities.Tables;
using BenimSalonumAPI.DataAccess;

var builder = WebApplication.CreateBuilder(args);

// 🔹 **Swagger Servislerini ve API Tanımlamalarını Ekleyelim**
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "BenimSalonum API", Version = "v1" });
});

// 📌 Repository'yi Dependency Injection'a Ekle
builder.Services.AddScoped(typeof(IRepository<>), typeof(DataAccess<>));

// 🔹 **Yetkilendirme & Kimlik Doğrulama**
builder.Services.AddAuthorization();
builder.Services.AddAuthentication();

// 🔹 **Kestrel Yapılandırması**
builder.WebHost.ConfigureKestrel((context, options) =>
{
    options.Configure(context.Configuration.GetSection("Kestrel"));
});

// 🔹 **1️⃣ Şifreyi Oku & Çöz**
var encryptedPassword = builder.Configuration["DatabaseSettings:Password"]
    ?? throw new InvalidOperationException("Şifre bulunamadı! Lütfen `SetDatabasePassword` ile şifre belirleyin.");

var decryptedPassword = AesEncryption.Decrypt(encryptedPassword)
    ?? throw new InvalidOperationException("Şifre çözülemedi! Lütfen `SetDatabasePassword` ile şifreyi tekrar belirleyin.");

// 🔹 **2️⃣ Connection String'i Oku & Güncelle**
var connectionStringTemplate = builder.Configuration.GetConnectionString("DefaultConnection")
    ?? throw new InvalidOperationException("ConnectionStrings:DefaultConnection eksik!");

string finalConnectionString = connectionStringTemplate.Replace("ENC(YOUR_ENCRYPTED_PASSWORD_HERE)", decryptedPassword);

// 🔹 **3️⃣ DbContext Konfigurasyonu**
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
        Console.WriteLine("✅ TEST VERİLERİ YÜKLENDİ - BAŞARILI ");
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
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

// 🔹 **API'yi Başlat**
app.Run();