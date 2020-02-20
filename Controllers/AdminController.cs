using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IndyBooks.Models;
using IndyBooks.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace IndyBooks.Controllers
{
    public class AdminController : Controller
    {
        private IndyBooksDataContext _db;
        public AdminController(IndyBooksDataContext db) { _db = db; }

        public IActionResult Search()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Search(SearchViewModel search)
        {
            IQueryable<Book> foundBooks = _db.Books; // start with entire collection

            //Filter the collection using each non-empty Field as noted
            if (search.Title != null)
            {
                //Filter the collection by Title which "contains" string 
                //(Note: searchBook is the info from the form)
                foundBooks = foundBooks
                             .Where(b => b.Title.Contains(search.Title));
            }

            if (search.LastName != null)
            {
                foundBooks = foundBooks
                             .Where(b => b.Author.EndsWith(search.LastName));
            }

            foundBooks = foundBooks
                            .Where(b => b.Price >= search.MinPrice);
            
            if (search.MaxPrice != 0)
                foundBooks = foundBooks
                            .Where(b => b.Price <= search.MaxPrice);

            if ("title" == search.SortBy)
            {
                foundBooks = foundBooks.OrderBy(b => b.Title);
            } else if ("author" == search.SortBy)
            {
                foundBooks = foundBooks.OrderBy(b => b.Author);
            }

            return View("SearchResults", foundBooks);
        }
    }
}
