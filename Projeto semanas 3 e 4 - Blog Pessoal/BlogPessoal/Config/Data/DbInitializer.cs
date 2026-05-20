using BlogPessoal.Models;
using Microsoft.AspNetCore.Identity;

namespace BlogPessoal.Config.Data;

public static class DbInitializer
{
    public static async Task InitializeAsync(IServiceProvider serviceProvider)
    {
        var userManager = serviceProvider.GetRequiredService<UserManager<Usuario>>();
        var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole<int>>>();

        //as duas roles que vão ser usadas
        string[] roles = { "Admin", "Usuario" };

        //criar se não tiver no banco
        foreach(var role in roles)
        {
            if (!await roleManager.RoleExistsAsync(role))
                await roleManager.CreateAsync(new IdentityRole<int>(role));
        }

        string adminEmail = "admin@blogpessoal.com";

        var adminUser = await userManager.FindByEmailAsync(adminEmail);

        if(adminUser == null)
        {
            var novoAdmin = new Usuario
            {
                UserName = adminEmail,
                Email = adminEmail,
            };

            //senha admin
            var result = await userManager.CreateAsync(novoAdmin, "Chefe@Blog7");

            //novo admin
            if (result.Succeeded)
                await userManager.AddToRoleAsync(novoAdmin, "Admin");
        }

    }
}
