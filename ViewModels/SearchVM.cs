using System;
using System.ComponentModel.DataAnnotations;
namespace IndyBooks.ViewModels
{
    public class SearchVM
    {
        [Display(Name = "Title to Find: ")]
        public String Title { get; set; }

        //TODO: Add properties and Display annotation needed for searching
        [Display(Name= "Author:")]
        public String Author { get; set; }

        [Display(Name = "Minimum Price")]
        public String MinPrice { get; set; }

        [Display(Name = "Max Price")]
        public String MaxPrice { get; set; }

        [Display(Name = "Half-Price Sale: ")]
        public Boolean HalfPriceSale { get; set; }
    }
}
