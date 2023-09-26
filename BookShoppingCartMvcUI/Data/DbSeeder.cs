﻿using Microsoft.AspNetCore.Identity;
using BookShoppingCartMvcUI.Constants;
using Microsoft.EntityFrameworkCore;
using System;

namespace BookShoppingCartMvcUI.Data
{
    public class DbSeeder
    {


        public static async Task SeedDefaultData(IServiceProvider service)
        {

            var dbContextsvc = service.GetRequiredService<ApplicationDbContext>();
            await dbContextsvc.Database.MigrateAsync();

            var userMgr = service.GetService<UserManager<IdentityUser>>();
            var roleMgr = service.GetService<RoleManager<IdentityRole>>();


            if (userMgr == null)
            {
                throw new ArgumentNullException(nameof(userMgr));
            }
            if (roleMgr == null)
            {
                throw new ArgumentNullException(nameof(userMgr));
            }

            //adding some Test to db
            await roleMgr.CreateAsync(new IdentityRole(Roles.Admin.ToString()));
            await roleMgr.CreateAsync(new IdentityRole(Roles.User.ToString()));

            // create admin user

            //var admin = new IdentityUser
            //{
            //    UserName = "admin@gmail.com",
            //    Email = "admin@gmail.com",
            //    EmailConfirmed = true
            //};

            //var userInDb = await userMgr.FindByEmailAsync(admin.Email);
            //if (userInDb is null)
            //{
            //    await userMgr.CreateAsync(admin, "Admin@123");
            //    await userMgr.AddToRoleAsync(admin,Roles.Admin.ToString());
            //}


           
        }
    }
}
