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

    [HttpPost("/addBook")]
    public ActionResult AddBook(string title)
    {
      Book newBook = new Book(title);
      newBook.Save();
      return RedirectToAction("Index");
    }


    // [HttpPost("/AddBook")]
    // public ActionResult AddBook(string title)
    // {
    //   Book newBook = new Book(title);
    //   newBook.Save();
    //   return View(newBook);
    // }
  }
}
