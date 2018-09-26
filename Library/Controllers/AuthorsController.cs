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
      Author newAuthor = Author.Find(id);
      List<Book> allBooks = newAuthor.GetBooks();
      return View(allBooks);
    }

  }
}
