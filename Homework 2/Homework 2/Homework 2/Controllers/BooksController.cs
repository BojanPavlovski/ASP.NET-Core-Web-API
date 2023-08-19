using Homework_2.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using static System.Net.Mime.MediaTypeNames;

namespace Homework_2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //Add new BooksController
    public class BooksController : ControllerBase
    {
        //Add GET method that returns all books
        [HttpGet]
        public ActionResult<List<Book>> GetAllBooks()
        {
            try
            {
                return Ok(StaticDb.Books);
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred,try again.");
            }
        }
        //Add GET method that returns one book by sending index in the query string
        [HttpGet("getBook")]
        public ActionResult<Book> GetBookByIndex(int index) 
        {
            try
            {
                if (index < 0)
                {
                    return BadRequest("The index can not be negative.");
                }
                if (index >= StaticDb.Books.Count)
                {
                    return NotFound("Such a book does not exist.");
                }

                return Ok(StaticDb.Books[index]);
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred,try again.");
            }
        }
        //Add GET method that returns one book by filtering by author and title (use query string parameters)
        [HttpGet("getFilteredBook")]
        public ActionResult<Book> GetFilteredBook(string? author, string? title)
        {
            try
            {
                //if both are null or empty get all books
                if (string.IsNullOrEmpty(author) && string.IsNullOrEmpty(title))
                {
                    return Ok(StaticDb.Books);
                }
                //if author is empty, filter with title
                if (string.IsNullOrEmpty(author))
                {
                    List<Book> bookDb = StaticDb.Books.Where(x => x.Title.ToLower() == title.ToLower()).ToList();
                    return Ok(bookDb);
                }
                //if title is empty, filter with author
                if (string.IsNullOrEmpty(title))
                {
                    List<Book> bookDb = StaticDb.Books.Where(x => x.Author.ToLower() == author.ToLower()).ToList();
                    return Ok(bookDb);
                }

                //if there are values for both title and author
                List<Book> filteredBooks = StaticDb.Books.Where(x => x.Author.ToLower().Contains(author.ToLower())
                 && x.Title.ToLower() == title.ToLower()).ToList();
                return Ok(filteredBooks);
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred,try again.");
            }


        }
        //Add POST method that adds new book to the list of books (use the FromBody attribute)
        [HttpPost]
        public IActionResult AddBook([FromBody] Book book)
        {
            try
            {
                //validations for Book's properties
                if (string.IsNullOrEmpty(book.Author))
                {
                    return BadRequest("Author field can not be empty.");
                }
                if (string.IsNullOrEmpty(book.Title))
                {
                    return BadRequest("Title field can not be empty.");
                }
                //adding to db
                StaticDb.Books.Add(book);
                return StatusCode(StatusCodes.Status201Created);
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred,try again.");
            }
        }
    }
}
