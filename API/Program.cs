using BLL;
using BLL.Interfaces;
using DAL;
using DAL.Helper;
using DAL.Helper.Interfaces;
using DAL.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// ============ Cấu hình JWT Authentication ============
string secretKey = builder.Configuration["AppSettings:Secret"]
    ?? throw new InvalidOperationException("Thiếu cấu hình AppSettings:Secret trong appsettings.json");

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey)),
        ValidateIssuer = false,
        ValidateAudience = false,
        ClockSkew = TimeSpan.Zero
    };
});
builder.Services.AddAuthorization();

// ============ DAL Helper ============
builder.Services.AddScoped<IDatabaseHelper, DatabaseHelper>();

// ============ DAL Repositories ============
builder.Services.AddScoped<INguoiDungRepository, NguoiDungRepository>();
builder.Services.AddScoped<ICongTyRepository, CongTyRepository>();
builder.Services.AddScoped<IKyNangRepository, KyNangRepository>();
builder.Services.AddScoped<ITinTuyenDungRepository, TinTuyenDungRepository>();
builder.Services.AddScoped<IHoSoRepository, HoSoRepository>();
builder.Services.AddScoped<IDonUngTuyenRepository, DonUngTuyenRepository>();
builder.Services.AddScoped<ILichPhongVanRepository, LichPhongVanRepository>();
builder.Services.AddScoped<IDeNghiTuyenDungRepository, DeNghiTuyenDungRepository>();
builder.Services.AddScoped<IThongBaoRepository, ThongBaoRepository>();
builder.Services.AddScoped<IBaoCaoRepository, BaoCaoRepository>();

// ============ BLL Business ============
builder.Services.AddScoped<INguoiDungBusiness, NguoiDungBusiness>();
builder.Services.AddScoped<ICongTyBusiness, CongTyBusiness>();
builder.Services.AddScoped<IKyNangBusiness, KyNangBusiness>();
builder.Services.AddScoped<ITinTuyenDungBusiness, TinTuyenDungBusiness>();
builder.Services.AddScoped<IHoSoBusiness, HoSoBusiness>();
builder.Services.AddScoped<IDonUngTuyenBusiness, DonUngTuyenBusiness>();
builder.Services.AddScoped<ILichPhongVanBusiness, LichPhongVanBusiness>();
builder.Services.AddScoped<IDeNghiTuyenDungBusiness, DeNghiTuyenDungBusiness>();
builder.Services.AddScoped<IThongBaoBusiness, ThongBaoBusiness>();
builder.Services.AddScoped<IBaoCaoBusiness, BaoCaoBusiness>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
