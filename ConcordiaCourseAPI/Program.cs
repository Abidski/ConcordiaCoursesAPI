using ConcordiaCourseAPI;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<CourseDb>(options => options.UseSqlite("Data Source=courses.db"));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();
var app = builder.Build();

var courses = app.MapGroup("/courses");

courses.MapGet("/", async (CourseDb db)  =>
{
    var result = db.Courses.ToListAsync();
    return result;
});

courses.MapGet("/{id}", async (CourseDb db, int id)  =>
{
   var result = await db.Courses.FindAsync(id);
   if (result == null)
   {
       return Results.NotFound();
   }

   return Results.Ok(result);
});

courses.MapPost("/", async (Course course, CourseDb db) =>
{

    db.Courses.Add(course);
    await db.SaveChangesAsync();

    return Results.Created($"/courses/{course.Id}", course);
});

courses.MapPut("/{id}", async (Course course, CourseDb db) =>
{
    var result = await db.Courses.FindAsync(course.Id);

    if (result == null)
    {
        return Results.NotFound();
    }

    result.Department = course.Department;
    result.Code = course.Code;
    result.Location = course.Location;

    await db.SaveChangesAsync();

    return Results.Ok(course);

});

courses.MapDelete("/{id}" , async (CourseDb db) => {
    
    var result = await db.Courses.FindAsync(course.Id);

    if (result == null)
    {
        return Results.NotFound();
    }

    db.Remove(result);
    await db.SaveChangesAsync();
    
    return Results.Ok("Course deleted");

});

app.Run();