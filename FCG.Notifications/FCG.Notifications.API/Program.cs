using FCG.Notifications.Application.Common.Ports;
using FCG.Notifications.Application.Notifications.UseCases.Commands.SendWelcomeEmail;
using FCG.Notifications.Infrastructure.Messaging.Bus;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddMassTransitConfiguration(builder.Configuration);
builder.Services.AddScoped<ICommandHandler<SendWelcomeEmailCommand>, SendWelcomeEmailCommandHandler>();
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Notifications API V1");
});

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
