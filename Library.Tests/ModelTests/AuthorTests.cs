using Microsoft.VisualStudio.TestTools.UnitTesting;
using Library.Models;
using System;

namespace Library.Tests
{
  [TestClass]
  public class AuthorTests : IDisposable
  {
    public AuthorTests()
    {
      DBConfiguration.ConnectionString = "server=localhost;user id=root;password=root;port=8889;database=library_test;";
    }
    public void Dispose()
    {
      Author.DeleteAll();
    }
    [TestMethod]
    public void GetAll_ReturnAuthorsInList_True()
    {
      Author newAuthor = new Author("Lord of the Rings");
      newAuthor.Save();

      int result = Author.GetAll().Count;

      Assert.AreEqual(1, result);
    }
    [TestMethod]
    public void Find_ReturnAuthor_NameByID_True()
    {
      Author newAuthor = new Author("Lord of the Rings");
      newAuthor.Save();

      string result = Author.Find(newAuthor.Id).Name;

      Assert.AreEqual("Lord of the Rings", result);
    }

  }
}
