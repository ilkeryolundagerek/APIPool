using API01.Data;
using API01.Data.Repositories;
using API01.Services;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers().AddNewtonsoftJson(o =>
{
    o.SerializerSettings.ReferenceLoopHandling=ReferenceLoopHandling.Ignore;
});
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<DataContext>(o => o.UseSqlServer(builder.Configuration.GetConnectionString("data")));
//Eğer UnitOfWork yapısı yazılırsa tüm bu repository yapıları oradan yönetilir, buraya eklemeye gerek kalmaz.
//builder.Services.AddScoped<IPersonRepository,PersonRepository>();
//builder.Services.AddScoped<IAddressRepository,AddressRepository>();
//builder.Services.AddScoped<IDepartmentRepository,DepartmentRepository>();
builder.Services.AddScoped<IUnitOfWorkData, UnitOfWorkData>();
builder.Services.AddScoped<IResponseService, ResponseService>();
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

app.Run();
