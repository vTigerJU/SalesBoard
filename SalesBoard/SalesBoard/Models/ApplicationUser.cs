using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace SalesBoard.Models
{
    public class ApplicationUser : IdentityUser
    {
        [PersonalData]
        public string? Name { get; set; }
        [PersonalData]
        public DateTime DOB {  get; set; }

        public List<Item>? Item { get; set; }

        public List<Customer>? Customers { get; set; }
    }
}
