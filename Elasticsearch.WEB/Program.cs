using Elasticsearch.WEB.Extensions;
using Elasticsearch.WEB.Repositories;
using Elasticsearch.WEB.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();
builder.Services.AddElasticClient(builder.Configuration);
builder.Services.AddScoped<BlogService>();
builder.Services.AddScoped<BlogRepository>();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
