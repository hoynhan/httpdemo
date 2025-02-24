using HttpDemo.SessionStorage;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();


builder.Services.AddSingleton<IMySessionStorageEngine>(service =>
{
    var path = Path.Combine(service.GetRequiredService<IHostEnvironment>().ContentRootPath, "sessions");
    Directory.CreateDirectory(path);

    return new FileSessionStorageEngine(path);
});

builder.Services.AddSingleton<IMySessionStorage, MySessionStorage>();   
builder.Services.AddScoped<MySessionScopedContainer>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();


app.Run();

