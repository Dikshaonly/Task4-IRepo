using Task4.Repository;
using Microsoft.EntityFrameworkCore.SqlServer;
using Microsoft.Data.SqlClient;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connstr = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddSingleton(connstr);

builder.Services.AddControllersWithViews();

builder.Services.AddScoped<IEmployeeRepository, EmployeeRepository>();

builder.Services.AddScoped<IDepartmentRepository, DepartmentRepository>();

builder.Services.AddScoped<IErrorRepository, ErrorRepository>();



var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{

    app.UseExceptionHandler("/Home/Error");

    //app.UseDeveloperExceptionPage();
  
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
