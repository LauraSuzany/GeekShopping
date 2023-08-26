using GeekShopping.IdentityServer.Configuration;
using GeekShopping.IdentityServer.Model;
using GeekShopping.IdentityServer.Model.Context;
using IdentityModel;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace GeekShopping.IdentityServer.Models.Initializer
{
    public class DBInitializer : IDBInitializer
    {
        private readonly MySQLContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public DBInitializer(MySQLContext context, 
            UserManager<ApplicationUser> userManager, 
            RoleManager<IdentityRole> roleManager)
        {
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public void Initialize()
        {
            if (_roleManager.FindByNameAsync(IdentityConfiguration.Admin).Result != null) return;
            _roleManager.CreateAsync(new IdentityRole(
                IdentityConfiguration.Admin)).GetAwaiter().GetResult();
            _roleManager.CreateAsync(new IdentityRole(
                IdentityConfiguration.Client)).GetAwaiter().GetResult();


            ApplicationUser client = new ApplicationUser()
            {
                UserName = "laura-client",
                Email = "laura-client@erudio.com.br",
                EmailConfirmed = true,
                PhoneNumber = "+55 (91)91980170827",
                FristName = "Laura",
                LastName = "Suzany"
            };

            _userManager.CreateAsync(client, "password").GetAwaiter().GetResult();
            _userManager.AddToRoleAsync(client, IdentityConfiguration.Client).GetAwaiter().GetResult();
            var clientClaims = _userManager.AddClaimsAsync(client, new Claim[]
            {
                new Claim(JwtClaimTypes.Name, $"{client.FristName} {client.LastName}"),
                new Claim(JwtClaimTypes.GivenName, client.FristName),
                new Claim(JwtClaimTypes.FamilyName, client.LastName),
                new Claim(JwtClaimTypes.Role, IdentityConfiguration.Client)
            });


            ApplicationUser admin = new ApplicationUser()
            {
                UserName = "laura-admin",
                Email = "laura-admin@erudio.com.br",
                EmailConfirmed = true,
                PhoneNumber = "+55 (91)91980170827",
                FristName = "Laura",
                LastName = "Suzany"
            };

            _userManager.CreateAsync(admin, "password").GetAwaiter().GetResult();
            _userManager.AddToRoleAsync(admin, IdentityConfiguration.Admin).GetAwaiter().GetResult();
            var adminClaims = _userManager.AddClaimsAsync(admin, new Claim[]
            {
                new Claim(JwtClaimTypes.Name, $"{admin.FristName} {admin.LastName}"),
                new Claim(JwtClaimTypes.GivenName, admin.FristName),
                new Claim(JwtClaimTypes.FamilyName, admin.LastName),
                new Claim(JwtClaimTypes.Role, IdentityConfiguration.Admin)
            });
        }
    }
}
