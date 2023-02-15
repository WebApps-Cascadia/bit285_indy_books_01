using System;
using System.ComponentModel.DataAnnotations;
namespace IndyBooks.ViewModels
{
    public class SearchVM
    {
        [Display(Name = "Title to Find: ")]
        public String Title { get; set; }

        //TODO: Add properties and Display annotation needed for searching
        [Display(Name = "Author Last Name: ")]
        public string LastName { get; set; }

        [Display(Name = "Min Price: ")]
        public decimal MinPrice { get; set; }

        [Display(Name = "Max Price: ")]
        public decimal MaxPrice { get; set; }

        [Display(Name = "Half-Price Sale: ")]
        public Boolean HalfPriceSale { get; set; }
    }
}
