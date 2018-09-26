using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using Library;

namespace Library.Models
{
  public class Author
  {
    public int Id { get; set; }
    public string Name { get; set; }

    public Author(string name, int id = 0)
    {
      Id = id;
      Name = name;
    }

    public void Save()
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();

      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"INSERT INTO authors (name) VALUES (@name);";
      cmd.Parameters.AddWithValue("@name", this.Name);

      cmd.ExecuteNonQuery();
      Id = (int) cmd.LastInsertedId;
      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
    }

    public static Author Find(int id)
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();

      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"SELECT * FROM authors WHERE id = @searchId;";
      cmd.Parameters.AddWithValue("@searchId", id);

      var rdr = cmd.ExecuteReader() as MySqlDataReader;
      int foundId = 0;
      string foundName = "";
      while (rdr.Read())
      {
        foundId = rdr.GetInt32(0);
        foundName = rdr.GetString(1);
      }

      Author foundAuthor = new Author(foundName, foundId);

      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
      return foundAuthor;
    }

    public static List<Author> GetAll()
    {
      List<Author> allAuthors = new List<Author>{};
      MySqlConnection conn = DB.Connection();
      conn.Open();

      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"SELECT * FROM authors;";
      var rdr = cmd.ExecuteReader() as MySqlDataReader;
      while(rdr.Read())
      {
        int authorId = rdr.GetInt32(0);
        string authorName = rdr.GetString(1);

        Author newAuthor = new Author(authorName, authorId);
        allAuthors.Add(newAuthor);
      }
      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
      return allAuthors;
    }

    public static void DeleteAll()
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"DELETE FROM authors;";
      cmd.ExecuteNonQuery();
      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
    }


  }
}
