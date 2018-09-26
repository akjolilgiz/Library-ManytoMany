using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using Library;

namespace Library.Models
{
  public class Catalog
  {
    public int Id{get; set; }
    public int Author_Id{get; set; }
    public int Book_Id{get; set; }

    public Catalog(int author_id, int book_id, int id = 0)
    {
      Id = id;
      Author_Id = author_id;
      Book_Id = book_id;
    }
    public void Save()
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();

      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"INSERT INTO catalog (author_id, book_id) VALUES (@author_id, @book_id);";
      cmd.Parameters.AddWithValue("@author_id", this.Author_Id);
      cmd.Parameters.AddWithValue("@book_id", this.Book_Id);

      cmd.ExecuteNonQuery();
      Id = (int) cmd.LastInsertedId;
      conn.Close();
      if (conn !=null)
      {
        conn.Dispose();
      }
    }
    public static List<Catalog> GetAll()
    {
      List<Catalog> allCatalogs = new List<Catalog> {};
      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"SELECT * FROM catalog;";
      var rdr = cmd.ExecuteReader() as MySqlDataReader;
      while (rdr.Read())
      {
        int catalogId = rdr.GetInt32(0);
        int catalogAuthor_id = rdr.GetInt32(1);
        int catalogBook_id = rdr.GetInt32(2);

        Catalog newCatalog = new Catalog(catalogAuthor_id, catalogBook_id, catalogId);
        allCatalogs.Add(newCatalog);
      }
      conn.Close();
        if (conn != null)
        {
          conn.Dispose();
        }
        return allCatalogs;
    }
    public static void DeleteAll()
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"DELETE FROM catalog;";
      cmd.ExecuteNonQuery();
      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
    }


  }
}
