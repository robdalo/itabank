using itabank.Core.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddItaBank();
builder.Services.AddControllers();
builder.Services.AddOpenApi();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();
app.UseRouting();
app.MapControllers();
app.Run();