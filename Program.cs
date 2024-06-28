using EmpresaNexer.Data;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddDbContext<EmpresaNexerDataContext>();

var app = builder.Build();


app.MapControllers();
app.Run();
