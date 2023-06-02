using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel;
using MvcCoreDynamoDb.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
AmazonDynamoDBClient dynamoDBClient = new AmazonDynamoDBClient();
DynamoDBContext dynamoDBContext = new DynamoDBContext(dynamoDBClient);

builder.Services.AddTransient <DynamoDBContext>(x => dynamoDBContext);
builder.Services.AddTransient<ServiceDynamoDb>();
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
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
