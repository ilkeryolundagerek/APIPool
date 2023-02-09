using APICompute.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSingleton(new SimpleComputeRepository());

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapGet("/hello/{your_name}", async (string your_name, SimpleComputeRepository sRepo) => await sRepo.HelloWorldAsync(your_name));

app.MapGet("/ToFahrenheit/{c}", async (double c, SimpleComputeRepository sRepo) => await sRepo.CelciusToFahrenheitAsync(c));

app.Run();