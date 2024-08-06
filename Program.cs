using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using ArticleApi.Data;
using ArticleApi.Services;
using Microsoft.Extensions.Configuration;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<IArticleService, ArticleService>();
builder.Services.AddScoped<IAuthorService, AuthorService>();
builder.Services.AddScoped<ICategoryService, CategoryService>();

builder.Services.AddControllers();

var app = builder.Build();
app.MapControllers();
// app.MapControllerRoute(
//     name:"Article.CreateArticle",
//     pattern:"{controller=Article}/{action=CreateArticle}");
// app.MapControllerRoute(
//     name: "Article.Index",
//     pattern: "{controller=Article}/{action=Index}/{id?}");

app.Run();