using Microsoft.VisualStudio.TestTools.UnitTesting;
using Library.Models;
using System;

namespace Library.Tests
{
  [TestClass]
  public class CatalogTests : IDisposable
  {
    public CatalogTests()
    {
      DBConfiguration.ConnectionString = "server=localhost;user id=root;password=root;port=8889;database=library_test;";
    }
    public void Dispose()
    {
      Catalog.DeleteAll();
    }
    [TestMethod]
    public void GetAll_ReturnCatalogsInList_True()
    {
      Catalog newCatalog = new Catalog(1, 2);
      newCatalog.Save();

      int result = Catalog.GetAll().Count;

      Assert.AreEqual(1, result);
    }
  }
}
