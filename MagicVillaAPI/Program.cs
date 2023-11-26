
using Serilog;
using MagicVillaAPI.Data;
using Microsoft.EntityFrameworkCore;
using MagicVillaModelAPI.Data;
using MagicVillaAPI;
using MagicVillaAPI.Repository.IRepository;
using MagicVillaAPI.Repository;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

Log.Logger = new LoggerConfiguration().MinimumLevel.Debug()
.WriteTo.File("log/villalog.txt", rollingInterval: RollingInterval.Day).CreateLogger();


builder.Host.UseSerilog();

builder.Services.AddDbContext<ApplictionDbContext>(option =>
{
    option.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});
builder.Services.AddScoped<IVillaRepository, VillaRepository>();

builder.Services.AddAutoMapper(typeof(MappingConfig));

builder.Services.AddControllers(option =>
{
    option.ReturnHttpNotAcceptable = false;
}).AddNewtonsoftJson();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Adding Cros in Web API
builder.Services.AddCors(option => {
    option.AddPolicy("corsapp", builder => {
        builder.WithOrigins("*").AllowAnyMethod().AllowAnyHeader();
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseSwaggerUI(options =>
{
    options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
    options.RoutePrefix = string.Empty;
});
app.UseHttpsRedirection();

app.UseCors("corsapp");

app.UseAuthorization();

app.MapControllers();

app.Run();
