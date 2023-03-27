using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using MinimalAPINew;
using MinimalAPINew.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddServices();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();


app.MapGet("/books", async (MinimalDbContext context) =>  await context.Books.ToListAsync());
    
  

app.MapGet("/books/{id}", async (MinimalDbContext context, int id) =>

   await context.Books.FindAsync(id) is Book book ? Results.Ok(book) : Results.NotFound("Book is not found"));





app.MapPost("/books", async (MinimalDbContext context, Book book) =>
{
    context.Books.Add(book);
    await context.SaveChangesAsync();   
    return Results.Ok(await context.Books.ToListAsync());    

});

app.MapPut("/books/{id}", async (MinimalDbContext context, Book updateBook, int id) =>
{

    var book = context.Books.Find(id);
    if (book is null)
    {
        return Results.NotFound("book is not found");
    }

    book.Id = updateBook.Id;
    book.Title = updateBook.Title;
    book.Author = updateBook.Author;

    await context.SaveChangesAsync();

    return Results.Ok(await context.Books.ToListAsync());



});


app.MapDelete("/books/{id}", async (MinimalDbContext context, int id) =>
{
    var book = await context.Books.FindAsync(id);
    if (book is null) return Results.NotFound("book is not found");

    context.Books.Remove(book);
    await context.SaveChangesAsync();
    return Results.Ok(await context.Books.ToListAsync());
}); 




app.Run();

