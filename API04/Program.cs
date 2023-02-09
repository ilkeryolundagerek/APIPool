using Microsoft.Net.Http.Headers;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers().AddNewtonsoftJson();
builder.Services.AddHttpClient("dummyapi.io", cfg =>
{
    cfg.BaseAddress = new Uri("https://dummyapi.io/data/v1/");
    //DummyAPI.io alt yapısı ihtiyacı olan başlık bilgisini alamazsa '{"error":"APP_ID_MISSING"}' hatasını vermektedir.
    cfg.DefaultRequestHeaders.Add("app-id", "63e4a40e7d733b0ef5fdea65");
});
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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
