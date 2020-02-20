using System;
namespace IndyBooks.ViewModels
{
    public class SearchViewModel
    {
        public String Title { get; set; }
        public string LastName { get; set; }
        public decimal MinPrice { get; set; }
        public decimal MaxPrice { get; set; }

        public string SortBy { get; set; }

        //TODO: Add properties needed for searching
    }
}
