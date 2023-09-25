using Tailwind.CMS.Data;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors();
builder.Services.AddAuthentication().AddJwtBearer();
builder.Services.AddAuthorization();

var app = builder.Build();

app.UseStaticFiles();
app.MapFallbackToFile("index.html");

var libPath = Path.Combine(app.Environment.WebRootPath, "Content");
var contentLibrary = new ContentLibrary(libPath).Load();

app.UseCors(builder => builder
 .AllowAnyOrigin()
 .AllowAnyMethod()
 .AllowAnyHeader()
);

app.UseAuthentication();
app.UseAuthorization();

//load the routes
Tailwind.CMS.Api.Content.MapRoutes(app, contentLibrary);
app.Run();

//this is for tests
public partial class Program { }