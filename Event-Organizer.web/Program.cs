using Data.Context;
using Microsoft.EntityFrameworkCore;
using Data.DataAccess;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
//Add Data access injection:
builder.Services.AddScoped<IDataAccess, DataAccess>();

//SQL Server Service Registration
builder.Services.AddDbContext<EventOrganizerDbContext>(
    options =>
        options.UseSqlServer(
            builder.Configuration.GetConnectionString("AzureConnection")));

// Add session support
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30); // Set the session timeout
    options.Cookie.HttpOnly = true; // Make the cookie accessible only to the server
    options.Cookie.IsEssential = true; // Ensure the session cookie is essential
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
	app.UseExceptionHandler("/Error");
	// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
	app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseSession(); // Must be added before app.UseAuthorization()

app.UseAuthorization();

app.MapRazorPages();

app.Run();
