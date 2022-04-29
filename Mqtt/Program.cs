using Mqtt.Services;
using Mqtt.Models;

var builder = WebApplication.CreateBuilder(args);

MqttService.Instance.Build();
var _ = MqttService.Instance.Start();

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddDbContext<MqttContext>();
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

