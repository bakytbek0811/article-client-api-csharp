using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using ArticleApi.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDatabase(builder.Configuration);
builder.Services.AddApplicationServices();
builder.Services.AddControllers();

var app = builder.Build();
app.MapControllers();

app.Run();