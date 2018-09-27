using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using Library;

namespace Library.Models
{
  public class Book
  {
    public int Id { get; set; }
    public string Title { get; set; }

    public Book(string title, int id = 0)
    {
      Id = id;
      Title = title;
    }

    public void Save()
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();

      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"INSERT INTO books (title) VALUES (@title);";
      cmd.Parameters.AddWithValue("@title", this.Title);

      cmd.ExecuteNonQuery();
      Id = (int) cmd.LastInsertedId;
      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
    }


    public static Book Find(int id)
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();

      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"SELECT * FROM books WHERE id = @searchId;";
      cmd.Parameters.AddWithValue("@searchId", id);

      var rdr = cmd.ExecuteReader() as MySqlDataReader;
      int foundId = 0;
      string foundTitle = "";
      while (rdr.Read())
      {
        foundId = rdr.GetInt32(0);
        foundTitle = rdr.GetString(1);
      }

      Book foundBook = new Book(foundTitle, foundId);

      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
      return foundBook;
    }

    public static List<Book> GetAll()
    {
      List<Book> allBooks = new List<Book>{};
      MySqlConnection conn = DB.Connection();
      conn.Open();

      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"SELECT * FROM books;";
      var rdr = cmd.ExecuteReader() as MySqlDataReader;
      while(rdr.Read())
      {
        int bookId = rdr.GetInt32(0);
        string bookTitle = rdr.GetString(1);

        Book newBook = new Book(bookTitle, bookId);
        allBooks.Add(newBook);
      }
      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
      return allBooks;
    }

    public static void DeleteAll()
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"DELETE FROM books;";
      cmd.ExecuteNonQuery();
      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
    }
    public void AddAuthor(Author newAuthor)
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"INSERT INTO catalog (author_id, book_id) VALUES (@AuthorId, @BookId);";

      MySqlParameter book_id = new MySqlParameter();
      book_id.ParameterName = "@BookId";
      book_id.Value = Id;
      cmd.Parameters.Add(book_id);

      MySqlParameter Author_id = new MySqlParameter();
      Author_id.ParameterName = "@AuthorId";
      Author_id.Value = newAuthor.Id;
      cmd.Parameters.Add(Author_id);

      cmd.ExecuteNonQuery();
      conn.Close();
      if (conn != null)
      {
          conn.Dispose();
      }
    }
    public List<Author> GetAuthors()
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();
      MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"SELECT authors.* FROM books
      JOIN catalog ON (books.id = catalog.book_id)
      JOIN authors ON (catalog.author_id = authors.id)
      WHERE books.id = @bookId;";

      MySqlParameter booksParameter = new MySqlParameter();
      booksParameter.ParameterName = "@bookId";
      booksParameter.Value = Id;
      cmd.Parameters.Add(booksParameter);

      MySqlDataReader rdr = cmd.ExecuteReader() as MySqlDataReader;
      List<Author> authors = new List<Author>{};

      while(rdr.Read())
      {
        int authorId = rdr.GetInt32(0);
        string authorName = rdr.GetString(1);
        Author newAuthor = new Author(authorName, authorId);
        authors.Add(newAuthor);
      }
      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
      return authors;
    }
    public static List<Book> SearchInBookTable(string bookTitle)
    {
      List<Book> allBooks = new List<Book>{};
      MySqlConnection conn = DB.Connection();
      conn.Open();
      MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;

      cmd.CommandText = @"SELECT * FROM books WHERE title LIKE @searchTitle;";

      cmd.Parameters.AddWithValue("@searchTitle", "%" + bookTitle + "%");

      MySqlDataReader rdr = cmd.ExecuteReader() as MySqlDataReader;

      while (rdr.Read())
      {
        int bookId = rdr.GetInt32(0);
        string booksTitle = rdr.GetString(1);

        Book newBook = new Book (booksTitle, bookId);
        allBooks.Add(newBook);

      }
      conn.Close();
      if (conn !=null)
      {
        conn.Dispose();
      }
      return allBooks;
    }
  }
}
