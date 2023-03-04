using Microsoft.EntityFrameworkCore;
using IndyBooks;

var builder = WebApplication.CreateBuilder(args);

//Enable MVC and DIJ Services for this application
builder.Services.AddMvc();
//TODO: Initialize the DBC Service for your particular OS
var connection = builder.Configuration.GetConnectionString("IndyBooks-Mac-Sqlite");
builder.Services.AddDbContext<IndyBooks.Models.IndyBooksDataContext>(options =>
    options.UseSqlServer(connection));

var app = builder.Build();


/* Middleware in the HTTP Request Pipeline
 */

if (app.Environment.IsDevelopment())
{
    app.InitializeDb();    //custom extension method to seed the DB
}

app.UseStaticFiles();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller}/{action}",
    defaults: new
    {
        controller = "Admin",
        action = "Search"
    });

app.Run();
