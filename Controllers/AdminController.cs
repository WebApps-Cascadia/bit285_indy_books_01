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
                // TODO: order the results by Title
                foundBooks = foundBooks
                             .Where(b => b.Title.Contains(search.Title))
                             .OrderBy(b => b.Title);
                if (search.Author != null)
                {
                    foundBooks = AuthorResult(search, foundBooks);
                }
                    foundBooks = PriceResult(search, foundBooks);
            }

            //TODO: Add logic to filter the collection by last part of the Author's Name, if given
            // (HINT: consider the EndsWith() method, also you will need to adjust the View and ViewModel)

            else if (search.Author != null)
            {
                foundBooks = AuthorResult(search, foundBooks);
                
                foundBooks = PriceResult(search, foundBooks);
            }


            //TODO: Filter the collection by price between a low and high value, if given
            //       order results by descending price 
            // (Note: you will need to adjust the ViewModel and View to add search fields)

                foundBooks = PriceResult(search, foundBooks);

            return View("SearchResults", foundBooks);
        }

        public IQueryable<Book> AuthorResult(SearchViewModel search, IQueryable<Book> foundBooks) 
        {
            IQueryable<Book> f = foundBooks
                    .Where(b => b.Author.EndsWith(search.Author));
            return f;
        }

        public IQueryable<Book> PriceResult(SearchViewModel search, IQueryable<Book> foundBooks)
        {
            IQueryable<Book> f = foundBooks;

            if (search.MaxPrice == 0 && search.MinPrice != 0)
            {
                f = f.Where(b => b.Price >= search.MinPrice)
                               .OrderByDescending(b => b.Price);
            }
            else if (search.MinPrice == 0 && search.MaxPrice != 0)
            {
                f = f.Where(b => b.Price <= search.MaxPrice)
                               .OrderByDescending(b => b.Price);
            }
            else if (search.MinPrice != 0 && search.MaxPrice != 0)
            {
                f = f.Where(b => b.Price >= search.MinPrice && b.Price <= search.MaxPrice)
                    .OrderByDescending(b => b.Price);
            }
            return f;
        }
    }
}
