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
        public ActionResult Search(SearchVM searchVM)
        {
            IQueryable<Book> foundBooks = _db.Books; // start with entire collection
            

            //Filter the collection using the non-empty Title Field as noted
            if (searchVM.Title != null)
            {
                //Filter the collection by Title which "contains" the given string
                foundBooks = foundBooks
                             .Where(b => b.Title.Contains(searchVM.Title))
                             .OrderBy(book => book.Title);
                // TODO: Order the results by Title
            }
            if(searchVM.LastName != null)
            {
                foundBooks = foundBooks.Where(b => b.Author.EndsWith(searchVM.LastName));
            }

            //TODO: Add similar logic to filter foundbooks collection by last part of the Author's Name, if given
            // (HINT: consider the EndsWith() method, also adjust the Search View and ViewModel to add items)
            if(searchVM.MinPrice != null && searchVM.MaxPrice != null && searchVM.MaxPrice > searchVM.MinPrice)
            {
                foundBooks = foundBooks.Where(b => b.Price >= searchVM.MinPrice)
                                       .Where(b => b.Price <= searchVM.MaxPrice);
                                       //.OrderByDescending(b => b.Price);
            }

            //TODO: Filter the collection by price between a low and high value, if given
            //       order results by descending price 
            // (Note: you will need to adjust the Search ViewModel and View to add search fields)


            //TODO:  Create a projection as a new Book collection with the "Half-Off Sale" Price for books over $90
            //       You only need to include the Title, Author, and sale price in the projection
            if (searchVM.HalfPriceSale)
            {
                var halfOffPrice = foundBooks.Where(b => b.Price >= 90)
                                             .Select(b => new { Title = b.Title, Author = b.Author, Price = b.Price / 2 });

                //foreach(var book in halfOffPrice)
                //{
                //    foundBooks = foundBooks.Where(b => b.Price < 90).Append(book);
                //}
                

            }

            //Pass a SearchResultsVM object to View
            var searchResults = new SearchResultsVM {
                FoundBooks = foundBooks,
                HalfPriceSale = searchVM.HalfPriceSale
            };

            return View("SearchResults", searchResults );
        }
    }
}
