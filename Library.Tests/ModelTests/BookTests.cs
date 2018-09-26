using Microsoft.VisualStudio.TestTools.UnitTesting;
using Library.Models;
using System;

namespace Library.Tests
{
  [TestClass]
  public class BookTests : IDisposable
  {
    public BookTests()
    {
      DBConfiguration.ConnectionString = "server=localhost;user id=root;password=root;port=8889;database=library_test;";
    }
    public void Dispose()
    {
      Book.DeleteAll();
    }
    [TestMethod]
    public void GetAll_ReturnBooksInList_True()
    {
      Book newBook = new Book("Lord of the Rings");
      newBook.Save();

      int result = Book.GetAll().Count;

      Assert.AreEqual(1, result);
    }
    [TestMethod]
    public void Find_ReturnBook_TitleByID_True()
    {
      Book newBook = new Book("Lord of the Rings");
      newBook.Save();

      string result = Book.Find(newBook.Id).Title;

      Assert.AreEqual("Lord of the Rings", result);
    }

  }
}
