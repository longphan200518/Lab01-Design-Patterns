using Lab01.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// Register Singleton Pattern - Logger Service (BÀI 1)
builder.Services.AddSingleton<ILoggerService>(provider => LoggerService.Instance);

builder.Services.AddControllers();
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

// Comment this line to allow HTTP (no SSL certificate needed)
// app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
