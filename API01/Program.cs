using API01.Data;
using API01.Data.Repositories;
using API01.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Net.Http.Headers;
using Newtonsoft.Json;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers().AddNewtonsoftJson(o =>
{
    o.SerializerSettings.ReferenceLoopHandling=ReferenceLoopHandling.Ignore;
});

builder.Services.AddCors(o =>
{
    o.AddDefaultPolicy(p =>
    {
        //Eğitim sırasında testlerde kullanma amacıyla ekledim (ilker turan)
        p.SetIsOriginAllowed(orj => true)//Servis orjini izinlere açık demektir.
       .AllowAnyOrigin() //Herhangi bir orjinden gelen talebi kabul et.
       .AllowAnyMethod() //Gelen talebin tüm metot modellerini kabul et. (get,post,put,delete)
       .AllowAnyHeader(); //Gelen talebin tüm header bilgilerini kabul et.

        //p.WithOrigins("http://example.com", "http://www.contoso.com").WithMethods("GET", "PUT").WithHeaders(HeaderNames.ContentType, "client-secret-key").SetIsOriginAllowedToAllowWildcardSubdomains();
    });
    o.AddPolicy(name: "MyCors", p =>
    {
        p
        //.SetIsOriginAllowed(orj => true)//Servis orjini izinlere açık demektir.
        //.AllowAnyOrigin() //Herhangi bir orjinden gelen talebi kabul et.
        .WithOrigins("http://example.com", "http://www.contoso.com") //Belirtilen orjinlerden gelen talepleri kabul et.
        .AllowAnyMethod() //Gelen talebin tüm metot modellerini kabul et. (get,post,put,delete)
        .AllowAnyHeader() //Gelen talebin tüm header bilgilerini kabul et.

        ;
    });
    //Values Controller için hazırlandı.
    o.AddPolicy(name: "Values", p =>
    {
        p.SetIsOriginAllowed(orj => true)//Servis orjini izinlere açık demektir.
        .AllowAnyOrigin() //Herhangi bir orjinden gelen talebi kabul et.
        .AllowAnyMethod() //Gelen talebin tüm metot modellerini kabul et. (get,post,put,delete)
        .AllowAnyHeader(); //Gelen talebin tüm header bilgilerini kabul et.
    });
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

//AddDefaultPolicy: Policy bu şekilde eklenirse isim vermemize gerek yoktur.
app.UseCors();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

//app.UseRouting();

app.UseAuthorization();

//Spesifik Cors için rota yapısı detaylandırılmalı
app.MapControllers();

//Her rota için olan bu yapı dışında bütün yapı için ortak cors sistemi daha sağlıklıdır.
//app.UseEndpoints(endpoints =>
//{
//    endpoints.MapGet("/api/values", ctx => ctx.Response.WriteAsync("")).RequireCors("Values");
//});

app.Run();
