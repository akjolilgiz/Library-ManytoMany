using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Library.Models;
using Library;
using System;

namespace Library.Controllers
{
  public class AuthorsController : Controller
  {
    [HttpGet("/authors")]
    public ActionResult Index()
    {
      List<Author> allAuthors = Author.GetAll();
      return View(allAuthors);
    }

    [HttpGet("/authors/{id}/details")]
    public ActionResult Details(int id)
    {
      Dictionary<string, object> model = new Dictionary<string, object> {};
      Author foundAuthor = Author.Find(id);
      List<Book> allAuthorBooks = foundAuthor.GetBooks();
      model.Add("foundAuthor", foundAuthor);
      model.Add("allAuthorBooks", allAuthorBooks);
      return View(model);
    }

    [HttpPost("/authors/{id}/details")]
    public ActionResult AddNewBook(int id)
    {
      Author foundAuthor = Author.Find(id);
      Console.WriteLine(foundAuthor.Name);
      Book newBook = new Book(Request.Form["newBook"]);
      newBook.Save();
      Catalog newCatalog = new Catalog(foundAuthor.Id, newBook.Id);
      newCatalog.Save();
      return RedirectToAction("Details");
    }
  }
}
