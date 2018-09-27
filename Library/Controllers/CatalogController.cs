using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Library.Models;
using Library;
using System;

namespace Library.Controllers
{
  public class CatalogController : Controller
  {
    [HttpGet("/catalog")]
    public ActionResult Index()
    {
      return View();
    }
    //
    // [HttpPost("/addBook")]
    // public ActionResult AddBook(string title)
    // {
    //   Book newBook = new Book(title);
    //   newBook.Save();
    //   return RedirectToAction("Index");
    // }

    [HttpPost("/addCatalog")]
    public ActionResult AddCatalog()
    {
      Book newBook = new Book(Request.Form["title"]);
      newBook.Save();
      Author newAuthor = new Author(Request.Form["name"]);
      newAuthor.Save();
      Catalog newCatalog = new Catalog(newAuthor.Id, newBook.Id);
      newCatalog.Save();
      return RedirectToAction("Index");
    }

    // [HttpGet("/{id}/addAuthor")]
    // public ActionResult AddAuthor(int id)
    // {
    //   Book foundBook = Book.Find(id);
    //   return View(foundBook);
    // }

    //
    // [HttpPost("/authors/{id}/details")]
    // public ActionResult AddNewAuthor(int id)
    // {
    //   Book foundBook = Book.Find(id);
    //   Console.WriteLine(foundBook.Title);
    //   Author newAuthor = new Author(Request.Form["newAuthor"]);
    //   newAuthor.Save();
    //   Catalog newCatalog = new Catalog(newAuthor.Id, foundBook.Id);
    //   newCatalog.Save();
    //   return RedirectToAction("Details");
    // }


    // [HttpPost("/AddBook")]
    // public ActionResult AddBook(string title)
    // {
    //   Book newBook = new Book(title);
    //   newBook.Save();
    //   return View(newBook);
    // }
  }
}
