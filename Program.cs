using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using dotNetWedEve.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();

// Auth service
builder.Services.AddSingleton<Model.SimpleAuthService>();

// Use DbContextFactory for proper scoped DbContext in components
builder.Services.AddDbContextFactory<dotNetWedEveContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("FinanceDb")));

builder.Services.AddQuickGridEntityFrameworkAdapter();

var app = builder.Build();

// Migrate and seed database using the DbContextFactory
using (var scope = app.Services.CreateScope())
{
    var dbFactory = scope.ServiceProvider.GetRequiredService<IDbContextFactory<dotNetWedEveContext>>();
    using var db = await dbFactory.CreateDbContextAsync();

    db.Database.Migrate();
    await DbSeeder.Seed(db);
}

// Configure the HTTP request pipeline
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

app.Run();
