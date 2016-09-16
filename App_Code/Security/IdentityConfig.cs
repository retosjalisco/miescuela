﻿using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for IdentityConfig
/// </summary>
public class IdentityConfig
{
	public IdentityConfig()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    public static UserManager Create(
        IdentityFactoryOptions<UserManager> options, 
        IOwinContext context)
    {
        var manager = new UserManager();
  
        // Configure validation logic for usernames
        manager.UserValidator = new UserValidator<User>(manager)
        {
            AllowOnlyAlphanumericUserNames = false,
            RequireUniqueEmail = true
        };
        // Configure validation logic for passwords
        manager.PasswordValidator = new PasswordValidator
        {
            RequiredLength = 6,
            RequireNonLetterOrDigit = true,
            RequireDigit = true,
            RequireLowercase = true,
            RequireUppercase = true,
        };
        
        var dataProtectionProvider = options.DataProtectionProvider;
        if (dataProtectionProvider != null)
        {
            manager.UserTokenProvider = new DataProtectorTokenProvider<User>(
                dataProtectionProvider.Create("ASP.NET Identity"));
        }
        return manager;
    }
}
