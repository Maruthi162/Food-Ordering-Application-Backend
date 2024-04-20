using Microsoft.AspNetCore.Identity;
using System.Net;

namespace Food_Ordering_Application.Models
{
    public class User:IdentityUser
    {
        //user Id, username, email, phone NO, password, values wil be inherited from IdentityUser class and also role as well
        public ICollection<Order> Orders { get; set; }
        public ICollection<Review> Reviews { get; set; }
        public ICollection<Address> Addresses { get; set; } // Collection of addresses for a user
        public ICollection<Payment> Payments { get; set; }

    }
}
