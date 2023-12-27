using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using AdvancedEshop.Web.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace AdvancedEshop.Web.Models
{
    public class Order
    {
        [BindNever]
        public int OrderID { get; set; }

        [BindNever]
        public ICollection<CartLine> Lines { get; set; } = new List<CartLine>();

        [Required(ErrorMessage = "Please enter your first name")]
        public string? FirstName { get; set; }

        [Required(ErrorMessage = "Please enter your last name")]
        public string? LastName { get; set; }

        [Required(ErrorMessage = "Please enter your email")]
        [EmailAddress(ErrorMessage = "Invalid email address")]
        public string? Email { get; set; }

        [Required(ErrorMessage = "Please enter your mobile number")]
        public string? MobileNumber { get; set; }

        [Required(ErrorMessage = "Please enter your address line 1")]
        public string? AddressLine1 { get; set; }

        public string? AddressLine2 { get; set; }

        [Required(ErrorMessage = "Please enter your country")]
        public string? Country { get; set; }

        [Required(ErrorMessage = "Please enter your city")]
        public string? City { get; set; }

        [Required(ErrorMessage = "Please enter your state")]
        public string? State { get; set; }

        public string? ZipCode { get; set; }

        /*public bool CreateAccount { get; set; }*/
        [BindNever]
        public bool Shipped { get; set; }



        /*[NotMapped]
        public Cart Cart { get; set; }*/

        // Add any additional properties you may need

        // Constructor
        /*public Order()
        {
            // Initialize the Lines collection
            Lines = new List<CartLine>();

        }*/
    }
}
