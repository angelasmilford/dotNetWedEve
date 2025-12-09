using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using dotNetWedEve.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();

builder.Services.AddSingleton<WeatherForecastService>();
builder.Services.AddSingleton<Model.SimpleAuthService>();

// Register DbContext factory with SQLite
builder.Services.AddDbContextFactory<dotNetWedEveContext>(options =>
    options.UseSqlite("Data Source=finance.db"));

builder.Services.AddQuickGridEntityFrameworkAdapter();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

// Seed database
using (var scope = app.Services.CreateScope())
{
    var factory = scope.ServiceProvider.GetRequiredService<IDbContextFactory<dotNetWedEveContext>>();
    using var db = await factory.CreateDbContextAsync();
    await DbSeeder.Seed(db);
}

app.Run();
