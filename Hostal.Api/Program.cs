using Hostal.Api.Middlewares.ErrorHandlingMiddleware;
using Hostal.Api.Middlewares.RequestLoggingMiddleware;
using Hostal.Api.ServicesExtensions;
using Serilog;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddApiServices(builder.Configuration);
builder.Host.UseSerilog((context, loggerConfiguration) =>
{
    loggerConfiguration.ReadFrom.Configuration(context.Configuration);
});
var app = builder.Build();

/*Implement seed Data for Database*/
/*
var scoped = app.Services.CreateScope();
var seeders = scoped.ServiceProvider.GetRequiredService<IRoomsSeeders>();
await seeders.SeedAsync();
*/

app.UseMiddleware<ErrorHandlingMiddleware>();
app.UseMiddleware<RequestLoggingMiddleware>();

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