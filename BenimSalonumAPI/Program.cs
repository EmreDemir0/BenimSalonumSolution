using Microsoft.EntityFrameworkCore;
using BenimSalonumAPI.DataAccess.Context;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using BenimSalonum.Tools;
using Microsoft.Extensions.Configuration;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// **Swagger servislerini ekleyelim**
builder.Services.AddControllers();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "BenimSalonum API", Version = "v1" });
});

builder.Services.AddAuthorization();
builder.Services.AddAuthentication();

builder.WebHost.ConfigureKestrel((context, options) =>
{
    options.Configure(context.Configuration.GetSection("Kestrel"));
});

// **🔹 1. `DatabaseSettings:Password` içindeki şifreyi oku**
var encryptedPassword = builder.Configuration["DatabaseSettings:Password"];

if (string.IsNullOrWhiteSpace(encryptedPassword))
{
    Console.WriteLine("[HATA] DatabaseSettings:Password değeri API içinde bulunamadı!");
    throw new InvalidOperationException("Şifre bulunamadı! Lütfen `SetDatabasePassword` ile şifre belirleyin.");
}

Console.WriteLine($"[DEBUG] API İçin Okunan Şifre: {encryptedPassword}");

// **🔹 2. Şifreyi çöz**
string decryptedPassword = AesEncryption.Decrypt(encryptedPassword);

if (string.IsNullOrWhiteSpace(decryptedPassword))
{
    Console.WriteLine("[HATA] Şifre çözme başarısız oldu! Bağlantı dizesi oluşturulamadı.");
    throw new InvalidOperationException("Şifre çözülemedi! Lütfen `SetDatabasePassword` ile şifreyi tekrar belirleyin.");
}

Console.WriteLine($"[DEBUG] Çözülen Şifre: {decryptedPassword}");

// **🔹 3. ConnectionString'i oku ve şifreyi entegre et**
var connectionStringTemplate = builder.Configuration.GetConnectionString("DefaultConnection");

if (string.IsNullOrWhiteSpace(connectionStringTemplate))
{
    Console.WriteLine("[HATA] ConnectionStrings:DefaultConnection bulunamadı!");
    throw new InvalidOperationException("ConnectionStrings:DefaultConnection eksik!");
}

// 🔹 **Bağlantı dizesine şifreyi ekle**
string finalConnectionString = connectionStringTemplate.Replace("ENC(YOUR_ENCRYPTED_PASSWORD_HERE)", decryptedPassword);

Console.WriteLine($"[DEBUG] Güncellenmiş ConnectionString: {finalConnectionString}");

// 🔹 **DbContext'e bağlantıyı tanımla**
builder.Services.AddDbContext<BenimSalonumContext>(options =>
    options.UseSqlServer(finalConnectionString));

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "BenimSalonum API v1");
    });
}

// **Yetkilendirme middleware ekleyelim**
app.UseAuthentication();
app.UseAuthorization();

app.UseHttpsRedirection();
app.MapControllers();

app.Run();
