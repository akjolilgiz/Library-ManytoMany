using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Library.Models;
using Library;
using System;

namespace Library.Controllers
{
  public class BooksController : Controller
  {
    [HttpGet("/books")]
    public ActionResult Index()
    {
      List<Book> allBooks = Book.GetAll();
      return View(allBooks);
    }

    [HttpGet("/books/{id}/details")]
    public ActionResult Details(int id)
    {
      Dictionary<string, object> model = new Dictionary<string, object> {};
      Book newBook = Book.Find(id);
      List<Author> allAuthors = newBook.GetAuthors();
      model.Add("newBook", newBook);
      model.Add("allAuthors", allAuthors);
      return View(model);
    }

    [HttpPost("/books/{id}/details")]
    public ActionResult AddNewAuthor(int id)
    {
      Book foundBook = Book.Find(id);
      Console.WriteLine(foundBook.Title);
      Author newAuthor = new Author(Request.Form["newAuthor"]);
      newAuthor.Save();
      Catalog newCatalog = new Catalog(newAuthor.Id, foundBook.Id);
      newCatalog.Save();
      return RedirectToAction("Details");
    }

    [HttpPost("/books/search")]
    public ActionResult Search()
    {
      string searchedTitle = Request.Form["searchedBook"];
      List<Book> foundBooks = Book.SearchInBookTable(searchedTitle);
      Console.WriteLine(foundBooks);
      return View(foundBooks);

    }
  }
}
