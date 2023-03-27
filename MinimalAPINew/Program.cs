using Microsoft.AspNetCore.Http.HttpResults;
using MinimalAPINew.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();


var books = new List<Book>
{
    new () {Id=1,Author="Harper Lee",Title="Bülbülü Öldürmek" },
    new () {Id=2,Author="George Orwell",Title="1984" },
    new () {Id=3,Author="John Steinbeck",Title="Fareler ve Ýnsanlar" },
    new () {Id=4,Author="Franz Kafka",Title="Dönüþüm" },
    new () {Id=5,Author="Ray Bradbury",Title="Fahrenheit 451" },
    
};

app.MapGet("/books", () => { return books; });

app.MapGet("/books/{id}", (int id) =>
{
   
   var book = books.Find(b => b.Id == id);

    if (book is null)
        return Results.NotFound("Book is not found");
   return Results.Ok(book);
    
});


app.MapPost("/books", (Book book) =>
{
    books.Add(book);
    return Results.Ok(book);
});

app.MapPut("/books/{id}", (Book book,int id) =>
{
    var model = books.Find(b=>b.Id == id);

    if (model is null)
        return Results.NotFound("book is not found");

    model.Author = book.Author;
    model.Title = book.Title;
    model.Id = book.Id;
    return Results.Ok(book);

});


app.MapDelete("/books/{id}", (int id) =>
{
    var book = books.Find(b=> b.Id == id); 
    if(book is null)
        return Results.NotFound("book is not found");  
    books.Remove(book);
    return Results.Ok(books);    
});



app.Run();

