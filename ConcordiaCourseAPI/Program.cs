using ConcordiaCourseAPI;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<CourseDb>(options => options.UseSqlite("Data Source=courses.db"));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();
var app = builder.Build();

var courses = app.MapGroup("/courses");

courses.MapGet("/", async (CourseDb db)  => await db.Courses.ToListAsync());

app.Run();