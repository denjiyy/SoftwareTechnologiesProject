namespace MusicalShop.Services.Models
{
    using Microsoft.AspNetCore.Identity;
    using MusicalShop.Data.Models;
    using MusicalShop.Services.Mapping;
    using System;
    using System.Collections.Generic;

    public class MusicalShopUserServiceModel : IdentityUser, IMapFrom<MusicalShopUser>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime BirthDate { get; set; }
    }
}