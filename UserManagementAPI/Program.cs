using UserManagementAPI.Repositories;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddSingleton<IUserRepository, UserRepository>();
builder.Services.AddControllers();
builder.Services.AddLogging();

var app = builder.Build();

app.UseMiddleware<RequestResponseLoggingMiddleware>();
app.UseMiddleware<ExceptionHandlingMiddleware>();
app.MapControllers();

app.MapGet("/", () => "Hello World!");

app.Run();
