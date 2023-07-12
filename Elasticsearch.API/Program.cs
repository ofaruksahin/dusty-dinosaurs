using Elasticsearch.API.Extensions;
using Elasticsearch.API.Repositories;
using Elasticsearch.API.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddElasticClient(builder.Configuration);
builder.Services.AddScoped<ProductRepository>();
builder.Services.AddScoped<ECommerceRepository>();
builder.Services.AddScoped<ProductService>();
builder.Services.AddScoped<ECommerceService>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
