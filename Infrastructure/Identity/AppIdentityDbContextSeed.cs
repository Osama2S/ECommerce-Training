using Core.Entities.Identity;
using Microsoft.AspNetCore.Identity;

namespace Infrastructure.Identity
{
    public class AppIdentityDbContextSeed
    {
        public static async Task SeedUsersAsyc(UserManager<AppUser> userManager)
        {
            if(!userManager.Users.Any())
            {
                var user = new AppUser
                {
                    DisplayName="Bob",
                    Email="BobTest@.com",
                    UserName="BobTest",
                    Address=new Address
                    {
                        FirstName="Bob",
                        LastName="Test",
                        City="New York",
                        Street="NY",
                        ZipCode="90210"
                    }
                };
                await userManager.CreateAsync(user,"P@$$w0rd");
            }
        }
    }
}
